using System.Collections;
using UnityEngine;

/**
This script activates one GameObject given a certain period of time.
*/
public class ActivateObjectAfterTime : MonoBehaviour
{
    [SerializeField] private float time;

    public void ActivateObjectAfterTimeFunction(GameObject objectToActivate)
    {
        StartCoroutine(ActivateObjectAfterTimeCoroutine(objectToActivate));
    }

    private IEnumerator ActivateObjectAfterTimeCoroutine(GameObject objectToActivate)
    {
        yield return new WaitForSeconds(time);
        objectToActivate.SetActive(true);
    }
}
