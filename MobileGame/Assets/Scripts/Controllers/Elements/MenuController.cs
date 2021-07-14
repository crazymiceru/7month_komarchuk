using UnityEngine;
using UnityEngine.UI;

namespace MobileGame
{
    internal sealed class MenuController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("MenuController");
        private SubscriptionField<GameState> _gameState;
        private GameObject _menu;

        internal MenuController(SubscriptionField<GameState> gameState, GameObject menu)
        {
            _gameState = gameState;

            _menu = menu;
            var goStartGame=menu.transform.GetComponentInChildren<TagButtonStartGame>();
            if (goStartGame.TryGetComponent<Button>(out Button button))
            {
                button.onClick.AddListener(StartGame);
            }
        }

        private void StartGame()
        {
            _gameState.Value = GameState.startGame;
            Object.Destroy(_menu);
        }
    }
}