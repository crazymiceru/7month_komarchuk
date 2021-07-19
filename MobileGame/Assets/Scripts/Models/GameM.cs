namespace MobileGame
{
    internal sealed class GameM
    {
        public SubscriptionField<GameState> gameState { get; }
        public SubscriptionField<int> scores { get; }
        public IAnalitics Analitics { get; }
        public IAds ADS { get; }

        public GameM(IAds ADS)
        {
            gameState = new SubscriptionField<GameState>();
            scores = new SubscriptionField<int>();
            Analitics = new UnityAnalitics();
            this.ADS = ADS;
        }
    }
}