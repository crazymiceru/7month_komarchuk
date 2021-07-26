namespace MobileGame
{
    internal sealed class EmptyUnitController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("");
        private UnitM _unit;
        private IUnitView _iUnitView;

        internal EmptyUnitController(UnitM unit, IUnitView iUnitView)
        {
            _unit = unit;
            _iUnitView = iUnitView;
        }
    }
}
