using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Collections;

namespace MobileGame
{
    public sealed class CustomButton : Button
    {
        public static string NameTypeAnimationData = nameof(_customButtonCfg);

        [SerializeField] private CustomButtonCfg _customButtonCfg;
        //[SerializeField] private TypeAnimationData[] _typeAnimationData;
        private Dictionary<TypeTransitionButton, TypeAnimationData> _typeAnimationDataDictionary
            = new Dictionary<TypeTransitionButton, TypeAnimationData>();

        private RectTransform _rectTransform;
        private bool _isGetValues;
        private Vector3 _startPosition;
        private Vector3 _startRotation;
        private Vector3 _startScale;
        private Tweener tweener = null;


        protected override void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            MakeDictionary();
            StartCoroutine(WaitGetStartValues());
        }

        private IEnumerator WaitGetStartValues()
        {
            yield return null; 
            GetStartValues();            
        }

        private void GetStartValues()
        {
            if (!_isGetValues)
            {
                _startPosition = _rectTransform.position;
                _startRotation = _rectTransform.rotation.eulerAngles;
                _startScale = _rectTransform.localScale;
                _isGetValues = true;
                MakeAnimation(TypeTransitionButton.exit);
            }
        }

        private void MakeDictionary()
        {
            foreach (TypeTransitionButton item in (TypeTransitionButton[])Enum.GetValues(typeof(TypeTransitionButton)))
                _typeAnimationDataDictionary.Add(item, new TypeAnimationData());

            if (_customButtonCfg.typeAnimationData == null) return;

                foreach (var item in _customButtonCfg.typeAnimationData)
                    _typeAnimationDataDictionary[item.typeTransitionButton] = item;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            GetStartValues();
            MakeAnimation(TypeTransitionButton.click);
            base.OnPointerClick(eventData);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            GetStartValues();
            MakeAnimation(TypeTransitionButton.highlight);
            base.OnPointerEnter(eventData);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            GetStartValues();
            MakeAnimation(TypeTransitionButton.exit);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            GetStartValues();
            MakeAnimation(TypeTransitionButton.pressed);
        }

        private void MakeAnimation(TypeTransitionButton typeTransition)
        {
            var item = _typeAnimationDataDictionary[typeTransition];

            _rectTransform.DOKill();
            if (item.audioClip!=null)
            {
                SoundUI.inst.Play(item.audioClip);                
            }
            if (item.isClearPosition)
            {
                _rectTransform.position = _startPosition;
                _rectTransform.rotation = Quaternion.Euler(_startRotation);
                _rectTransform.localScale = _startScale;
            }

            switch (item.typeAnimation)
            {
                case TypeAnimationButton.shake:
                    tweener = _rectTransform.DOShakeAnchorPos(item.duration, item.value).SetEase(item.ease);
                    if (item.isLoop) tweener?.SetLoops(-1);
                    break;
                case TypeAnimationButton.scale:
                    tweener = _rectTransform.DOScale(item.value, item.duration).SetEase(item.ease);
                    if (item.isLoop) tweener?.SetLoops(-1);
                    break;
                case TypeAnimationButton.rotate:
                    tweener = _rectTransform.DORotate(new Vector3(0, 0, item.value), item.duration, RotateMode.FastBeyond360).SetEase(item.ease);
                    if (item.isLoop) tweener?.SetLoops(-1);
                    break;
            }
        }

        protected override void OnDestroy()
        {
            _rectTransform.DOKill();
        }
    }
}