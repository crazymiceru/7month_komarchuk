using UnityEngine;

namespace MobileGame
{
    public sealed class PackInteractiveData
    {
        public int attackPower { get; }
        public TypeItem typeItem { get; }
        public PackInteractiveData(int attackPower, TypeItem typeItem)
        {
            this.attackPower = attackPower;
            this.typeItem = typeItem;            
        }
    }
}