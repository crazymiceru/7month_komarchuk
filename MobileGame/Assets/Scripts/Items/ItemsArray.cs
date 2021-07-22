using System.Collections.Generic;
using UnityEngine;

namespace MobileGame
{
    [CreateAssetMenu(menuName = "My/ItemsArray", fileName = "Items")]
    public class ItemsArray : ScriptableObject
    {
        [SerializeField] private List<ItemCfg> _itemCfg;
        public List<ItemCfg> ItemCfg => _itemCfg;

        public void AddItems()
        {
            _itemCfg.Add(null);

        }
        public void DeleteItems(int i)
        {
            _itemCfg.RemoveAt(i);
        }
    }
}