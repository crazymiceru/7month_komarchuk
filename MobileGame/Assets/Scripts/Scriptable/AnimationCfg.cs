using UnityEngine;

namespace MobileGame
{

    [CreateAssetMenu(menuName = "My/AnimationData",fileName ="UnitAnimation")]
    public sealed class AnimationCfg : ScriptableObject
    {
        public float Speed => _speed;
        [SerializeField] private float _speed=10;
        public AnimationData[] animationData => _animationData;
        [SerializeField] private AnimationData[] _animationData;
    }
}
