using UnityEngine;

namespace MobileGame
{
    internal sealed class MoveGroundController : ControllerBasic, IExecute
    {
        private ControlLeak _controlLeak = new ControlLeak("MoveGroundController");
        private IUnitView _unitView;
        private Vector2 leftRight;
        private DataUnit _unitData;
        private iReadOnlySubscriptionField<Vector2> _control;
        private iReadOnlySubscriptionField<bool> _isJump;
        private iReadOnlySubscriptionField<float> _maxSpeed;
        private iReadOnlySubscriptionField<float> _powerJump;
        private iReadOnlySubscriptionField<bool> _isOnGround;

        internal MoveGroundController(iReadOnlySubscriptionField<Vector2> control, iReadOnlySubscriptionField<bool> isJump,
            iReadOnlySubscriptionField<float> maxSpeed,
            iReadOnlySubscriptionField<float> powerJump,
            iReadOnlySubscriptionField<bool> isOnGround,
            IUnitView unitView, DataUnit unitData)
        {
            _control = control;
            _isJump = isJump;
            _unitView = unitView;
            _unitData = unitData;
            _maxSpeed = maxSpeed;
            _powerJump = powerJump;
            _isOnGround = isOnGround;
        }

        public void Execute(float deltaTime)
        {
            Move(deltaTime);
            SetLimits();
        }

        private void SetLimits()
        {
            var velocity = _unitView.objectRigidbody2D.velocity;
            velocity.y = 0;
            velocity = Vector3.ClampMagnitude(velocity, _maxSpeed.Value);
            velocity.y = _unitView.objectRigidbody2D.velocity.y;
            _unitView.objectRigidbody2D.velocity = velocity;
        }

        private void Move(float deltaTime)
        {
            leftRight.x = deltaTime * _unitData.PowerMove * _unitView.objectRigidbody2D.mass * _control.Value.x;
            _unitView.objectRigidbody2D.AddForce(leftRight);
            if (_isJump.Value && _isOnGround.Value) _unitView.objectRigidbody2D.AddForce(_powerJump.Value * _unitView.objectRigidbody2D.mass * Vector2.up);
        }

    }
}