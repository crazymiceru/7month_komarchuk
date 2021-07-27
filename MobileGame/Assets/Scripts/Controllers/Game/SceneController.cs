namespace MobileGame
{
    internal sealed class SceneController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("SceneBuild");
        private const string _nameRes = "Levels/Level ##";

        internal SceneController(int numCfg) : base()
        {
            _numCfg = numCfg;
            CreateGameObject(Reference.ActiveElements, _nameRes);
        }
    }
}
