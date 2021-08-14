using UnityEngine;

namespace MobileGame
{
    internal sealed class AllParallaxController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("ParalaxController");

        internal AllParallaxController(Transform playerTransform, Transform skyTransform)
        {
            if (playerTransform == null) { Debug.LogWarning($"layerTransform == null"); return; }
            if (skyTransform == null) { Debug.LogWarning($"skyTransform == null"); return; }

            AddController(new ParallaxController(skyTransform, playerTransform, LoadResources.GetValue<ParalaxCfg>("Any/ParalaxBackground")));
            AddController(new ParallaxController(Reference.MainCamera.transform, playerTransform, LoadResources.GetValue<ParalaxCfg>("Any/ParalaxCamera")));
        }
    }
}
