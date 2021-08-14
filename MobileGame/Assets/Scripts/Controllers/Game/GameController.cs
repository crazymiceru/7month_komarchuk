using System;
using UnityEngine;

namespace MobileGame
{
    internal sealed class GameController : ControllerBasic
    {
        private GameModel _gameModel;
        private UnitModel _playerModel;
        private Vector3 _startCameraPosition;

        private ControlLeak _controlLeak = new ControlLeak("GameBuild");

        internal GameController(GameModel gameModel, UnitModel playerModel)
        {
            _gameModel = gameModel;
            _playerModel = playerModel;
            _gameModel.gameState.Subscribe(ChangeStateGame);
            _gameModel.gameState.Value = GameState.Menu;
            _startCameraPosition = Reference.MainCamera.transform.position;
        }

        protected override void OnDispose()
        {
            _gameModel.gameState.UnSubscribe(ChangeStateGame);
        }

        private void ChangeStateGame(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Menu:
                    Menu();
                    break;
                case GameState.StartLevel:
                    StartGame();
                    break;
                case GameState.GameOver:
                    break;
                case GameState.WinGame:
                    break;
                case GameState.DailyRewards:
                    DailyRewards();
                    break;
                default:
                    break;
            }
        }

        private void DailyRewards()
        {
            Clear();
            AddController(new DailyRewardsController(_gameModel));
        }

        private void Menu()
        {
            LoadBundles.AddBundle("test");
            Clear();
            AddController(new MenuGlobalController(_gameModel.gameState));
        }

        private void StartGame()
        {
            Transform playerTransform;
            Transform skyTransform;
            ControllerBasic sceneController;
            ControllerBasic playerController;

            Clear();
            Reference.MainCamera.transform.position = _startCameraPosition;

            _gameModel.Analitics.SendMessage("Start Game");

            AddController(sceneController = new SceneController(_gameModel.currentLevel.Value));
            sceneController.evtAddressableCompleted += LoadSceneCompleted;

            void LoadSceneCompleted(GameObjectData dataScene)
            {
                AddController(playerController = new PlayerBuild().Create(_gameModel, _playerModel));
                playerTransform = playerController[0].gameObject.transform;
                var _playerView = playerController[0].iUnitView;

                AddController(new ActivateMazeElementsController(dataScene.gameObject.transform, _playerModel, _playerView));

                skyTransform = dataScene.gameObject.transform.GetComponentInChildren<TagSky>().transform;
                AddController(new AllParallaxController(playerTransform, skyTransform));

                AddController(new ExitController(_gameModel));
            }
        }
    }
}
