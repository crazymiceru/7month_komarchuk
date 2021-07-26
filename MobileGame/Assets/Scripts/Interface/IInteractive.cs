using System;
using System.Collections.Generic;
using UnityEngine;

namespace MobileGame
{
    public interface IInteractive
    {
        (int scores, bool isDead) Attack(PackInteractiveData data);
        event Action<bool> evtAnyCollision;
        event Action<IInteractive, bool> evtCollision;
        event Func<PackInteractiveData, (int, bool)> evtAttack;
        event Action<Collider2D, bool> evtTrigger;
        void Kill();
    }
}