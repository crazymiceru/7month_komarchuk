using UnityEngine.Analytics;

namespace MobileGame
{
    internal interface IAnalitics
    {
        internal void SendMessage(string eventName, object parameters=null);
    }
}