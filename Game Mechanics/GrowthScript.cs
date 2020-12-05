using System;
using UnityEngine;
using UnityEngine.Events;

/**
This script controls a growth mechanic.
*/
public class GrowthScript : MonoBehaviour
{
    [SerializeField, Range(0.0f, 100.0f)]
    private float Growth;

    [SerializeField, Range(0.0f, 100.0f)]
    private float InitialValue;

    [SerializeField]
    private UnityEvent OnGrowth;
    [SerializeField]
    private UnityEvent OnFinish;
    [SerializeField]
    private UnityEvent OnWither;
    [SerializeField]
    private UnityEvent onReset;

    public void Start()
    {
        Reset();
    }
    
    public void Reset()
    {
        Growth = InitialValue;
        onReset?.Invoke();
    }

    public void OnDisable()
    {
        Reset();
    }

    public void IncrementGrowth(float value)
    {
        Growth = Mathf.Min(Mathf.Max(Growth + value, 0.0f), 100.0f);
        OnGrowth?.Invoke();
        CheckStatus();
    }
    private void CheckStatus()
    {
        if (Growth >= 100.0f)
        {
            OnFinish?.Invoke();
        }
        else if (Growth <= 0.0f)
        {
            OnWither?.Invoke();
        }
    }
}

