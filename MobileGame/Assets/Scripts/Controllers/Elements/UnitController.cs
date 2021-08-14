using UnityEngine;

namespace MobileGame
{
    internal sealed class UnitController : ControllerBasic, IInitialization
    {
        #region Fields

        private const int _shieldItemID = 2;
        ControlLeak _controlLeak = new ControlLeak("UnitController");
        private UnitModel _unit;
        private DataUnit _unitData;
        private IInteractive _iInteractive;
        private GameObject _gameObject;
        private SubscriptionField<int> _scores;
        private EffectsModel _effectsModel;

        #endregion


        #region Init

        internal UnitController(UnitModel unit, EffectsModel effectsModel, SubscriptionField<int> scores, IInteractive iInteractive, TypeUnit typeItem, DataUnit unitData)
        {
            _unit = unit;
            _scores = scores;
            _iInteractive = iInteractive;
            _unitData = unitData;
            _effectsModel = effectsModel;
            _unit.packInteractiveData.Value = new PackInteractiveData(_unitData.AttackPower, typeItem);
            _gameObject = (_iInteractive as MonoBehaviour).gameObject;
            _unit.maxSpeed.Value = unitData.MaxSpeed;
            _unit.powerJump.Value = unitData.PowerJump;
            _iInteractive.evtAttack += Attacked;
            _iInteractive.evtCollision += OutInteractive;
            _unit.evtDecLives += DecLive;
        }

        protected override void OnDispose()
        {
            //_iInteractive.evtAttack -= Attacked;
            //_iInteractive.evtCollision -= OutInteractive;
            _unit.evtDecLives -= DecLive;
        }

        public void Initialization()
        {
            _unit.HP = _unitData.MaxLive;
            _scores.Value = 0;
        }

        #endregion


        #region Game

        private void DecLive()
        {
            if (_unitData.DestroyEffects != null)
            {
                var go = GameObject.Instantiate(_unitData.DestroyEffects, _gameObject.transform.position, Quaternion.identity);
                go.transform.SetParent(Reference.ActiveElements);
                GameObject.Destroy(go, _unitData.TimeViewDestroyEffects);
            }
        }


        private (int, bool) Attacked(PackInteractiveData pack)
        {
            int addScores = 0;
            bool isDead = false;
            if (_unit.HP != 0 && !_unit.isShielded.Value)
            {
                _unit.HP -= pack.attackPower;
                if (pack.attackPower > 0) _effectsModel.AddItem(_shieldItemID);
                if (_unit.HP == 0)
                {
                    addScores = _unitData.AddScores;
                    isDead = true;
                }
            }
            return (addScores, isDead);
        }

        private void OutInteractive(IInteractive ui, bool isEnter)
        {
            if (isEnter)
            {
                //Debug.Log($"Attack {_gameObject.name} on {(ui as MonoBehaviour).name}");
                var result = ui.Attack(_unit.packInteractiveData.Value);
                if (result.isDead)
                {
                    if (ui is IUnitView)
                    {
                        var uv = ui as IUnitView;
                        var type = uv.GetTypeItem();
                        _unit.killTypeItem.Value = type;
                    }
                }
                _scores.Value += result.scores;
            }
        }

        #endregion
    }
}
