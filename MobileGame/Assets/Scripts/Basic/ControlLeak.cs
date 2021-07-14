
using System.Collections.Generic;
using UnityEngine;

public class ControlLeak
{
    public static int Count { get; private set; }
    public static Dictionary<string, int> dataLeak { get; private set; } = new Dictionary<string, int>(); 

    private string _name;

    public ControlLeak(string name)
    {
        _name = name;
        Count++;
        dataLeak[name] = dataLeak.ContainsKey(name) ? dataLeak[name] + 1 : 1;
    }

    ~ControlLeak()
    {
        Count--;
        dataLeak[_name] -= 1;
    }
}
