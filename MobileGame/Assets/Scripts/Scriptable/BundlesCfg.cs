using UnityEngine;

namespace MobileGame
{
    [CreateAssetMenu(fileName ="BundlesCfg", menuName = "My/BundlesCfg")]
    public sealed class BundlesCfg : ScriptableObject
    {
        public BundlesData[] BundlesData => _bundlesData;
        [SerializeField] private BundlesData[] _bundlesData;
    }
}