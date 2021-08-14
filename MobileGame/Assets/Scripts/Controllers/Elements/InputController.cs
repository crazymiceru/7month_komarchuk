using UnityEngine;

namespace MobileGame
{
    internal sealed class InputController : ControllerBasic, IExecute
    {
        private ControlLeak _controlLeak = new ControlLeak("InputController");
        private ControlM _controlM;
        private Vector2 _vector2Zero= Vector2.zero;

        internal InputController(ControlM controlM)
        {
            _controlM = controlM;
        }

        public void Execute(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
            {   
                _controlM.isNewTouch.Value = true;
                
            }
            else _controlM.isNewTouch.Value = false;

            if (Input.GetMouseButton(0))
            {
                _controlM.positionCursor.Value = Reference.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            }

            var currentControlValue = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (currentControlValue != _controlM.control.Value) _controlM.control.Value = currentControlValue;
            _controlM.isJump.Value = Input.GetButtonDown("Jump");
        }
    }

}