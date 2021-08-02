using UnityEngine;

namespace MobileGame
{
    [CreateAssetMenu(menuName = "My/Rewards/DailyReward", fileName = "DailyReward")]
    public sealed class DailyRewardCfg : ItemCfg
    {
        [SerializeField] private TypeReward _typeReward;
        [SerializeField] private int _value;

        public int Value => _value;
        public TypeReward TypeReward => _typeReward;
    }

}