using UnityEngine;

namespace MobileGame
{
    internal sealed class MoveFlyController : ControllerBasic, IExecute
    {
        private ControlLeak _controlLeak = new ControlLeak("MoveFlyController");
        private IUnitView _unitView;
        private DataUnit _unitData;
        private iReadOnlySubscriptionField<Vector2> _control;
        private iReadOnlySubscriptionField<float> _maxSpeed;

        internal MoveFlyController(iReadOnlySubscriptionField<Vector2> control,
            iReadOnlySubscriptionField<float> maxSpeed,
            IUnitView unitView, DataUnit unitData)
        {
            _control = control;
            _unitView = unitView;
            _unitData = unitData;
            _maxSpeed = maxSpeed;
        }

        public void Execute(float deltaTime)
        {
            Move(deltaTime);
            SetLimits();
        }

        private void SetLimits()
        {
            //velocity.y = 0;
            //velocity = Vector3.ClampMagnitude(velocity, _maxSpeed.Value);
            //velocity.y = _unitView.objectRigidbody2D.velocity.y;
            var velocity = _unitView.objectRigidbody2D.velocity;
            _unitView.objectRigidbody2D.velocity = Vector3.ClampMagnitude(velocity, _maxSpeed.Value); 
        }

        private void Move(float deltaTime)
        {
            var force = deltaTime * _unitData.PowerMove * _unitView.objectRigidbody2D.mass * _control.Value;
            _unitView.objectRigidbody2D.AddForce(force);
        }

    }
}