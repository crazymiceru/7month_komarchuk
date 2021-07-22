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
            if (Application.isEditor) AddController(new InputController(controlM));
            else AddController(new TouchController(controlM));

        }
    }

}