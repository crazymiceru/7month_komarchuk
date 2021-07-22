namespace MobileGame
{
    internal sealed class GetKillItemBonuceController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("GetKillItemBonuceController");
        private iReadOnlySubscriptionField<(TypeUnit typeUnit, int cfg)> _killTypeItem;
        private IUpgardeM _upgardeM;

        internal GetKillItemBonuceController(iReadOnlySubscriptionField<(TypeUnit typeUnit, int cfg)> killTypeItem, IUpgardeM upgardeM)
        {
            _killTypeItem = killTypeItem;
            _upgardeM = upgardeM;
            _killTypeItem.Subscribe(Activate);
        }

        private void Activate((TypeUnit typeUnit, int cfg) killTypeItemValue)
        {
            switch (killTypeItemValue.typeUnit)
            {
                case TypeUnit.UpgradeItem:
                    _upgardeM.AddItem(killTypeItemValue.cfg);
                    break;
                default:
                    break;
            }
        }

    }
}
