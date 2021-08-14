using System;

namespace MobileGame
{
    internal sealed class SceneController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("SceneController");
        private const string _key = "Level ##";
        private const string _keyAddressable = "Level ##.prefab";
        private const string _bandle = "test";
        private const string _nameLevelsCfg = "Any/Levels";
        

        internal SceneController(int numCfg)
        {
            _numCfg = numCfg;
            //CreateGameObjectBandle(Reference.ActiveElements, _bandle, _key);
            var cfg = LoadResources.GetValue<LevelsCfg>(_nameLevelsCfg);
            //CreateGameObjectAddressable(Reference.ActiveElements, cfg.LoadPrefab[_numCfg]);
            CreateGameObjectAddressable(Reference.ActiveElements, _keyAddressable);
        }


    }
}
