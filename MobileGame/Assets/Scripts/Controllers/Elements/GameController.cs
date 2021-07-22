using UnityEngine;
using UnityEngine.Advertisements;

namespace MobileGame
{
    internal sealed class GameController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("GameBuild");
        private GameM _gameM;

        internal GameController() : base()
        {
            var unityAds = GameObject.FindObjectOfType<UnityAds>();
            if (unityAds == null) Debug.LogWarning($"UnityAds dont find on the scene");
            Advertisement.AddListener(unityAds);

            _gameM = new GameM(unityAds);
            _gameM.gameState.Subscribe(ChangeStateGame);
            _gameM.gameState.Value = GameState.menu;
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
            var player = new PlayerBuild().Create(_gameM);
            AddController(player);
            playerTransform = player[0].gameObject.transform;
            var sceneController = new SceneController(1); 
            AddController(sceneController);
            var go = sceneController[0];
            skyTransform= go.gameObject.transform.GetComponentInChildren<TagSky>().gameObject.transform;
            if (playerTransform != null && skyTransform != null)
            {
                AddController(new ParallaxController(skyTransform, playerTransform, new Vector2(0.4f, 1), new Vector2(32, 0)));
                AddController(new ParallaxController(Reference.MainCamera.transform, playerTransform, new Vector2(0f, 1f), new Vector2(0, 0)));
            }
            AddController(new ActivateMazeElementsController(go.gameObject.transform));
        }
    }
}
