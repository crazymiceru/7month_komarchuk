using UnityEngine;

namespace MobileGame
{
    internal sealed class ModificationController<T> : ControllerBasic 
    {
        private ControlLeak _controlLeak = new ControlLeak("UpgradeController");
        private IItemsM<T> _upgradeM;
        private UnitM _unitM;
        private IUnitView _unitView;

        internal ModificationController(IItemsM<T> upgradeM, UnitM unitM,IUnitView unitView)
        {
            _upgradeM = upgradeM;
            _unitM = unitM;
            _upgradeM.EvtAddItem += AddItem;
            _upgradeM.EvtRemoveItem += RemoveItem;
            _unitView = unitView;
        }

        private void AddItem(T upgradeItemCfg,bool isHave)
        {
            if (!isHave) SetUpgradeParams(upgradeItemCfg, 1);
        }

        private void RemoveItem(T upgradeItemCfg)
        {
            SetUpgradeParams(upgradeItemCfg, -1);
        }

        private void SetUpgradeParams(T upgradeItemCfg, int sign)
        {
            TypeModification typeUpgrade = default;
            float value = default;

            if (upgradeItemCfg is EffectsItemCfg)
            {
                var cfg = (upgradeItemCfg as EffectsItemCfg);
                typeUpgrade = cfg.TypeUpgrade;
                value = cfg.Value;
            }
            else if (upgradeItemCfg is UpgradeItemCfg)
            {
                var cfg = (upgradeItemCfg as UpgradeItemCfg);
                typeUpgrade = cfg.TypeUpgrade;
                value = cfg.Value;
            }
            else Debug.LogWarning($"Error parse upgradeItemCfg");

            switch (typeUpgrade)
            {
                case TypeModification.speed:
                    _unitM.maxSpeed.Value += value * sign;
                    Debug.Log($"MaxSpeed:{_unitM.maxSpeed.Value}");
                    break;
                case TypeModification.jump:
                    _unitM.powerJump.Value += value * sign;
                    Debug.Log($"PowerJump:{_unitM.powerJump.Value}");
                    break;
                case TypeModification.setSize:
                    var currentSize = _unitView.objectTransform.localScale.x;
                    currentSize += value * sign;
                    _unitView.objectTransform.localScale = new Vector3(currentSize, currentSize, currentSize);
                    Debug.Log($"SmallCar");
                    break;
                case TypeModification.none:
                    break;
            }
        }
    }
}
