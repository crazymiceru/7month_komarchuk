using System;
using UnityEngine;

public class OnTriggerView : MonoBehaviour
{
    public bool isOnTrigger = false;
    private int _countTrigger = 0;

    public event Action<Collider2D, bool> evtUpdate = delegate { };

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        _countTrigger++;
        isOnTrigger = true;
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
            isOnTrigger = false;
            evtUpdate.Invoke(other, false);
        }
    }

}
