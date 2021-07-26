using UnityEngine;

namespace MobileGame
{
    public interface IUnitView
    {
        (TypeUnit type, int cfg) GetTypeItem();
        void SetTypeItem(TypeUnit type = TypeUnit.None, int cfg = -1);

        Transform objectTransform { get; }
        Rigidbody2D objectRigidbody2D { get; }
        SpriteRenderer objectSpriteRednderer { get; }
    }
}