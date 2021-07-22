using System;
using System.Collections.Generic;

namespace MobileGame
{
    public interface IUpgradeM
    {
        public void AddItem(int ID);
        public void RemoveItem(int ID);
        public Dictionary<int, ItemCfg> allUpgrades { get; }
        public Dictionary<int, ItemCfg> allPlaces { get; }

        public event Action<UpgradeItemCfg> EvtAddItem;
        public event Action<UpgradeItemCfg> EvtRemoveItem;

    }
}