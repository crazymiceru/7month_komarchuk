

namespace MobileGame
{
    internal sealed class TrailController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("TrailBuild");
        private const string _nameRes = "Trail";
 
        internal TrailController() : base()
        {
            var data = CreateGameObject(Reference.ActiveElements, _nameRes);
            var trailM = new ControlM();
            AddController(new TouchController(trailM));
            AddController(new PositionTrailTouchController(trailM.positionTouch, trailM.isNewTouch, data.gameObject, Reference.ActiveElements));
        }
    }
}
