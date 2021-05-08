using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField]
    private float DestroyTime = 1;
    
    void Start()
    {
        Destroy(gameObject, DestroyTime);
    }
}
