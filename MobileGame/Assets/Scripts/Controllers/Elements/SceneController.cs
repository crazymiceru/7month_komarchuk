namespace MobileGame
{
    internal sealed class SceneController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("SceneBuild");
        private const string _nameRes = "GameScene";

        internal SceneController() : base()
        {
            CreateGameObject(Reference.ActiveElements, _nameRes);
        }
    }
}
