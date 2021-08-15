using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.SmartFormat.Extensions;
using UnityEngine.Localization.SmartFormat.GlobalVariables;

namespace MobileGame
{
    internal sealed class DailyRewardsController : ControllerBasic
    {       
        private ControlLeak _controlLeak = new ControlLeak("DailyRewardsController");
        private const string _nameResDailyRewards = "Items/DailyRewards/DailyRewards";
        private const string _nameResReward = "Items/DailyRewards/Reward";
        private const string _nameResDailyRewardsArrayCfg = "Items/DailyRewards/DailyRewardsArray";
        private const string _nameResDailyRewardsVisualCfg = "Items/DailyRewards/DailyRewardsVisual";
        private ItemsArray _dailyRewardsArray;
        private GameModel _gameModel;
        private List<RewardView> _rewardsViews = new List<RewardView>();
        private DailyRewardsVisualCfg _dailyRewardsVisual;
        private Transform _folderElements;
        private TMP_Text _timeNextUpdateText;
        private Button _buttonClose;
        private IntGlobalVariable _localizationHours;
        private IntGlobalVariable _localizationMinutes;

        internal DailyRewardsController(GameModel gameModel)
        {

            _gameModel = gameModel;
            Debug.Log($"Amount of money:{_gameModel.money.Value}");
            Debug.Log($"Amount of wood:{_gameModel.wood.Value}");

            var dataRoot = CreateGameObject(Reference.Canvas, _nameResDailyRewards);

            FindElements(dataRoot);

            _dailyRewardsArray = LoadResources.GetValue<ItemsArray>(_nameResDailyRewardsArrayCfg);
            _dailyRewardsVisual = LoadResources.GetValue<DailyRewardsVisualCfg>(_nameResDailyRewardsVisualCfg);
            _gameModel.dataTimeDataBase.Subscribe(GetDateTimeBase);
            _gameModel.UpdateDataTimeDataBase();

            CreateLocalisationSettings();

            void CreateLocalisationSettings()
            {
                var source = LocalizationSettings
                    .StringDatabase
                    .SmartFormatter
                    .GetSourceExtension<GlobalVariablesSource>();

                _localizationHours = source["global"]["Hours"] as IntGlobalVariable;                
                _localizationMinutes = source["global"]["Minutes"] as IntGlobalVariable;
            }

            void FindElements(GameObjectData dataRoot)
            {
                _folderElements = dataRoot.gameObject.transform.
                    GetComponentInChildren<TagGreed>().transform;

                var closeElement = dataRoot.gameObject.transform.GetComponentInChildren<TagButtonClose>();
                if (closeElement != null && closeElement.TryGetComponent<Button>(out Button buttonClose))
                {
                    buttonClose.onClick.AddListener(Close);
                    _buttonClose = buttonClose;
                }
                else Debug.LogWarning($"The 'Close' button cannot be created");

                var TimeNextUpdateElement = dataRoot.gameObject.transform.GetComponentInChildren<TagTimeNextUpdate>(); ;
                if (TimeNextUpdateElement != null &&
                    TimeNextUpdateElement.TryGetComponent(out TMP_Text tMP_Text))
                {
                    _timeNextUpdateText = tMP_Text;
                    _timeNextUpdateText.enabled = false;
                }
            }
        }

        private void MakeRewardsView(DateTime dateTimeDataBase)
        {
            for (int i = 0; i < _dailyRewardsArray.ItemCfg.Count; i++)
            {
                var data = CreateGameObject(_folderElements, _nameResReward);
                data.gameObject.name = $"Reward:{i}";
                if (data.gameObject.TryGetComponent(out RewardView rewardView))
                {
                    var dailyRewardCfg = _dailyRewardsArray.ItemCfg[i] as DailyRewardCfg;
                    _rewardsViews.Add(rewardView);
                    rewardView.Initialisation(dailyRewardCfg, _dailyRewardsVisual, false, false);
                }
            }
            UpdateRewardsView(dateTimeDataBase);
            _gameModel.dataTimeDataBase.Subscribe(UpdateRewardsView);
        }
        private void UpdateRewardsView(DateTime dateTimeDataBase)
        {
            if ((dateTimeDataBase - _gameModel.timeStartRewards.Value).TotalSeconds > (_dailyRewardsArray.ItemCfg.Count - 1) * _gameModel.durationNextReward.Value)
                Restart();

            var differenceTime = (_gameModel.DateTimeGetRewards.Value + TimeSpan.FromSeconds(_gameModel.durationNextReward.Value))- dateTimeDataBase;
            
            //var tmpTimeNextUpdateText = $"Until the next award is left: {differenceTime.Hours} Hours { (differenceTime.Minutes > 0 ? differenceTime.Minutes : 1)} Minutes";
            _timeNextUpdateText.enabled = true;
            _localizationHours.Value = differenceTime.Hours;
            _localizationMinutes.Value = differenceTime.Minutes > 0 ? differenceTime.Minutes : 1;

            for (int i = 0; i < _dailyRewardsArray.ItemCfg.Count; i++)
            {
                if (_dailyRewardsArray.ItemCfg[i] is DailyRewardCfg dailyRewardCfg)
                {
                    if (i == _gameModel.currentDayReward.Value
                        && (dateTimeDataBase - _gameModel.DateTimeGetRewards.Value).TotalSeconds >= _gameModel.durationNextReward.Value)
                    {
                        _rewardsViews[i].InitialisationUpdate(true, true, () => Click(dailyRewardCfg.TypeReward, dailyRewardCfg.Value));
                        
                        _timeNextUpdateText.enabled = false;
                    }
                    else if (i < _gameModel.currentDayReward.Value)
                        _rewardsViews[i].InitialisationUpdate(false, false);
                    else _rewardsViews[i].InitialisationUpdate(true, false);
                }
            }
        }

        private void GetDateTimeBase(DateTime dateTimeDataBase)
        {
            _gameModel.dataTimeDataBase.UnSubscribe(GetDateTimeBase);
            MakeRewardsView(dateTimeDataBase);
        }

        private void Close() => _gameModel.gameState.Value = GameState.StartLevel;

        private void Click(TypeReward typeReward, int countReward)
        {
            Debug.Log($"Click typereward:{typeReward} count:{countReward}");
            _gameModel.DateTimeGetRewards.Value = _gameModel.dataTimeDataBase.Value;
            _gameModel.currentDayReward.Value++;

            switch (typeReward)
            {
                case TypeReward.Money:
                    _gameModel.money.Value += countReward;
                    break;
                case TypeReward.Wood:
                    _gameModel.wood.Value += countReward;
                    break;
                default:
                    break;
            }

            UpdateRewardsView(_gameModel.dataTimeDataBase.Value);
        }

        private void Restart()
        {
            Debug.Log($"Restart Reward");
            _gameModel.DateTimeGetRewards.Value = DateTime.UtcNow.AddDays(-1);
            _gameModel.timeStartRewards.Value = DateTime.UtcNow;
            _gameModel.currentDayReward.Value = 0;
        }

        protected override void OnDispose()
        {
            _rewardsViews.Clear();
            _gameModel.dataTimeDataBase.UnSubscribe(UpdateRewardsView);
            _gameModel.dataTimeDataBase.UnSubscribe(GetDateTimeBase);
            _buttonClose.onClick.RemoveAllListeners();
        }
    }
}
