namespace MobileGame
{
    internal sealed class GameM
    {
        public SubscriptionField<GameState> gameState { get; }
        public SubscriptionField<int> scores { get; }

        public GameM()
        {
            gameState = new SubscriptionField<GameState>();
            scores = new SubscriptionField<int>();
        }
    }
}