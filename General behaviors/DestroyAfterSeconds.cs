using UnityEngine;

/// <summary>
/// Original Author: Bugulet
/// Revisor: YvensFaos
/// </summary>
public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField] private float destroyTime = 1.0f;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
