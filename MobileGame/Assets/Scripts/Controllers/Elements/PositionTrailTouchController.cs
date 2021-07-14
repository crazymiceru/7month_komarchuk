using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MobileGame
{
    internal sealed class PositionTrailTouchController : ControllerBasic, IExecute
    {
        private ControlLeak _controlLeak = new ControlLeak("PositionTouchController");
        private List<DataTrails> _trails = new List<DataTrails>();
        private int currentTrail = 0;
        private Transform _folder;
        private iReadOnlySubscriptionField<Vector2> _positionTouch;
        private iReadOnlySubscriptionField<bool> _isNewTouch;

        internal PositionTrailTouchController(iReadOnlySubscriptionField<Vector2> positionTouch, iReadOnlySubscriptionField<bool> isNewTouch, GameObject trailObject, Transform folder)
        {
            _positionTouch = positionTouch;
            _isNewTouch = isNewTouch;
            _trails.Add(new DataTrails { transform = trailObject.transform, renderer = trailObject.GetComponent<TrailRenderer>() });
            _folder = folder;
        }

        protected override void OnDispose()
        {
            for (int i = 0; i < _trails.Count; i++)
            {
                Object.Destroy(_trails[i].transform.gameObject);
            }
        }

        public void Execute(float deltaTime)
        {

            if (_isNewTouch.Value)
            {
                bool isFindTrail = false;
                for (int i = 0; i < _trails.Count; i++)
                {
                    if (_trails[i].renderer.positionCount == 0)
                    {
                        currentTrail = i;
                        isFindTrail = true;
                    }
                }
                if (!isFindTrail)
                {
                    var trailObject = Object.Instantiate(_trails[0].transform.gameObject, _folder);
                    _trails.Add(new DataTrails { transform = trailObject.transform, renderer = trailObject.GetComponent<TrailRenderer>() });
                    currentTrail = _trails.Count - 1;
                }
            }

            _trails[currentTrail].transform.position = _positionTouch.Value;
            if (_isNewTouch.Value) _trails[currentTrail].renderer.Clear();
        }
    }
}