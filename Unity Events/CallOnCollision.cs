using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Original Author: Bugulet
/// Revisor: YvensFaos
/// </summary>
public class CallOnCollision : MonoBehaviour
{
    [SerializeField] private bool onCollision = true;
    [SerializeField] private bool onTrigger;

    [Tooltip("What object type do you want this to work on, empty for any object")] [SerializeField]
    private string objectTag;

    [SerializeField] private UnityEvent eventToTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (onTrigger)
        {
            Resolve(other.gameObject);    
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (onCollision)
        {
            Resolve(other.gameObject);    
        }
    }

    private void Resolve(GameObject other)
    {
        if (objectTag.Length > 0)
        {
            if (other.CompareTag(objectTag))
            {
                eventToTrigger.Invoke();
            }
        }
        else
        {
            eventToTrigger.Invoke();
        }
    }
}
