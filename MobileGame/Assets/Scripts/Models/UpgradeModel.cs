using System;
using System.Collections.Generic;
using UnityEngine;

namespace MobileGame
{
    public sealed class UpgradeModel : IItemsModel<UpgradeItemCfg>
    {
        public event Action<UpgradeItemCfg, bool> EvtAddItem = delegate { };
        public event Action<UpgradeItemCfg> EvtRemoveItem = delegate { };
        public IReadOnlyDictionary<int, ItemCfg> allItems { get; private set; }
        public IReadOnlyDictionary<int, ItemCfg> allPlaces { get; private set; }

        private ControlLeak _controlLeak = new ControlLeak("UpgradeM");
        private Dictionary<ItemCfg, ItemCfg> _upgrades = new Dictionary<ItemCfg, ItemCfg>();

        public UpgradeModel(ItemsArray itemsForUpgrades, ItemsArray places)
        {
            allItems = UtilsUnit.DecompositeItems(itemsForUpgrades);
            allPlaces = UtilsUnit.DecompositeItems(places);
        }

        public void AddItem(int ID)
        {
            if (allItems.TryGetValue(ID, out ItemCfg itemCfg))
            {
                var upgradeItemCfg = itemCfg as UpgradeItemCfg;
                if (_upgrades.ContainsKey(upgradeItemCfg.PlaceOfUpgrade))
                {
                    EvtRemoveItem.Invoke(_upgrades[upgradeItemCfg.PlaceOfUpgrade] as UpgradeItemCfg);
                    _upgrades[upgradeItemCfg.PlaceOfUpgrade] = upgradeItemCfg;
                }
                else _upgrades.Add(upgradeItemCfg.PlaceOfUpgrade, upgradeItemCfg);
                EvtAddItem.Invoke(upgradeItemCfg, false);
                //Debug.Log($"Added the UpgradeItem: {upgradeItemCfg} on Place:{upgradeItemCfg.PlaceOfUpgrade}");
            }
            else Debug.LogWarning($"Attempt to add  an unknown UpgradeItemID:{ID}");
        }

        public void RemoveItem(int ID)
        {
            if (allItems.TryGetValue(ID, out ItemCfg itemCfg))
            {
                var upgradeItemCfg = itemCfg as UpgradeItemCfg;
                if (_upgrades.ContainsKey(upgradeItemCfg))
                {
                    _upgrades.Remove(upgradeItemCfg.PlaceOfUpgrade);
                    EvtRemoveItem.Invoke(upgradeItemCfg);
                }
                else Debug.LogWarning($"Attempt to delete an unknown UpgradeItem:{upgradeItemCfg}");
            }
            else Debug.LogWarning($"Attempt to remove an unknown UpgradeItemID:{ID}");
        }
    }
}