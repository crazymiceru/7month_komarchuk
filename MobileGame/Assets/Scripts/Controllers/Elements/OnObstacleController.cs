using UnityEngine;

namespace MobileGame
{
    
    internal sealed class OnObstacleController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("OnCollisionController");
        private UnitM _unit;
        private Transform _transform;
        private int count;

        internal OnObstacleController(UnitM unit, IUnitView iUnitView)
        {
            _unit = unit;
            _transform = iUnitView.objectTransform;
            if (FindDetectCollision("WhellBack", out OnTriggerView onGroundView)) onGroundView.evtUpdate += DetectGround;
            else Debug.LogWarning($"Dont find WhellBack OnTriggerView");
            if (FindDetectCollision("WhellFront", out OnTriggerView onGroundView2)) onGroundView2.evtUpdate += DetectGround;
            else Debug.LogWarning($"Dont find WhellFront OnTriggerView");
            count = 0;
        }


        private bool FindDetectCollision(string name,out OnTriggerView onGroundView)
        {
            bool isOk = true;
            onGroundView = null;

            var go = _transform.Find(name);
            if (go == null)
            {
                isOk = false;
            }
            onGroundView= go.GetComponent<OnTriggerView>();
            if (onGroundView == null)
            {
                isOk = false;
            }
            return isOk;
        }

        private void DetectGround(Collider2D _, bool isEnter)
        {
            if (isEnter) count++;
            else count--;
            if (count>0 && !_unit.isOnGround.Value) _unit.isOnGround.Value=true;
            if (count==0 && _unit.isOnGround.Value) _unit.isOnGround.Value=false;

            if (_unit.isOnGround.Value) _unit.command.Value = Commands.onGround;            
        }
    }
}