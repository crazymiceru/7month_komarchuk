using UnityEngine;

namespace MobileGame
{
    public interface IUnitView
    {
        public (TypeUnit type, int cfg) GetTypeItem();
        void SetTypeItem(TypeUnit type = TypeUnit.None, int cfg = -1);

        public Transform objectTransform { get; }
        public Rigidbody2D objectRigidbody2D { get; }
        public SpriteRenderer objectSpriteRednderer { get; }

    }
}