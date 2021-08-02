using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace MobileGame
{
    internal sealed class GameController : ControllerBasic
    {
        private GameModel _gameModel;
        private UnitModel _playerModel;

        private ControlLeak _controlLeak = new ControlLeak("GameBuild");

        internal GameController(GameModel gameModel, UnitModel playerModel)
        {
            _gameModel = gameModel;
            _playerModel = playerModel;
            _gameModel.gameState.Subscribe(ChangeStateGame);
            _gameModel.gameState.Value = GameState.menu;
        }

        protected override void OnDispose()
        {
            _gameModel.gameState.UnSubscribe(ChangeStateGame);
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
                case GameState.dailyRewards:
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
            Clear();
            AddController(new MenuGlobalController(_gameModel.gameState));
        }

        private void StartGame()
        {
            Transform playerTransform;
            Transform skyTransform;
            Clear();
            _gameModel.Analitics.SendMessage("Start Game");
            var playerController = new PlayerBuild().Create(_gameModel, _playerModel);
            var _playerView = playerController[0].iUnitView;
            AddController(playerController);
            playerTransform = playerController[0].gameObject.transform;
            var sceneController = new SceneController(1);
            AddController(sceneController);
            var dataScene = sceneController[0];
            skyTransform = dataScene.gameObject.transform.GetComponentInChildren<TagSky>().gameObject.transform;
            if (playerTransform != null && skyTransform != null)
            {
                AddController(new ParallaxController(skyTransform, playerTransform, LoadResources.GetValue<ParalaxCfg>("Any/ParalaxBackground")));
                AddController(new ParallaxController(Reference.MainCamera.transform, playerTransform, LoadResources.GetValue<ParalaxCfg>("Any/ParalaxCamera")));
            }
            AddController(new ActivateMazeElementsController(dataScene.gameObject.transform, _playerModel, _playerView));
        }
    }
}
