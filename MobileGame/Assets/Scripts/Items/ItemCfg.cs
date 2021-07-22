using UnityEngine;

namespace MobileGame
{
    [CreateAssetMenu(menuName = "My/Item", fileName = "Item")]
    public class ItemCfg : ScriptableObject
    {
        [SerializeField] private int _id;
        public int Id => _id;
        
        [SerializeField] private string _titleName;
        public string TitleName => _titleName;
         
    }
}