

using DG.Tweening;
using System.Linq;
using UnityEngine;

namespace MobileGame
{
    internal sealed class AnimationWheelController : ControllerBasic
    {
        private const float _speedRotateWheel = 1f;
        private ControlLeak _controlLeak = new ControlLeak("");
        private UnitModel _unit;
        private IUnitView _iUnitView;
        private iReadOnlySubscriptionField<Vector2> _control;
        private Transform[] _whells;
        private Vector3 _valueRotateForward = new Vector3(0, 0, -360);
        private Vector3 _valueRotateBackward = new Vector3(0, 0, 360);

        internal AnimationWheelController(iReadOnlySubscriptionField<Vector2> control, IUnitView iUnitView)
        {
            _control = control;
            _iUnitView = iUnitView;
            _whells = _iUnitView.objectTransform.GetComponentsInChildren<TagWheel>()
                .Select(x => (x as MonoBehaviour).transform).ToArray();
            _control.Subscribe(UpdateControl);
        }

        protected override void OnDispose()
        {
            _control.UnSubscribe(UpdateControl);
        }

        private void UpdateControl(Vector2 value)
        {
            
            var _valueRotate = value.x > 0 ? _valueRotateForward : _valueRotateBackward ;
            for (int i = 0; i < _whells.Length; i++)
            {
                //Debug.Log($"DoRotate");
                _whells[i].DOKill();
                if (value.x != 0) _whells[i].DORotate(_valueRotate, 1 / Mathf.Abs(value.x) / _speedRotateWheel, RotateMode.LocalAxisAdd).SetLoops(-1);
            }
        }
    }
}
