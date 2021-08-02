using System.Collections;
using UnityEngine;

public class CoroutinesView : MonoBehaviour
{
    public static CoroutinesView inst;
    private void Awake()
    {
        if (inst == null) inst = this;
    }

    public void AddCoroutine(IEnumerator Metod)
    {
        StartCoroutine(Metod);
    }
    public void RemoveCoroutine(IEnumerator Metod)
    {
        StopCoroutine(Metod);
    }


}
