using UnityEngine;
using UnityEngine.UI;

namespace MobileGame
{
    internal sealed class MenuController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("MenuController");
        private SubscriptionField<GameState> _gameState;
        private GameObject _menu;
        private Button _button;

        internal MenuController(SubscriptionField<GameState> gameState, GameObject menu)
        {
            _gameState = gameState;

            _menu = menu;
            var goStartGame=menu.transform.GetComponentInChildren<TagButtonStartGame>();
            if (goStartGame.TryGetComponent<Button>(out Button button))
            {
                _button = button;
                _button.onClick.AddListener(StartGame);
            }
        }

        private void StartGame()
        {
            _gameState.Value = GameState.startLevel;
            Object.Destroy(_menu);
        }

        protected override void OnDispose()
        {
            _button?.onClick.RemoveAllListeners();
        }
    }
}