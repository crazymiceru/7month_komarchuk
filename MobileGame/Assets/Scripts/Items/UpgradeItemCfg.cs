using UnityEngine;

namespace MobileGame
{
    [CreateAssetMenu(menuName = "My/UpgradeItem", fileName = "UpgradeItem")]
    public class UpgradeItemCfg : ItemCfg
    {
        [SerializeField] private ItemCfg _placeofUpgrade;
        public ItemCfg PlaceOfUpgrade => _placeofUpgrade;

        [SerializeField] private Sprite _sprite;
        public Sprite Sprite => _sprite;

        [SerializeField] private TypeUpgrade _typeUpgrade;
        public TypeUpgrade TypeUpgrade => _typeUpgrade;

        [SerializeField] private float _value;
        public float Value => _value;
    }

}