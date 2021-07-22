using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MobileGame
{
    internal sealed class UpgradeShowController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("UpgradeShowController");
        private const string _nameRes = "Items/Equip";
        private IUpgradeM _upgradeM;
        private Dictionary<ItemCfg, Image> placeUpgradesTransform=new Dictionary<ItemCfg, Image>();

        internal UpgradeShowController(IUpgradeM upgradeM) : base()
        {
            var data = CreateGameObject(Reference.Canvas, _nameRes);
            _upgradeM = upgradeM;
            _upgradeM.EvtAddItem += AddItem;
            _upgradeM.EvtRemoveItem += RemoveItem;
            foreach (var item in _upgradeM.allPlaces)
            {
                //Debug.Log($"Place:{item.Value.name}");
                var currentPlace=data.gameObject.transform.Find(item.Value.name);
                if (currentPlace!=null)
                {
                    if (currentPlace.gameObject.TryGetComponent(out Image image))
                    {
                        placeUpgradesTransform.Add(item.Value, image);
                        image.enabled = false;
                    }
                }
                else Debug.LogWarning($"Dont find the transform Place:{item.Value.name}");
            }
        }

        private void AddItem(UpgradeItemCfg upgradeItemCfg)
        {
            if (placeUpgradesTransform.TryGetValue(upgradeItemCfg.PlaceOfUpgrade,out Image image))
            {
                image.enabled = true;
                image.sprite = upgradeItemCfg.Sprite;
            }            
            else Debug.LogWarning($"There is no installable place:{upgradeItemCfg.PlaceOfUpgrade.name}");
        }

        private void RemoveItem(UpgradeItemCfg upgradeItemCfg)
        {
            if (placeUpgradesTransform.TryGetValue(upgradeItemCfg.PlaceOfUpgrade, out Image image))
            {
                image.enabled = false;
            }
            else Debug.LogWarning($"There is no installable place:{upgradeItemCfg.PlaceOfUpgrade.name}");
        }
    }
}
