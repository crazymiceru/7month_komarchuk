using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MobileGame
{

    public abstract class ControllerBasic : IDisposable, IController
    {
        #region Fields

        protected List<GameObjectData> _gameObjects=new List<GameObjectData>();
        protected int _numCfg = 0;        
        protected List<IController> _iControllers;
        protected List<string> DataNames = new List<string>();
        private bool isDispose;

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

        internal ControllerBasic()
        {
            _iControllers = new List<IController>();
            AddDataName();
        }

        public ControllerBasic SetNumCfg(int numCfg)
        {
            _numCfg = numCfg;
            AddDataName();
            return this;
        }

        private GameObjectData CreateUnitBasic(string nameRes)
        {
            var data = new GameObjectData();
            nameRes = nameRes.Replace("##", $"{_numCfg}");
            data.prefabGameObject = LoadResources.GetValue<GameObject>($"{nameRes}");
            return data;
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

        #endregion


        #region Builds

        protected GameObjectData CreateGameObjectPool(Transform folder, string nameRes)
        {
            var data= CreateUnitBasic(nameRes);
            data.gameObject = PoolInstatiate.Instantiate(data.prefabGameObject);
            _gameObjects.Add(data);
            SetFolder(folder,data);
            GetInfoGameObject(data);
            return data;
        }

        protected GameObjectData CreateGameObject(Transform folder, string nameRes)
        {
            var data = CreateUnitBasic(nameRes);
            data.gameObject = GameObject.Instantiate(data.prefabGameObject);            
            SetFolder(folder, data);
            GetInfoGameObject(data);
            _gameObjects.Add(data);
            return data;
        }

        protected GameObjectData SetGameObject(GameObject gameObject)
        {
            var data = new GameObjectData();
            data.gameObject = gameObject;
            GetInfoGameObject(data);
            _gameObjects.Add(data);
            return data;
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

        public void Dispose()
        {
            OnDispose();

            for (int i = 0; i < _iControllers.Count; i++)
            {
                ListControllers.Delete(_iControllers[i]);
                if (_iControllers[i] is IDisposable) (_iControllers[i] as IDisposable).Dispose();
            }

            _iControllers.Clear();
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                Object.Destroy(_gameObjects[i].gameObject);
            }
            _gameObjects.Clear();

        }

        protected virtual void OnDispose()
        {

        }

        #endregion
    }
}
