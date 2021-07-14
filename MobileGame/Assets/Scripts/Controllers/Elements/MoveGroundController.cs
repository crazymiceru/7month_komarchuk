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

        internal MoveGroundController(iReadOnlySubscriptionField<Vector2> control, iReadOnlySubscriptionField<bool> isJump, IUnitView unitView, DataUnit unitData)
        {
            _control= control;
            _isJump = isJump;
            _unitView = unitView;
            _unitData = unitData;
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
            velocity = Vector3.ClampMagnitude(velocity, _unitData.MaxSpeed);
            velocity.y = _unitView.objectRigidbody2D.velocity.y;
            _unitView.objectRigidbody2D.velocity = velocity;
        }

        private void Move(float deltaTime)
        {
            leftRight.x = deltaTime * _unitData.PowerMove * _unitView.objectRigidbody2D.mass * _control.Value.x;
            _unitView.objectRigidbody2D.AddForce(leftRight);
            //if (leftRight.x != 0) _unit.command = Commands.run;
            //else _unit.command = Commands.stop;
            //if (_unitView.objectRigidbody2D.velocity.y < -0.1f /*&& !_unit.isOnGround*/) _unit.command = Commands.fall;
            //if (_unit.isOnGround) _unit.command = Commands.onGround;
            if (_isJump.Value ) _unitView.objectRigidbody2D.AddForce(_unitData.PowerJump * _unitView.objectRigidbody2D.mass * Vector2.up);
            //if (_unit.isJump) _unit.command = Commands.jump;

        }

    }
}