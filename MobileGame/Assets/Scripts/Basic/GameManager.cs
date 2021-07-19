using UnityEngine;

namespace MobileGame
{

    internal sealed class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            Time.timeScale = 1;
            LoadResources.Init();
            ListControllers.Init();

            new GameController();
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