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
                case GameState.startGame:
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
            Clear();
            _gameM.Analitics.SendMessage("Start Game", "Test");
            AddController(new SceneController());
            AddController(new PlayerBuild().Create(_gameM));
            AddController(new SetBonuceController(_gameM));
        }
    }
}
