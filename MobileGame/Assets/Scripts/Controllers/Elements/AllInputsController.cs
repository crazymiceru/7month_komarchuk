﻿using UnityEngine;

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
                AddController(new InputController(controlM));
#else
                AddController(new TouchController(controlModel));
#endif
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