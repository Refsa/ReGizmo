using System;
using UnityEngine;

public class ReGizmoProxy : MonoBehaviour, IDisposable
{
    public event Action inUpdate;
    public event Action inLateUpdate;
    public event Action inFixedUpdate;
    public event Action inDrawGizmos;

    public event Action inDestroy;

    void OnDestroy()
    {
        inDestroy?.Invoke();
    }

    void Update()
    {
        inUpdate?.Invoke();
    }

    void FixedUpdate()
    {
        inFixedUpdate?.Invoke();
    }

    void LateUpdate()
    {
        inLateUpdate?.Invoke();
    }

    void OnDrawGizmos()
    {
        inDrawGizmos?.Invoke();
    }

    public void Dispose()
    {
        inDestroy?.Invoke();
    }
}