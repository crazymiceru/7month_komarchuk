namespace MobileGame
{
    internal sealed class MenuGlobalController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("MenuBuild");
        private const string _nameRes = "Menu";

        internal MenuGlobalController(SubscriptionField<GameState> gameState) : base()
        {
            var data = CreateGameObject(Reference.Canvas, _nameRes);
            AddController(new MenuController(gameState, data.gameObject));
            AddController(new TrailController());
        }
    }
}
