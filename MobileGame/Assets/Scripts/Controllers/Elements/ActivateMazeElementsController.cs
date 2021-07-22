using System.Linq;
using UnityEngine;

namespace MobileGame
{
    internal sealed class ActivateMazeElementsController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("ActivateMazeElementsController");        

        internal ActivateMazeElementsController(Transform folder) 
        {
            var objects=folder.GetComponentsInChildren<MonoBehaviour>().OfType<IUnitView>();
            foreach (var item in objects)
            {
                var t = item.GetTypeItem();
                //Debug.Log($"GameObject:{(item as MonoBehaviour).name}");
                AddController(
                    Utils.ParseType(t.type).SetNumCfg(t.cfg).SetGameObject((item as MonoBehaviour).gameObject).CreateControllers()
                    );

            }

        }
    }
}
