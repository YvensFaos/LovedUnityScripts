using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/**
REQUIRES: DOTWEEN

This script uses DTWeen to move one object to a given Vector3 position.
*/
public class MoveObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToBeMoved;
    [SerializeField] private float warmUpTime;
    [SerializeField] private float duration;
    [SerializeField] private Vector3 moveTo;

    public void Move()
    {
        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        yield return new WaitForSeconds(warmUpTime);
        objectToBeMoved.transform.DOMove(moveTo, duration);
    } 
}
