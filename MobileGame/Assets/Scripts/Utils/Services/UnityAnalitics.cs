using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

namespace MobileGame
{
    internal sealed class UnityAnalitics : IAnalitics
    {
        void IAnalitics.SendMessage(string eventName,Dictionary<string,object> parameters)
        {
            if (parameters == null) parameters = new Dictionary<string, object>();            
            var result=Analytics.CustomEvent(eventName, parameters);
            //Debug.Log($"Result Analitics:{result}");
        }
    }
}