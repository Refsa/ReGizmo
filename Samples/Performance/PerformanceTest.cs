using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ReGizmo.Samples.Performance
{
    public abstract class PerformanceTest : MonoBehaviour
    {
        [SerializeField] float duration = 10f;
        [SerializeField] protected int testSizeSqr = 100;

        FrameTimeDebug _frameTimeDebug;

        float startTime;

        float averageFrameTime;
        public float AverageFrameTime => averageFrameTime;

        void Awake()
        {
            _frameTimeDebug = FindObjectOfType<FrameTimeDebug>();
        }

        public virtual void Prepare()
        {
            _frameTimeDebug.Clear();
            startTime = Time.time;
        }

        public bool Warmup()
        {
            if (Time.time - startTime > duration * 0.25f)
            {
                Prepare();
                return false;
            }

            RunInternal();

            return false;
        }

        public bool Run()
        {
            if (Time.time - startTime > duration)
            {
                averageFrameTime = _frameTimeDebug.LastAvgFrameTime;
                return false;
            }

            RunInternal();

            return true;
        }

        protected abstract void RunInternal();

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            if (Selection.activeGameObject != this.gameObject) return;

            var sw = System.Diagnostics.Stopwatch.StartNew();
            RunInternal();
            sw.Stop();
            Debug.Log($"{this.GetType().Name} took {sw.ElapsedTicks / 10_000f} ms");
        }
#endif
    }
}