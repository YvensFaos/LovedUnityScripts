using UnityEngine;

/**
This script activates and deactivates a given GameObject.
*/
public class ActivateScript : MonoBehaviour
{
    [SerializeField]
    private GameObject activateObject;

    public void ActivateObject()
    {
        activateObject.SetActive(true);
    }

    public void DeactivateObject()
    {
        activateObject.SetActive(false);
    }
}

