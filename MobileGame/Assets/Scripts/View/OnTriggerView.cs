using System;
using UnityEngine;

public class OnTriggerView : MonoBehaviour
{
    private int _countTrigger = 0;
    public event Action<Collider2D, bool> evtUpdate = delegate { };
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        _countTrigger++;
        if (_countTrigger == 1)
        {
            evtUpdate.Invoke(other, true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _countTrigger--;
        if (_countTrigger == 0)
        {
            evtUpdate.Invoke(other, false);
        }
    }
}
