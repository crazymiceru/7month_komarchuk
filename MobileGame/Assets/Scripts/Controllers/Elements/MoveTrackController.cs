using UnityEngine;

namespace MobileGame
{
    internal sealed class MoveTrackController : ControllerBasic, IExecute, IInitialization
    {
        ITraectory _iTraectory;
        private int _numTraectory;
        private ControlLeak _controlLeak = new ControlLeak("MoveTrackController");        
        private DataUnit _dataUnit;
        private SubscriptionField<Vector2> _control;
        private IUnitView _iUnitView;

        internal MoveTrackController(SubscriptionField<Vector2> control,  ITraectory iTraectory,IUnitView iUnitView, DataUnit dataUnit)
        {
            _control = control;
            _iTraectory = iTraectory;
            _iUnitView = iUnitView;
            _dataUnit = dataUnit;
        }

        public void Initialization()
        {
            _numTraectory = 0;
        }

        public void Execute(float deltaTime)
        {
            if (_iTraectory.Track.Length > 0)
            {
                Vector2 control=Vector2.zero;                
                control.x = Mathf.Sign(_iTraectory.Track[_numTraectory].transform.position.x - _iUnitView.objectTransform.position.x) * _iTraectory.Track[_numTraectory].powerMove;
                control.y = Mathf.Sign(_iTraectory.Track[_numTraectory].transform.position.y - _iUnitView.objectTransform.position.y) * _iTraectory.Track[_numTraectory].powerMove;
                if (Mathf.Abs(_iTraectory.Track[_numTraectory].transform.position.x - _iUnitView.objectTransform.position.x)
                    <= _iTraectory.Track[_numTraectory].powerMove * deltaTime) control.x = 0;
                if (Mathf.Abs(_iTraectory.Track[_numTraectory].transform.position.y - _iUnitView.objectTransform.position.y)
                    <= _iTraectory.Track[_numTraectory].powerMove * deltaTime) control.y = 0;

                var d = Utils.SqrDist(_iUnitView.objectTransform.position.Change(z:0), _iTraectory.Track[_numTraectory].transform.position.Change(z: 0));
                if (d < _dataUnit.MinSqrLenghthTraectory)
                {
                    _numTraectory++;
                    if (_numTraectory == _iTraectory.Track.Length) _numTraectory = 0;
                    //Debug.Log($"Next Traectory {_unitView.Track[_numTraectory].transform.gameObject.name}");
                }
                _control.Value = control;
            }
        }
    }
}