using UnityEngine;
using TMPro;

namespace MobileGame
{
    internal sealed class ScoresCanvasController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("ScoresCanvasController");
        private TextMeshProUGUI textScores;
        private iReadOnlySubscriptionField<int> _scores;

        internal ScoresCanvasController(iReadOnlySubscriptionField<int> scores)
        {
            _scores = scores;
            var goTextScores = GameObject.FindObjectOfType<TagCanvasScores>();
            if (goTextScores == null) Debug.LogWarning($"Dont find the TagCanvasScores");
            textScores = goTextScores.GetComponent<TextMeshProUGUI>();
            _scores.Subscribe(UpdateScores);
        }

        private void UpdateScores(int scores)
        {
            textScores.text = scores.ToString();
        }

        protected override void OnDispose()
        {
            _scores.UnSubscribe(UpdateScores);
        }
    }
}