using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CallOnCollision : MonoBehaviour
{
    [SerializeField] bool OnCollision = true;
    [SerializeField] bool OnTrigger = false;

    [Tooltip("What object type do you want this to work on, empty for any object")]
    public string CollisionObjectTag="";
    
    public UnityEvent eventToTrigger;
    
    private void OnTriggerEnter(Collider other)
    {
        if (OnTrigger)
        {
            if (CollisionObjectTag.Length > 0 && other.gameObject.tag == CollisionObjectTag)
            {
                eventToTrigger.Invoke();
            }

            if (CollisionObjectTag.Length == 0)
            {
                eventToTrigger.Invoke();
            }
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (OnCollision)
        {
            if (CollisionObjectTag.Length > 0 && collision.gameObject.tag == CollisionObjectTag)
            {
                eventToTrigger.Invoke();
            }

            if (CollisionObjectTag.Length == 0)
            {
                eventToTrigger.Invoke();
            }
        }
    }
}
