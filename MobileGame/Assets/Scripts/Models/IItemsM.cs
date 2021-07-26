using System;
using System.Collections.Generic;

namespace MobileGame
{
    public interface IItemsM<T>
    {
        event Action<T,bool> EvtAddItem;
        event Action<T> EvtRemoveItem;
        IReadOnlyDictionary<int, ItemCfg> allItems { get; }
        void AddItem(int ID);
        void RemoveItem(int ID);
    }
}