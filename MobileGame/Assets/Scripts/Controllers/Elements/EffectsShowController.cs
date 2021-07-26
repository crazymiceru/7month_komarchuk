using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MobileGame
{
    internal sealed class EffectsImageData
    {        
        public GameObject gameObject;
        public TextMeshProUGUI text;
    }

    internal sealed class EffectsShowController : ControllerBasic
    {
        private ControlLeak _controlLeak = new ControlLeak("EffectsShowController");
        private const string _nameRes = "Items/EffectsPlace";
        private const string _nameResEffect = "Items/Effect";
        private EffectsM _effectsM;
        private Dictionary<int, EffectsImageData> effectsImageData = new Dictionary<int, EffectsImageData>();
        private int countEffects;
        private float heightImage;
        private Transform folder;

        internal EffectsShowController(EffectsM effectsM)
        {
            var data = CreateGameObject(Reference.Canvas, _nameRes);
            folder = data.gameObject.transform;
            _effectsM = effectsM;
            _effectsM.EvtAddItem += AddItem;
            _effectsM.EvtRemoveItem += RemoveItem;
            var go = CreateGameObject(folder, _nameResEffect).gameObject;

            effectsImageData.Add(0, new EffectsImageData {gameObject=go});
            heightImage = go.GetComponent<RectTransform>().rect.height;
            go.SetActive(false);
            countEffects = 0;
        }

        private void AddItem(EffectsItemCfg effectsItemCfg,bool isHave)
        {
            if (isHave) return;
            
            var go = GameObject.Instantiate(effectsImageData[0].gameObject, folder);
            go.SetActive(true);
            
            var image = go.transform.GetChild(1).GetComponent<Image>();
            var text = go.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            effectsImageData.Add(effectsItemCfg.Id,new EffectsImageData {gameObject=go,text=text}); 
            image.sprite = effectsItemCfg.Sprite;
            go.transform.localPosition = new Vector3(0, -countEffects * heightImage, 0);
            
        }

        private void RemoveItem(EffectsItemCfg upgradeItemCfg)
        {
            if (effectsImageData.TryGetValue(upgradeItemCfg.Id,out EffectsImageData currentEffects))
            {
                GameObject.Destroy(currentEffects.gameObject);
                effectsImageData.Remove(upgradeItemCfg.Id);
            }
        }
    }
}
