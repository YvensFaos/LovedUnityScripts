using UnityEngine;
using UnityEngine.Events;

/**
This script attaches a UnityEvent to this object callable via CallEvents script.
*/
public class EventHandler : MonoBehaviour
{
    [SerializeField]
    private UnityEvent events;

    public void CallEvents()
    {
        events.Invoke();
    }
}


