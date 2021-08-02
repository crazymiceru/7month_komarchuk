using UnityEngine;

namespace MobileGame
{
    [CreateAssetMenu(menuName = "My/Rewards/DailyRewardsVisual", fileName = "DailyRewardsVisual")]
    public sealed class DailyRewardsVisualCfg : ScriptableObject
    {
        public Sprite[] Sprite => _sprite;
        [SerializeField] private Sprite[] _sprite;
    }

}