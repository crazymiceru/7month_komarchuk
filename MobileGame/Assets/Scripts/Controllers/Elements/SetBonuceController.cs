using UnityEngine;
using UnityEngine.UI;

namespace MobileGame
{
    internal sealed class SetBonuceController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("SetBonuceBuild");
        private const string _nameResButton = "BonusButton";
        private const string _nameResShow = "BonusShow";
        private Button _button;
        private GameModel _gameM;

        internal SetBonuceController(GameModel gameM)
        {
            var data = CreateGameObject(Reference.Canvas, _nameResButton);
            _button = data.gameObject.GetComponent<Button>();
            _button.onClick.AddListener(Activate);
            _gameM = gameM;
        }

        protected override void OnDispose()
        {
            _button?.onClick.RemoveAllListeners();
        }

        private void Activate()
        {
            _button.gameObject.SetActive(false);
            _gameM.ADS.ShowVideoReward(EndVideo);
        }

        private void EndVideo(VideoResult videoResult)
        {
            if (videoResult == VideoResult.Finished)
            {
                _button?.onClick.RemoveAllListeners();
                Clear();
                CreateGameObject(Reference.Canvas, _nameResShow);
            }
            else
            {
                _button.gameObject.SetActive(true);
            }
        }
    }
}
