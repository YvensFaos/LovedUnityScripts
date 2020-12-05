using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/**
This script calls a Unity Events list after a given time.
*/
public class StartEventAfterSeconds : MonoBehaviour
{
    [SerializeField] private UnityEvent events;

    public void StartAfter(float seconds)
    {
        StartCoroutine(StartAfterCoroutine(seconds));
    }

    private IEnumerator StartAfterCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        events.Invoke();
    }
}
