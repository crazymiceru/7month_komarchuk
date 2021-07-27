using System.Collections.Generic;
using UnityEngine;

namespace MobileGame
{
    public static class UtilsUnit
    {
        public static ControllerBasic ParseType(TypeUnit typeItem)
        {
            return typeItem switch
            {
                TypeUnit.Player => new PlayerBuild(),
                TypeUnit.UpgradeItem => new UpgradeItemBuild(),
                TypeUnit.EffectsItem => new EffectsItemBuild(),
                TypeUnit.EnemyBird => new EnemyBirdBuild(),
                TypeUnit.None => new EmptyBuild(),
                _ => new EmptyBuild(),

            };
        }
        public static void DecompositeItems(ItemsArray itemsArray, Dictionary<int, ItemCfg> itemsArrayOut)
        {
            for (int i = 0; i < itemsArray.ItemCfg.Count; i++)
            {
                ItemCfg item = itemsArray.ItemCfg[i];
                if (!itemsArrayOut.ContainsKey(item.Id))
                {
                    itemsArrayOut.Add(item.Id, item);
                }
                else Debug.LogWarning($"Double Id of elements {item}:{itemsArrayOut[item.Id]}");
            }
        }
    }
}