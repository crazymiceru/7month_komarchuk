using System;
using System.Collections.Generic;
using UnityEngine;

namespace MobileGame
{
    public class UnitView : MonoBehaviour, IInteractive, IUnitView, IPool
    {

        #region Variables

        public event Action<Collider2D, bool> evtTrigger = delegate { };
        public event Action<IInteractive, bool> evtCollision = delegate { };
        private List<Func<PackInteractiveData, (int,bool)>> _evtAttack = new List<Func<PackInteractiveData, (int,bool)>>();
        public event Action<bool> evtAnyCollision = delegate { };

        public Transform objectTransform => _objectTransform;
        private Transform _objectTransform;
        public Rigidbody2D objectRigidbody2D => _objectRigidbody2D;
        private Rigidbody2D _objectRigidbody2D;
        public SpriteRenderer objectSpriteRednderer => _objectSpriteRednderer;
        private SpriteRenderer _objectSpriteRednderer;

        [SerializeField] private TypeItem _typeItem;
        [SerializeField] private int _numCfg = 0;
        private PoolInstatiate _poolInstatiate;
        private bool _isPool = false;

        private Dictionary<int, int> _listCollisionEnter = new Dictionary<int, int>();

        event Func<PackInteractiveData, (int, bool)> IInteractive.evtAttack
        {
            add=> _evtAttack.Add(value);
            remove=> _evtAttack.Remove(value);
        }

        #endregion


        #region Init

        public void ClearEvt()
        {
            evtTrigger = delegate { };
            evtCollision= delegate { };
            _evtAttack.Clear();
            evtAnyCollision = delegate { };            
        }

        private void Awake()
        {
            GetComponents();
        }

        private void GetComponents()
        {
            _objectTransform = transform;
            _objectRigidbody2D = GetComponent<Rigidbody2D>();
            if (_objectRigidbody2D == null) Debug.LogWarning($"does not find the Rigidbody2D on the {gameObject.name} object ");
            _objectSpriteRednderer = GetComponent<SpriteRenderer>();
            if (_objectSpriteRednderer == null) Debug.LogWarning($"does not find the SpriteRenderer on the {gameObject.name} object ");
        }

        #endregion


        #region Utils

        public (TypeItem type, int cfg) GetTypeItem()
        {
            return (_typeItem, _numCfg);
        }

        public void SetTypeItem(TypeItem type = TypeItem.Any, int cfg = -1)
        {
            if (cfg == -1) cfg = _numCfg;
            if (type == TypeItem.Any) type = _typeItem;
            _typeItem = type; _numCfg = cfg;
        }

        public void SetPoolDestroy()
        {
            _isPool = true;            
        }

        void IInteractive.Kill()
        {
            if (_isPool) PoolInstatiate.DestroyGameObject(gameObject);
            else Destroy(gameObject);
        }

        #endregion


        #region Collision

        private void OnCollisionEnter2D(Collision2D collision)
        {
                evtAnyCollision.Invoke(true);
                var ID = collision.gameObject.GetInstanceID();
                _listCollisionEnter[ID] = _listCollisionEnter.ContainsKey(ID) ? _listCollisionEnter[ID] + 1 : 1;

                if (_listCollisionEnter[ID] == 1 && collision.gameObject.TryGetComponent<IInteractive>(out IInteractive unitInteractive))
                {
                evtCollision.Invoke(unitInteractive, true);
                }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            evtAnyCollision.Invoke(false);
            var ID = collision.gameObject.GetInstanceID();
            _listCollisionEnter[ID] = _listCollisionEnter.ContainsKey(ID) ? _listCollisionEnter[ID] - 1 : 0;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {                           
                evtTrigger.Invoke(other,true);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
                evtTrigger.Invoke(other,false);
        }

        #endregion


        public (int,bool) Attack(PackInteractiveData data)
        {
            int addScores = 0;
            bool isDead=false;
            foreach (var item in _evtAttack)
            {
                (var addScoresTmp, var isDeadTmp) = item(data);
                addScores += addScoresTmp;
                if (isDeadTmp) isDead = true;
            }
            return (addScores,isDead);
        }
    }
}