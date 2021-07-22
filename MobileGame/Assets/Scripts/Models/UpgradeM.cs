using System;
using System.Collections.Generic;
using UnityEngine;

namespace MobileGame
{
    public sealed class UpgradeM : IUpgradeM
    {
        private ControlLeak _controlLeak = new ControlLeak("UpgradeM");
        private Dictionary<ItemCfg, ItemCfg> _upgrades = new Dictionary<ItemCfg, ItemCfg>();
        public event Action<UpgradeItemCfg> EvtAddItem = delegate { };
        public event Action<UpgradeItemCfg> EvtRemoveItem = delegate { };

        public Dictionary<int, ItemCfg> allUpgrades  {get; private set; }
        public Dictionary<int, ItemCfg> allPlaces { get; private set; }

        public UpgradeM(ItemsArray itemsForUpgrades, ItemsArray places)
        {
            allUpgrades = new Dictionary<int, ItemCfg>();
            MakeDictionary(itemsForUpgrades, allUpgrades);
            allPlaces = new Dictionary<int, ItemCfg>();
            MakeDictionary(places, allPlaces);
        }

        private void MakeDictionary(ItemsArray itemsArray, Dictionary<int, ItemCfg> dict)
        {

            for (int i = 0; i < itemsArray.ItemCfg.Count; i++)
            {
                    ItemCfg item = itemsArray.ItemCfg[i];
                    if (!dict.ContainsKey(item.Id))
                    {
                            dict.Add(item.Id, item);
                    }
                    else Debug.LogWarning($"Double Id of elements {item}:{dict[item.Id]}");
            }
        }

        public void AddItem(int ID)
        {
            if (allUpgrades.TryGetValue(ID, out ItemCfg itemCfg))
            {
                var upgradeItemCfg = itemCfg as UpgradeItemCfg;
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
            if (allUpgrades.TryGetValue(ID, out ItemCfg itemCfg))
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