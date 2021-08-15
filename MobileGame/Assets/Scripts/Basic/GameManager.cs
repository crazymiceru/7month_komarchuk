using UnityEngine;

namespace MobileGame
{

    internal sealed class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            LoadResources.Init();
            LoadBundles.Init();
            ListControllers.Init();
            ListControllers.Add(new GlobalGameController());
        }

        private void Start()
        {

            ListControllers.Initialization();
        }

        private void Update()
        {
            ListControllers.Execute(Time.deltaTime);
        }

        private void LateUpdate()
        {
            ListControllers.LateExecute();
        }
    }
}