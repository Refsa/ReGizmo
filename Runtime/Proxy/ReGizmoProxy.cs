using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class ReGizmoProxy : MonoBehaviour, IDisposable
{
    public event Action inUpdate;
    public event Action inLateUpdate;
    public event Action inFixedUpdate;
    public event Action inDrawGizmos;

    public event Action inDestroy;

    void Awake()
    {
        Debug.Log("######### ReGizmo Proxy Setup #########");
    }

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