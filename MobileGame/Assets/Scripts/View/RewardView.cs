using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

namespace MobileGame
{
    public class RewardView : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image image;
        [SerializeField] private GameObject active;
        [SerializeField] private GameObject off;
        [SerializeField] private TMP_Text count;

        public void Initialisation(DailyRewardCfg dailyRewardCfg, DailyRewardsVisualCfg dailyRewardsVisualCfg, bool isEnable, bool isActive, UnityAction buttonAction = null)
        {
            count.text = dailyRewardCfg.Value.ToString();
            image.sprite = dailyRewardsVisualCfg.Sprite[(int)dailyRewardCfg.TypeReward];
            InitialisationUpdate(isEnable, isActive, buttonAction);
        }

        public void InitialisationUpdate(bool isEnable, bool isActive, UnityAction buttonAction = null)
        {
            active.SetActive(isActive);
            off.SetActive(!isEnable);
            button.onClick.RemoveAllListeners();
            if (buttonAction != null)
            {
                button.enabled = true;
                button.onClick.AddListener(buttonAction);
            }
            else button.enabled = false;
        }


        private void OnDestroy()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}