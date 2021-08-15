using UnityEngine;
using UnityEngine.Advertisements;

namespace MobileGame
{
    internal sealed class GlobalGameController : ControllerBasic
    {
        private GameModel _gameModel;
        private UnitModel _playerModel;

        public GlobalGameController()
        {
            var unityAds = GameObject.FindObjectOfType<UnityAds>();
            if (unityAds == null) Debug.LogWarning($"UnityAds dont find on the scene");
            Advertisement.AddListener(unityAds);
            _playerModel = new UnitModel();
            _gameModel = new GameModel(unityAds);
            AddController(new FireBirdController(_gameModel));
            AddController(new GameController(_gameModel,_playerModel));
            AddController(new NotificationController(_gameModel));
        }
    }
}
