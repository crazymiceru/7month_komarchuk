﻿using UnityEngine;

namespace MobileGame
{
    internal sealed class AddModificationController<T> : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("UpgradeController");
        private IItemsModel<T> _upgradeM;
        private UnitModel _unitModel;
        private IUnitView _unitView;
        private SpriteRenderer _goShield;

        internal AddModificationController(IItemsModel<T> upgradeM, UnitModel unitModel, IUnitView unitView)
        {
            _upgradeM = upgradeM;
            _unitModel = unitModel;
            _unitView = unitView;

            var _tagShield = unitView.objectTransform.GetComponentInChildren<TagShield>();
            if (_tagShield == null) Debug.LogWarning($"Dont find Shield View");
            _goShield = _tagShield.gameObject.GetComponent<SpriteRenderer>();
            _goShield.enabled = _unitModel.isShielded.Value;

            _upgradeM.EvtAddItem += AddItem;
            _upgradeM.EvtRemoveItem += RemoveItem;
        }

        protected override void OnDispose()
        {
            _upgradeM.EvtAddItem -= AddItem;
            _upgradeM.EvtRemoveItem -= RemoveItem;
        }

        private void AddItem(T upgradeItemCfg, bool isHave)
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

            var isOn = sign > 0 ? true : false;

            switch (typeUpgrade)
            {
                case TypeModification.speed:
                    _unitModel.maxSpeed.Value += value * sign;
                    break;
                case TypeModification.jump:
                    _unitModel.powerJump.Value += value * sign;                    
                    break;
                case TypeModification.setSize:
                    var currentSize = _unitView.objectTransform.localScale.x;
                    currentSize += value * sign;
                    _unitView.objectTransform.localScale = new Vector3(currentSize, currentSize, currentSize);
                    break;
                case TypeModification.shield:
                    _unitModel.isShielded.Value = isOn;
                    _goShield.enabled=isOn;
                    break;
                case TypeModification.none:
                    break;
            }
        }
    }
}
