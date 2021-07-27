using System.Linq;
using UnityEngine;

namespace MobileGame
{
    internal sealed class ActivateMazeElementsController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("ActivateMazeElementsController");        

        internal ActivateMazeElementsController(Transform findFolder,UnitM unitMPlayer,IUnitView playerView) 
        {
            ControllerBasic controller;
            var objects=findFolder.GetComponentsInChildren<MonoBehaviour>().OfType<IUnitView>();
            foreach (var item in objects)
            {
                var type = item.GetTypeItem();
                switch (type.type)
                {
                    case TypeUnit.UpgradeItem:
                        AddController( new UpgradeItemBuild().SetNumCfg(type.cfg)
                            .SetGameObject((item as MonoBehaviour).gameObject)
                            .CreateControllers());                        
                        break;
                    case TypeUnit.EffectsItem:
                        AddController(new EffectsItemBuild().SetNumCfg(type.cfg)
                            .SetGameObject((item as MonoBehaviour).gameObject)
                            .CreateControllers());
                        break;
                    case TypeUnit.EnemyBird:
                        AddController( controller = new EnemyBirdBuild().SetNumCfg(type.cfg)
                            .SetGameObject((item as MonoBehaviour).gameObject));
                        (controller as EnemyBirdBuild).Create(unitMPlayer, playerView);
                        break;
                }


                //Debug.Log($"GameObject:{(item as MonoBehaviour).name}");
                AddController(
                    UtilsUnit.ParseType(type.type).SetNumCfg(type.cfg)
                    .SetGameObject((item as MonoBehaviour).gameObject).CreateControllers()
                    );

            }

        }
    }
}
