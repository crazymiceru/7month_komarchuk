using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace MobileGame
{

    public abstract class ControllerBasic : IDisposable, IController
    {
        #region Fields

        protected List<GameObjectData> _gameObjects = new List<GameObjectData>();
        protected int _numCfg = 0;
        protected List<IController> _iControllers = new List<IController>();
        protected List<string> DataNames = new List<string>();
        private bool isDispose;
        internal Action<GameObjectData> evtAddressableCompleted;


        #endregion


        #region Util

        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                sb.Append($"{_gameObjects[i].gameObject.name};");
            }
            return $"Objects:{sb}";
        }

        public ControllerBasic SetNumCfg(int numCfg)
        {
            _numCfg = numCfg;
            AddDataName();
            return this;
        }

        protected IController AddController(IController controller)
        {
            _iControllers.Add(controller);
            ListControllers.Add(controller);
            return controller;
        }

        private void GetInfoGameObject(GameObjectData data)
        {
            if (data.gameObject != null)
            {
                if (data.gameObject.TryGetComponent(out IInteractive iInteractive))
                {
                    data.iInteractive = iInteractive;
                }
                if (data.gameObject.TryGetComponent(out IUnitView iUnitView))
                {
                    data.iUnitView = iUnitView;
                }
            }
        }

        protected virtual void AddDataName()
        {
            DataNames.Clear();
        }

        public IEnumerable<string> GetDataName()
        {
            for (int i = 0; i < DataNames.Count; i++)
            {
                yield return DataNames[i];
            }
        }

        public GameObjectData this[int index]
        {
            get => _gameObjects[index];
        }

        public void Dispose()
        {
            OnDispose();
            Clear();
        }

        protected void Clear()
        {
            for (int i = 0; i < _iControllers.Count; i++)
            {
                ListControllers.Delete(_iControllers[i]);
                if (_iControllers[i] is IDisposable disposeController)
                    disposeController.Dispose();
            }
            _iControllers.Clear();

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                if (!_gameObjects[i].isAddressable) Object.Destroy(_gameObjects[i].gameObject);
                else Addressables.ReleaseInstance(_gameObjects[i].gameObject);
            }
            _gameObjects.Clear();
        }

        protected virtual void OnDispose()
        {
        }

        #endregion


        #region Builds

        private GameObjectData CreateUnitBasic(string nameRes)
        {
            var data = new GameObjectData();
            nameRes = nameRes.Replace("##", $"{_numCfg}");
            data.prefabGameObject = LoadResources.GetValue<GameObject>(nameRes);
            return data;
        }
        
        private GameObjectData CreateUnitBasicBundle(string bandle,string key)
        {
            var data = new GameObjectData();
            key = key.Replace("##", $"{_numCfg}");
            data.prefabGameObject = LoadBundles.GetValue<GameObject>(bandle,key);
            return data;
        }


        protected GameObjectData CreateGameObjectPool(Transform folder, string nameRes)
        {
            var data = CreateUnitBasic(nameRes);
            data.gameObject = PoolInstatiate.Instantiate(data.prefabGameObject);
            _gameObjects.Add(data);
            SetFolder(folder, data);
            GetInfoGameObject(data);
            return data;
        }

        protected GameObjectData CreateGameObject(Transform folder, string nameRes)
        {
            var data = CreateUnitBasic(nameRes);
            data.gameObject = GameObject.Instantiate(data.prefabGameObject, folder);
            GetInfoGameObject(data);
            _gameObjects.Add(data);
            return data;
        }
        protected GameObjectData CreateGameObjectBandle(Transform folder, string bandle, string key)
        {
            var data = CreateUnitBasicBundle(bandle,key);
            data.gameObject = GameObject.Instantiate(data.prefabGameObject, folder);
            GetInfoGameObject(data);
            _gameObjects.Add(data);
            return data;
        }

        protected void CreateGameObjectAddressable(Transform folder, string key)
        {
            key = key.Replace("##", $"{_numCfg}");
            var addressable = Addressables.InstantiateAsync(key, folder);
            addressable.Completed += CreateGameObjectAddressableCompleted;
        }

        private void CreateGameObjectAddressableCompleted(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
        {
            var data = new GameObjectData();
            data.isAddressable = true;
            data.gameObject = obj.Result;
            GetInfoGameObject(data);
            _gameObjects.Add(data);
            evtAddressableCompleted?.Invoke(data);
        }

        public ControllerBasic SetGameObject(GameObject gameObject)
        {
            var data = new GameObjectData();
            data.gameObject = gameObject;
            GetInfoGameObject(data);
            _gameObjects.Add(data);
            return this;
        }

        private void SetFolder(Transform folder, GameObjectData data)
        {
            data.gameObject.transform.SetParent(folder);
            data.gameObject.transform.localPosition = data.prefabGameObject.transform.position;
            data.gameObject.transform.localRotation = data.prefabGameObject.transform.rotation;
        }

        internal ControllerBasic SetPosition(Vector2 position, Quaternion rotation)
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].gameObject.transform.position = position;
                _gameObjects[i].gameObject.transform.rotation = rotation;

            }
            return this;
        }

        internal virtual ControllerBasic CreateControllers()
        {
            return this;
        }

        #endregion

    }
}
