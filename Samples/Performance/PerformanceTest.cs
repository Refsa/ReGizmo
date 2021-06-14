using UnityEngine;

namespace ReGizmo.Samples.Performance
{
    internal interface IParallelTest
    {
        void RunParallelTest();
    }

    internal interface ISequentialTest
    {
        void RunSequentialTest();
    }


    public abstract class PerformanceTest : MonoBehaviour
    {
        [SerializeField] float duration = 10f;
        [SerializeField] protected int testSizeSqr = 100;

        [Header("Preview")]
        [SerializeField] bool previewParallel;
        [SerializeField] bool logTime;

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

        public bool IsParallel()
        {
            return this is IParallelTest;
        }

        public bool IsSequential()
        {
            return this is ISequentialTest;
        }

        public bool RunParallel()
        {
            if (Time.time - startTime > duration)
            {
                averageFrameTime = _frameTimeDebug.LastAvgFrameTime;
                return false;
            }

            if (this is IParallelTest parallelTest)
            {
                parallelTest.RunParallelTest();
            }

            return true;
        }

        public bool RunSequential()
        {
            if (Time.time - startTime > duration)
            {
                averageFrameTime = _frameTimeDebug.LastAvgFrameTime;
                return false;
            }

            if (this is ISequentialTest sequentialTest)
            {
                sequentialTest.RunSequentialTest();
            }

            return true;
        }

        public bool Run()
        {
            if (Time.time - startTime > duration)
            {
                averageFrameTime = _frameTimeDebug.LastAvgFrameTime;
                return false;
            }

            if (this is IParallelTest parallelTest)
            {
                parallelTest.RunParallelTest();
            }

            if (this is ISequentialTest sequentialTest)
            {
                sequentialTest.RunSequentialTest();
            }

            return true;
        }

        protected abstract void RunInternal();

        void Preview()
        {
            if (previewParallel && this is IParallelTest parallelTest)
            {
                parallelTest.RunParallelTest();
            }
            else if (this is ISequentialTest sequentialTest)
            {
                sequentialTest.RunSequentialTest();
            }
            else
            {
                RunInternal();
            }
        }

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            if (UnityEditor.Selection.activeGameObject != this.gameObject) return;

            var sw = System.Diagnostics.Stopwatch.StartNew();

            Preview();

            sw.Stop();

            if (logTime)
            {
                Debug.Log($"{this.GetType().Name} took {sw.ElapsedTicks / 10_000f} ms");
            }
        }
#endif
    }
}