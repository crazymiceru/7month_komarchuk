using UnityEngine.Analytics;

namespace MobileGame
{
    internal sealed class UnityAnalitics : IAnalitics
    {
        void IAnalitics.SendMessage(string eventName,object parameters)
        {
            if (parameters == null) parameters = "";
            Analytics.SendEvent(eventName, parameters);
        }
    }
}