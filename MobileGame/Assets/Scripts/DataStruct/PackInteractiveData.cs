using UnityEngine;

namespace MobileGame
{
    public sealed class PackInteractiveData
    {
        public int attackPower { get; }
        public TypeUnit typeItem { get; }
        public PackInteractiveData(int attackPower, TypeUnit typeItem)
        {
            this.attackPower = attackPower;
            this.typeItem = typeItem;            
        }
    }
}