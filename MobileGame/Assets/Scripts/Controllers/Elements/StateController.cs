using System.Linq;
using UnityEngine;

namespace MobileGame
{
    internal sealed class StateController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("StateController");
        private StateMachineCfg _stateMachineCfg;
        private iReadOnlySubscriptionField<Commands> _command;
        private SubscriptionField<TypeAnimation> _typeAnimation;

        internal StateController(iReadOnlySubscriptionField<Commands> command,SubscriptionField<TypeAnimation> typeAnimation,  StateMachineCfg stateMachineCfg)
        {
            _command = command;
            _typeAnimation = typeAnimation;
            _stateMachineCfg = stateMachineCfg;
            command.Subscribe(GetCommand);
        }

        public void GetCommand(Commands command)
        {

            var targetAnimation = _stateMachineCfg.StateData.Where(x => x.command == command && (x.CurrentAnimation == TypeAnimation.Any || x.CurrentAnimation == _typeAnimation.Value)).Select(x => x.TargetAnimation).FirstOrDefault();

            if (targetAnimation != TypeAnimation.Any)
            {
                //Debug.Log($"Start:{_unit.typeAnimation} Command:{command} End:{targetAnimation}");
                if (_typeAnimation.Value != targetAnimation) _typeAnimation.Value = targetAnimation;
            }
        }
    }
}