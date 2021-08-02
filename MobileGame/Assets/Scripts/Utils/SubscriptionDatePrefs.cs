using System;
using UnityEngine;

public sealed class SubscriptionDatePrefs : iReadOnlySubscriptionField<DateTime>
{
    public DateTime Value
    {
        get => _value;
        set
        {
            _value = value;
            PlayerPrefs.SetString(_name, _value.ToString());
            evtChange.Invoke(value);
        }
    }
    private DateTime _value;
    private string _name;
    private event Action<DateTime> evtChange = delegate { };

    public SubscriptionDatePrefs(string name)
    {
        _name = name;
        var result = PlayerPrefs.GetString(_name, "");
        if (result.Equals("")) Value = new DateTime(2000, 1, 1);
        else Value = DateTime.Parse(PlayerPrefs.GetString(_name, ""));
    }

    public void Subscribe(Action<DateTime> func)
    {
        evtChange += func;
    }

    public void UnSubscribe(Action<DateTime> func)
    {
        evtChange -= func;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
