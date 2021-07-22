using UnityEngine;

namespace MobileGame
{
    internal sealed class AllInputsController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("AllInputsController");
        private ControlM _controlM;
        private Vector2 _vector2Zero = Vector2.zero;

        internal AllInputsController(ControlM controlM)
        {
            Debug.Log($"Remote:{UnityEditor.EditorApplication.isRemoteConnected}");
            _controlM = controlM;
            if (Application.isEditor && !UnityEditor.EditorApplication.isRemoteConnected)
            {
                AddController(new InputController(controlM));
                Debug.Log($"Keyboard Control");
            }
            else
            {
                AddController(new TouchController(controlM));
                Debug.Log($"Touch Control");
            }

        }
    }

}