using UnityEngine;

namespace MobileGame
{
    internal sealed class UpgradeController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("");
        private UpgradeM _upgradeM;
        private UnitM _unitM;

        internal UpgradeController(UpgradeM upgradeM, UnitM unitM) : base()
        {
            _upgradeM = upgradeM;
            _unitM = unitM;
            _upgradeM.EvtAddItem += AddItem;
            _upgradeM.EvtRemoveItem += RemoveItem;
        }

        private void AddItem(UpgradeItemCfg upgradeItemCfg)
        {
            SetUpgradeParams(upgradeItemCfg, 1);
        }

        private void RemoveItem(UpgradeItemCfg upgradeItemCfg)
        {
            SetUpgradeParams(upgradeItemCfg, -1);
        }

        private void SetUpgradeParams(UpgradeItemCfg upgradeItemCfg, int sign)
        {
            switch (upgradeItemCfg.TypeUpgrade)
            {
                case TypeUpgrade.speed:
                    _unitM.maxSpeed.Value += upgradeItemCfg.Value * sign;
                    Debug.Log($"MaxSpeed:{_unitM.maxSpeed.Value}");
                    break;
                case TypeUpgrade.jump:
                    _unitM.powerJump.Value += upgradeItemCfg.Value * sign;
                    Debug.Log($"PowerJump:{_unitM.powerJump.Value}");
                    break;
                case TypeUpgrade.none:
                    break;
            }
        }



    }
}
