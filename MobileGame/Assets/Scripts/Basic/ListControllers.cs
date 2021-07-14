using System;

namespace MobileGame
{
    public sealed class ListControllers 
    {
        private static event Action _init = delegate { };
        private static event Action<float> _execute = delegate { };
        private static event Action _lateExecute = delegate { };
        private static bool isInitHasPassed = false;
        public static int countAddListControllers = 0;

        public static void Init()
        {
        _init = delegate { };
        _execute = delegate { };
        _lateExecute = delegate { };
        isInitHasPassed = false;
        countAddListControllers = 0;
        }

        public static void Execute(float deltaTime)
        {
            _execute(deltaTime);
        }

        public static void Initialization()
        {
            isInitHasPassed = true;
            _init();
        }

        public static void LateExecute()
        {
            _lateExecute();
        }

        public static void Add(IController controller, string name = "")
        {
            countAddListControllers++;
            if (controller is IInitialization init)
            {
                _init += init.Initialization;
                if (isInitHasPassed) init.Initialization();
            }
            if (controller is IExecute execute)
            {
                _execute += execute.Execute;
            }
            if (controller is ILateExecute lateExecute)
            {
                _lateExecute += lateExecute.LateExecute;
            }
        }

        public static void Delete(IController controller)
        {
            countAddListControllers--;
            if (controller is IInitialization init)
            {
                _init -= init.Initialization;
            }
            if (controller is IExecute execute)
            {
                _execute -= execute.Execute;
            }
            if (controller is ILateExecute lateExecute)
            {
                _lateExecute -= lateExecute.LateExecute;
            }
        }
    }
}
