using UnityEngine;

namespace MobileGame
{
    internal sealed class UnitController : ControllerBasic, IInitialization
    {
        ControlLeak _controlLeak = new ControlLeak("UnitController");
        private UnitM _unit;
        private DataUnit _unitData;
        private IInteractive _iInteractive;
        private GameObject _gameObject;
        private SubscriptionField<int> _scores;

        #region Init

        internal UnitController(UnitM unit,SubscriptionField<int> scores , IInteractive iInteractive, TypeItem typeItem, DataUnit unitData)
        {
            _unit = unit;
            _scores = scores;
            _iInteractive = iInteractive;
            _unitData = unitData;
            _unit.packInteractiveData.Value = new PackInteractiveData(_unitData.AttackPower, typeItem);
            _iInteractive.evtAttack += Attack;
            _iInteractive.evtCollision += OutInteractive;
            _unit.evtDecLives += DecLive;
            _gameObject = (_iInteractive as MonoBehaviour).gameObject;

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


        private (int, bool) Attack(PackInteractiveData pack)
        {
            int addScores = 0;
            bool isDead = false;
            if (_unit.HP != 0)
            {
                _unit.HP -= pack.attackPower;
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
                var scores=ui.Attack(_unit.packInteractiveData.Value).scores;
                _scores.Value += scores;
            }
        }

        #endregion
    }
}
