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
            _controlM = controlM;
            if (Application.isEditor && !isRemoteConnected())
            {
                AddController(new InputController(controlM));
                Debug.Log($"Keyboard Control");
            }
            else
            {
                AddController(new TouchController(controlM));
                Debug.Log($"Touch Control");
            }

            bool isRemoteConnected()
            {
                bool result = false;
#if UNITY_EDITOR
                result = UnityEditor.EditorApplication.isRemoteConnected;
#endif
                return result;
            }
        }
    }

}