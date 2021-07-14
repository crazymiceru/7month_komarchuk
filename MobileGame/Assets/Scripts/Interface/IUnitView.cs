using UnityEngine;

namespace MobileGame
{
    public interface IUnitView
    {
        public (TypeItem type, int cfg) GetTypeItem();
        void SetTypeItem(TypeItem type = TypeItem.Any, int cfg = -1);

        public Transform objectTransform { get; }
        public Rigidbody2D objectRigidbody2D { get; }
        public SpriteRenderer objectSpriteRednderer { get; }

    }
}