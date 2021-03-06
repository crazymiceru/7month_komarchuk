

namespace MobileGame
{
    internal sealed class TrailController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("TrailController");
        private const string _nameRes = "Trail";
 
        internal TrailController()
        {
            var data = CreateGameObject(Reference.ActiveElements, _nameRes);
            var trailM = new ControlM();
            AddController(new AllInputsController(trailM));
            AddController(new PositionTrailTouchController(trailM.positionCursor, trailM.isNewTouch, data.gameObject, Reference.ActiveElements));
        }
    }
}
