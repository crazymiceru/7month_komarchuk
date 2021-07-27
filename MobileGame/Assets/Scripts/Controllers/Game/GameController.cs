using UnityEngine;
using UnityEngine.Advertisements;

namespace MobileGame
{
    internal sealed class GameController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("GameBuild");
        private GameM _gameM;
        private UnitM _playerM;
        private IUnitView _playerView;

        internal GameController()
        {
            var unityAds = GameObject.FindObjectOfType<UnityAds>();
            if (unityAds == null) Debug.LogWarning($"UnityAds dont find on the scene");
            Advertisement.AddListener(unityAds);

            _playerM = new UnitM();
            _gameM = new GameM(unityAds);
            _gameM.gameState.Subscribe(ChangeStateGame);
            _gameM.gameState.Value = GameState.menu;
        }

        protected override void OnDispose()
        {
            _gameM.gameState.UnSubscribe(ChangeStateGame);
        }

        private void ChangeStateGame(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.menu:
                    Menu();
                    break;
                case GameState.startLevel:
                    StartGame();
                    break;
                case GameState.gameOver:
                    break;
                case GameState.winGame:
                    break;
                default:
                    break;
            }
        }

        private void Menu()
        {
            Clear();
            AddController(new MenuGlobalController(_gameM.gameState));
        }

        private void StartGame()
        {
            Transform playerTransform;
            Transform skyTransform;
            Clear();
            _gameM.Analitics.SendMessage("Start Game");
            var player = new PlayerBuild().Create(_gameM, _playerM);
            _playerView = player[0].iUnitView;
            AddController(player);
            playerTransform = player[0].gameObject.transform;
            var sceneController = new SceneController(1);
            AddController(sceneController);
            var dataScene = sceneController[0];
            skyTransform = dataScene.gameObject.transform.GetComponentInChildren<TagSky>().gameObject.transform;
            if (playerTransform != null && skyTransform != null)
            {
                AddController(new ParallaxController(skyTransform, playerTransform, LoadResources.GetValue<ParalaxCfg>("Any/ParalaxBackground")));
                AddController(new ParallaxController(Reference.MainCamera.transform, playerTransform, LoadResources.GetValue<ParalaxCfg>("Any/ParalaxCamera")));
            }
            AddController(new ActivateMazeElementsController(dataScene.gameObject.transform, _playerM, _playerView));
        }
    }
}
