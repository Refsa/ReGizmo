using System;
using UnityEngine;

public class ReGizmoProxy : MonoBehaviour
{
    public event Action inUpdate;
    public event Action inLateUpdate;
    public event Action inFixedUpdate;
    public event Action inDrawGizmos;

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
}