using System;
using System.Collections.Generic;
using UnityEngine;

namespace MobileGame
{
    public interface IInteractive
    {
        public (int scores, bool isDead) Attack(PackInteractiveData data);
        public event Action<bool> evtAnyCollision;
        public event Action<IInteractive, bool> evtCollision;
        public event Func<PackInteractiveData, (int, bool)> evtAttack;
        public event Action<Collider2D, bool> evtTrigger;
        internal void Kill();
    }
}