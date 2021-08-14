using UnityEngine;

namespace MobileGame
{
    internal sealed class TouchController : ControllerBasic, IExecute
    {
        private ControlLeak _controlLeak = new ControlLeak("TouchController");
        private ControlM _controlM;
        private Camera _camera;
        private Vector3 _startPosition;
        private Vector2 _vector2Zero = Vector2.zero;

        internal TouchController(ControlM controlM)
        {
            _controlM = controlM;
            _camera = Reference.MainCamera;
        }

        public void Execute(float deltaTime)
        {
            _controlM.isNewTouch.Value = false;
            if (Input.touches.Length > 0)
            {
                var item = Input.touches[0];
                if (item.phase == TouchPhase.Moved || item.phase == TouchPhase.Began)
                {
                    var currentPosition = _camera.ScreenToWorldPoint(item.position) - _camera.transform.position;                    
                    _controlM.positionCursor.Value = _camera.ScreenToWorldPoint(item.position);

                    if (item.phase == TouchPhase.Began)
                    {
                        _controlM.isNewTouch.Value = true;
                        _startPosition = currentPosition;
                        UpdateControlPosition(currentPosition);
                    }
                    if (item.phase == TouchPhase.Moved)
                    {
                        UpdateControlPosition(currentPosition);
                    }

                }
            }
            else _controlM.control.Value = _vector2Zero;
            if (Input.touches.Length >= 2 && Input.touches[1].phase == TouchPhase.Began) _controlM.isJump.Value = true;
            else _controlM.isJump.Value = false;
        }

        private void UpdateControlPosition(Vector3 currentPosition) =>
            _controlM.control.Value = (currentPosition - _startPosition).normalized;        
    }

}