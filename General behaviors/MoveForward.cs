using UnityEngine;

/// <summary>
/// Original Author: Bugulet
/// Revisor: YvensFaos
/// </summary>
public class MoveForward : MonoBehaviour
{
    [SerializeField] private float speed;
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
