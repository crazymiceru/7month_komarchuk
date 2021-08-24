using UnityEngine;

namespace MobileGame
{
    internal sealed class AllInputsController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("AllInputsController");
        private ControlM _controlModel;
        private Vector2 _vector2Zero = Vector2.zero;

        internal AllInputsController(ControlM controlModel)
        {
            _controlModel = controlModel;
            if (Application.isEditor && !isRemoteConnected())
            {
                AddController(new InputController(controlModel));
            }
            else
            {
#if UNITY_STANDALONE_WIN
                AddController(new InputController(controlModel));
#else
                AddController(new TouchController(controlModel));
#endif
            }

            bool isRemoteConnected()
            {
#if UNITY_EDITOR
                return UnityEditor.EditorApplication.isRemoteConnected;
#else
                return false;
#endif
            }
        }
    }

}