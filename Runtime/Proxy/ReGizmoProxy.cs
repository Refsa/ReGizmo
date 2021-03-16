using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReGizmo.Core
{
    [AddComponentMenu("")]
    internal class ReGizmoProxy : MonoBehaviour, IDisposable
    {
        public event Action inUpdate;
        public event Action inLateUpdate;
        public event Action inDrawGizmos;

        public event Action inEnable;
        public event Action inDisable;

        public event Action inDestroy;

        void Start()
        {
#if REGIZMO_RUNTIME
            if (!ReGizmo.IsSetup)
            {
                ReGizmo.Initialize();
            }
#endif
        }

        void OnEnable()
        {
            inEnable?.Invoke();
        }

        void OnDisable()
        {
            inDisable?.Invoke();
        }

        void OnDestroy()
        {
            inDestroy?.Invoke();
        }

        void Update()
        {
            inUpdate?.Invoke();
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
}