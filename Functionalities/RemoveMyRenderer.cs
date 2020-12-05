using UnityEngine;

/**
This script disables the renderer of a game object on Awake.
*/
[RequireComponent(typeof(Renderer))]
public class RemoveMyRenderer : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Renderer>().enabled = false;
    }
}

