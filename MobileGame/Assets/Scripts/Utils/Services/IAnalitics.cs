using System.Collections.Generic;

namespace MobileGame
{
    internal interface IAnalitics
    {
        internal void SendMessage(string eventName, Dictionary<string, object> parameters=null);
    }
}