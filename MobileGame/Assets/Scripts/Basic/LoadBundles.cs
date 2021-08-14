using System.Collections.Generic;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace MobileGame
{
    public sealed class BundlesResourceData
    {
        public BundlesData bundlesData;
        public AssetBundle assetBundle;
    }

    public sealed class LoadBundles
    {
        private const string _nameBundleCfg = "Any/BundlesCfg";

        private static Dictionary<string, BundlesResourceData> _bundlesCfg = new Dictionary<string, BundlesResourceData>();

        public static void Init()
        {
            _bundlesCfg.Clear();
            var bundleCfg = LoadResources.GetValue<BundlesCfg>(_nameBundleCfg);
            foreach (var item in bundleCfg.BundlesData)
                _bundlesCfg.Add(item.name, new BundlesResourceData { bundlesData = item });
        }

        public static void AddBundle(string name)
        {
            if (_bundlesCfg.TryGetValue(name, out BundlesResourceData bundlesResourceData))
            {
                if (bundlesResourceData.assetBundle == null)
                {
                    CoroutinesView.inst.AddCoroutine(DownloadBundle(bundlesResourceData));
                }
                else Debug.LogWarning($"Bundle {name} is already there");
            }
            else Debug.LogWarning($"Bundle {name} don't find in Config");
        }

        private static IEnumerator DownloadBundle(BundlesResourceData bundlesResourceData)
        {
            var request = UnityWebRequestAssetBundle.
                GetAssetBundle(bundlesResourceData.bundlesData.link, bundlesResourceData.bundlesData.versions, 0);

            var resut = request.SendWebRequest();
            while (!resut.isDone)
            {
                Debug.Log($"load:{bundlesResourceData.bundlesData.name} progress:{request.downloadProgress * 100f}");
                yield return null;
            }
            if (request.error != null)
            {
                Debug.LogWarning($"Error '{request.error}' Download Bundle '{bundlesResourceData.bundlesData.name}' ");
                yield break;
            }

            Debug.Log($"Download {bundlesResourceData.bundlesData.name} completed");

            bundlesResourceData.assetBundle = DownloadHandlerAssetBundle.GetContent(request);
        }

        public static T GetValue<T>(string bundle, string key) where T : Object
        {
            if (_bundlesCfg.TryGetValue(bundle, out BundlesResourceData bundlesResourceData))
            {
                if (bundlesResourceData.assetBundle == null)
                {
                    Debug.LogWarning($"Bundle {bundle} is NULL");
                    return null;
                }
                var asset = bundlesResourceData.assetBundle.LoadAsset<T>(key);
                if (asset != null) return asset;
                Debug.LogWarning($"Dont find in bundle:{bundle} key:{key}");
                return null;
            }
            Debug.LogWarning($"Dont find bundle:{bundle}");
            return null;
        }
    }
}