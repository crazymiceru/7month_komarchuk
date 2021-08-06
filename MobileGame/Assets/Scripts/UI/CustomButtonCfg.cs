using UnityEngine;

namespace MobileGame
{
    [CreateAssetMenu(menuName = "My/ButtonUICfg", fileName = "ButtonUICfg")]
    public sealed class CustomButtonCfg : ScriptableObject
    {
        public TypeAnimationData[] typeAnimationData;
    }
}