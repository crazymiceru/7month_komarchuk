using UnityEngine;
using UnityEngine.UI;

namespace MobileGame
{
    internal sealed class ExitController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("ExitController");
        private const string _nameResButton = "ExitGame";
        private Button _button;
        private GameModel _gameModel;

        internal ExitController(GameModel gameModel)
        {
            var data = CreateGameObject(Reference.Canvas, _nameResButton);
            _button = data.gameObject.GetComponent<Button>();
            _button.onClick.AddListener(Activate);
            _gameModel = gameModel;
        }

        private void Activate()
        {
            _gameModel.gameState.Value = GameState.Menu;
        }


        protected override void OnDispose()
        {
            _button?.onClick.RemoveAllListeners();
        }
    }
}
