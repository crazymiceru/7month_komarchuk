﻿using UnityEngine;

namespace MobileGame
{
    internal sealed class InputController : ControllerBasic, IExecute
    {
        private ControlLeak _controlLeak = new ControlLeak("TouchController");
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

                _controlM.control.Value = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            
            _controlM.isJump.Value = Input.GetButtonDown("Jump");
        }
    }

}