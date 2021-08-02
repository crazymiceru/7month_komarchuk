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

        #region rewards

        public SubscriptionIntPrefs currentDayReward { get; }
        public SubscriptionDatePrefs timeGetRewards { get; }
        public SubscriptionDatePrefs timeStartRewards { get; }

        #endregion


        #region FireBird

        public SubscriptionField<DateTime> dateTimeDataBase { get; }
        public event Action evtUpdateDateTimeBase=delegate { };
        public void UpdateDateTimeDataBase()
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
            timeGetRewards = new SubscriptionDatePrefs(nameof(timeGetRewards));
            timeStartRewards = new SubscriptionDatePrefs(nameof(timeStartRewards));
            dateTimeDataBase = new SubscriptionField<DateTime>();
            Analitics = new UnityAnalitics();
            this.ADS = ADS;
        }
    }
}