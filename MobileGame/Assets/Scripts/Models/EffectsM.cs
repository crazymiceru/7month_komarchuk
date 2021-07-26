using System;
using System.Collections.Generic;
using UnityEngine;

namespace MobileGame
{
    public sealed class EffectsM : IItemsM<EffectsItemCfg>
    {
        public event Action<EffectsItemCfg,bool> EvtAddItem = delegate { };
        public event Action<EffectsItemCfg> EvtRemoveItem = delegate { };
        public IReadOnlyDictionary<int, ItemCfg> allItems { get; private set; }

        private ControlLeak _controlLeak = new ControlLeak("EffectsM");
        private List<ItemCfg> _effects = new List<ItemCfg>();

        public EffectsM(ItemsArray itemsForEffects)
        {
            var _allEffects = new Dictionary<int, ItemCfg>();
            UtilsUnit.DecompositeItems(itemsForEffects, _allEffects);
            allItems = _allEffects;
        }

        public void AddItem(int ID)
        {
            if (allItems.TryGetValue(ID, out ItemCfg itemCfg))
            {
                var effectsItemCfg = itemCfg as EffectsItemCfg;
                if (!_effects.Contains(effectsItemCfg))
                {
                    _effects.Add(effectsItemCfg);
                    EvtAddItem.Invoke(effectsItemCfg,false);
                }
                else EvtAddItem.Invoke(effectsItemCfg, true);
                Debug.Log($"Added the EffectItem: {effectsItemCfg}");
            }
            else Debug.LogWarning($"Attempt to add  an unknown UpgradeItemID:{ID}");
        }

        public void RemoveItem(int ID)
        {
            if (allItems.TryGetValue(ID, out ItemCfg itemCfg))
            {
                var effectsItemCfg = itemCfg as EffectsItemCfg;
                if (_effects.Contains(effectsItemCfg))
                {
                    _effects.Remove(effectsItemCfg);
                    EvtRemoveItem.Invoke(effectsItemCfg);
                }
                else Debug.LogWarning($"Attempt to delete an unknown EffectItem:{effectsItemCfg}");
            }
            else Debug.LogWarning($"Attempt to remove an unknown EffectItemID:{ID}");
        }
    }
}