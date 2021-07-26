using UnityEngine;

namespace MobileGame
{
    [CreateAssetMenu(menuName = "My/Items/EffectsItem", fileName = "EffectsItem")]
    public class EffectsItemCfg : ItemCfg
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private TypeModification _typeModification;
        [SerializeField] private float _value;
        [SerializeField] private float _time;

        public Sprite Sprite => _sprite;
        public TypeModification TypeUpgrade => _typeModification;
        public float Value => _value;
        public float Time => _time;
    }

}