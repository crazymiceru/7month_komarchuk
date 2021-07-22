using System;
using System.Collections.Generic;
using UnityEngine;

namespace MobileGame
{
    public interface IUpgardeM
    {
        public void AddItem(int ID);
        public void RemoveItem(int ID);
    }

    public sealed class UpgradeM : IUpgardeM
    {
        private ControlLeak _controlLeak = new ControlLeak("UpgradeM");
        private Dictionary<ItemCfg, ItemCfg> _upgrades = new Dictionary<ItemCfg, ItemCfg>();
        public event Action<UpgradeItemCfg> EvtAddItem = delegate { };
        public event Action<UpgradeItemCfg> EvtRemoveItem = delegate { };

        private Dictionary<int, UpgradeItemCfg> _allUpgrades = new Dictionary<int, UpgradeItemCfg>();

        public UpgradeM(ItemsArray itemsForUpgrades)
        {
            for (int i = 0; i < itemsForUpgrades.ItemCfg.Count; i++)
            {
                if (itemsForUpgrades.ItemCfg[i] is UpgradeItemCfg)
                {
                    UpgradeItemCfg item = itemsForUpgrades.ItemCfg[i] as UpgradeItemCfg;
                    if (!_allUpgrades.ContainsKey(item.Id))
                    {
                        _allUpgrades.Add(item.Id, item);
                    }
                    else Debug.LogWarning($"Double Id of elements {item}:{_allUpgrades[item.Id]}");
                }
                else Debug.LogWarning($"Items is not intended for updating {itemsForUpgrades.ItemCfg[i]}");
            }
        }

        public void AddItem(int ID)
        {
            if (_allUpgrades.TryGetValue(ID, out UpgradeItemCfg upgradeItemCfg))
            {
                if (_upgrades.ContainsKey(upgradeItemCfg.PlaceOfUpgrade))
                {
                    EvtRemoveItem.Invoke(_upgrades[upgradeItemCfg.PlaceOfUpgrade] as UpgradeItemCfg);
                    _upgrades[upgradeItemCfg.PlaceOfUpgrade] = upgradeItemCfg;
                }
                else _upgrades.Add(upgradeItemCfg.PlaceOfUpgrade, upgradeItemCfg);
                EvtAddItem.Invoke(upgradeItemCfg);
                Debug.Log($"Added the UpgradeItem: {upgradeItemCfg} on Place:{upgradeItemCfg.PlaceOfUpgrade}");
            }
            else Debug.LogWarning($"Attempt to add  an unknown UpgradeItemID:{ID}");
        }

        public void RemoveItem(int ID)
        {
            if (_allUpgrades.TryGetValue(ID, out UpgradeItemCfg upgradeItemCfg))
            {
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