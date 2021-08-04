﻿using DG.Tweening;

namespace MobileGame
{
    [System.Serializable]
    public sealed class TypeAnimationData
    {
        public TypeTransitionButton typeTransitionButton = TypeTransitionButton.none;
        public TypeAnimationButton typeAnimation = TypeAnimationButton.none;
        public Ease ease = Ease.Linear;
        public float value = 0;
        public float duration = 0.5f;
        public bool isLoop;
        public bool isClearPosition;
    }
}