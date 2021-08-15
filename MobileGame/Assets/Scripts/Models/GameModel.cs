using System;

namespace MobileGame
{
    internal sealed class GameModel
    {
        public SubscriptionField<GameState> gameState { get; }
        public SubscriptionField<int> scores { get; }
        public IAnalitics Analitics { get; }
        public IAds ADS { get; }

        public SubscriptionIntPrefs money { get; }
        public SubscriptionIntPrefs wood { get; }

        public SubscriptionField<int> currentLevel;

        #region rewards

        public SubscriptionIntPrefs currentDayReward { get; }
        public SubscriptionDatePrefs DateTimeGetRewards { get; }
        public SubscriptionDatePrefs timeStartRewards { get; }
        public SubscriptionField<int> durationNextReward { get; }
        #endregion


        #region FireBird

        public SubscriptionField<DateTime> dataTimeDataBase { get; }
        public event Action evtUpdateDateTimeBase=delegate { };
        public void UpdateDataTimeDataBase()
        {
            evtUpdateDateTimeBase.Invoke();
        }

        #endregion

        public GameModel(IAds ADS)
        {
            
            gameState = new SubscriptionField<GameState>();
            scores = new SubscriptionField<int>();
            money = new SubscriptionIntPrefs(nameof(money));
            wood = new SubscriptionIntPrefs(nameof(wood));
            currentDayReward = new SubscriptionIntPrefs(nameof(currentDayReward));
            DateTimeGetRewards = new SubscriptionDatePrefs(nameof(DateTimeGetRewards));
            timeStartRewards = new SubscriptionDatePrefs(nameof(timeStartRewards));
            durationNextReward = new SubscriptionField<int> { Value = 300 };
            dataTimeDataBase = new SubscriptionField<DateTime>();
            currentLevel = new SubscriptionField<int> { Value = 0 };
            Analitics = new UnityAnalitics();
            this.ADS = ADS;
        }
    }
}