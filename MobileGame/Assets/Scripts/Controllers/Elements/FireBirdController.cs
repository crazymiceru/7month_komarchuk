using Firebase;
using Firebase.Database;
using System;
using System.Collections;
using UnityEngine;

namespace MobileGame
{
    internal sealed class FireBirdController : ControllerBasic,IInitialization
    {
        private const int durationNextUpdateTime = 15;

        private ControlLeak _controlLeak = new ControlLeak("FireBirdController");
        private FirebaseDatabase _database;
        private GameModel _gameModel;

        internal FireBirdController(GameModel gameModel)
        {
            _gameModel = gameModel;

            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    _database = FirebaseDatabase.DefaultInstance;
                    Debug.Log($"FireBird OK");                    
                    _database.GetReference("Prefs").ValueChanged += Get;
                    _gameModel.evtUpdateDateTimeBase += UpdateDateTimeBase;                    
                }
                else
                {
                    Debug.LogError(System.String.Format(
                      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                }
            });            
        }

        private void UpdateDateTimeBase()
        {
            _database.GetReference("Prefs").Child("Time").SetValueAsync(ServerValue.Timestamp);
        }

        private void Get(object sender, ValueChangedEventArgs e)
        {
            if (e.DatabaseError!=null)
            {
                Debug.Log($"Database Error: {e.DatabaseError.Message}");
                return;
            }
            foreach (var item in e.Snapshot.Children)
            {
                if (item.Key.Equals("Time"))
                {                    
                    long count = long.Parse(item.Value.ToString());
                    DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(count);                    
                    _gameModel.dataTimeDataBase.Value = dateTimeOffset.DateTime;
                    //Debug.Log($"DateTimeDataBase: {_gameModel.dateTimeDataBase.Value}");
                }
            }
            
        }

        IEnumerator MakeUpdateDateTimeDataBase()
        {
            while (true)
            {
                yield return new WaitForSeconds(durationNextUpdateTime);
                _gameModel.UpdateDataTimeDataBase();
            }
        }

        public void Initialization()
        {
            Debug.Log($"Init FireBirdController");
            CoroutinesView.inst.AddCoroutine(MakeUpdateDateTimeDataBase());
        }

        protected override void OnDispose()
        {
            CoroutinesView.inst.RemoveCoroutine(MakeUpdateDateTimeDataBase());
        }
    }
}
