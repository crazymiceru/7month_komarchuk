using UnityEngine;

namespace MobileGame
{
    
    internal sealed class OnObstacleController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("OnObstacleController");
        private UnitModel _unit;
        private Transform _transform;
        private int count;
        SubscriptionField<bool> _isObstacle;

        internal OnObstacleController(UnitModel unit, IUnitView iUnitView,SubscriptionField<bool> isObstacle)
        {
            _unit = unit;
            _transform = iUnitView.objectTransform;
            _isObstacle = isObstacle;

            var allGroundView = iUnitView.objectTransform.GetComponentsInChildren<OnTriggerView>();
            foreach (var item in allGroundView)
                item.evtUpdate += DetectGround;
            count = 0;
        }

        private void DetectGround(Collider2D _, bool isEnter)
        {
            if (isEnter) count++;
            else count--;
            if (count>0 && !_unit.isOnGround.Value) _isObstacle.Value=true;
            if (count==0 && _unit.isOnGround.Value) _isObstacle.Value =false;
        }
    }
}