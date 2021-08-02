using UnityEngine;

namespace MobileGame
{
    [CreateAssetMenu(menuName = "My/DataUnit",fileName ="Unit_Data 0")]
    public sealed class DataUnit : ScriptableObject
    {
        [Header("Move")]
        [SerializeField] private float _powerMove = 500f;
        public float PowerMove => _powerMove;

        //public float _powerMove = 500f;

        [SerializeField] private float _powerJump = 300f;
        public float PowerJump => _powerJump;

        [SerializeField] private float _minSqrLenghthTraectory = 0.2f;
        public float MinSqrLenghthTraectory => _minSqrLenghthTraectory;

        [Header("Limits")]
        [SerializeField] private float _maxSpeed = 10;
        public float MaxSpeed => _maxSpeed;

        [Header("Live")]
        [SerializeField] private int _maxLive=1;
        public int MaxLive => _maxLive;

        [SerializeField] private GameObject _destroyEffects;
        public GameObject DestroyEffects => _destroyEffects;

        [SerializeField] private float _timeViewDestroyEffects=10;
        public float TimeViewDestroyEffects => _timeViewDestroyEffects;

        [Header("Attack")]
        [SerializeField] private int _attackPower=1;
        public int AttackPower => _attackPower;

        [SerializeField] private int _addScores=0;
        public int AddScores => _addScores;

        
    }
}
