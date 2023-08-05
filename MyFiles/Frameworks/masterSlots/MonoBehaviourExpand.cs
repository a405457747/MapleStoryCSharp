using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public static class MonoBehaviourExpand
{
    public static void Delay(this MonoBehaviour mono, float second, UnityAction action)
    {
        IEnumerator delay()
        {
            yield return new WaitForSeconds(second);
            action?.Invoke();
        }

        mono.StartCoroutine(delay());
    }
}