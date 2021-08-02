namespace MobileGame
{
    internal sealed class GetKillItemModificationController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("GetKillItemBonuceController");
        private iReadOnlySubscriptionField<(TypeUnit typeUnit, int cfg)> _killTypeItem;
        private IItemsModel<UpgradeItemCfg> _upgardeM;
        private IItemsModel<EffectsItemCfg> _effectsM;

        internal GetKillItemModificationController(iReadOnlySubscriptionField<(TypeUnit typeUnit, int cfg)> killTypeItem, IItemsModel<UpgradeItemCfg> upgardeM, IItemsModel<EffectsItemCfg> effectsM)
        {
            _killTypeItem = killTypeItem;
            _upgardeM = upgardeM;
            _effectsM = effectsM;
            _killTypeItem.Subscribe(Activate);
        }

        private void Activate((TypeUnit typeUnit, int cfg) killTypeItemValue)
        {
            switch (killTypeItemValue.typeUnit)
            {
                case TypeUnit.UpgradeItem:
                    _upgardeM.AddItem(killTypeItemValue.cfg);
                    break;
                case TypeUnit.EffectsItem:
                    _effectsM.AddItem(killTypeItemValue.cfg);
                    break;

                default:
                    break;
            }
        }

        protected override void OnDispose()
        {
            _killTypeItem.UnSubscribe(Activate);
        }
    }
}
