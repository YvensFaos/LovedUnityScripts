using UnityEngine;
using UnityEngine.Events;

/**
This script adds UnityEvents calls for this object's OnEnable and OnDisable.
*/
public class EnableDisableEventCaller : MonoBehaviour
{
    [SerializeField] private UnityEvent onEnableEvents;
    [SerializeField] private UnityEvent onDisableEvents;

    private void OnEnable()
    {
        onEnableEvents.Invoke();
    }

    private void OnDisable()
    {
        onDisableEvents.Invoke();
    }
}
