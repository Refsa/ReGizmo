using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ReGizmo.Drawing;
using UnityEngine;

namespace ReGizmo.Samples
{
    public class CubeGerstnerSample : MonoBehaviour
    {
        [System.Serializable]
        struct Wave
        {
            public Vector2 Dir;
            [Range(0f, 1f)] public float Steepness;
            public float Wavelength;
        }

        [SerializeField] Wave[] waves;
        [SerializeField] int sizeSqr = 256;

        Stopwatch sw = new Stopwatch();

        static readonly float TAU = Mathf.PI * 2f;

        void Gerstner(Wave wave, float time, ref Vector3 point)
        {
            float k = TAU / wave.Wavelength;
            float a = wave.Steepness / k;
            float c = Mathf.Sqrt(9.8f / k);
            Vector2 d = wave.Dir.normalized;

            float f = k * ((d.x * point.x + d.y * point.z) - (c - time));

            point.x += d.x * (a * Mathf.Cos(f));
            point.z += d.y * (a * Mathf.Cos(f));
            point.y += a * Mathf.Sin(f);
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
#else
        void Update()
#endif
        {
            sw.Restart();
            float time = TimeHelper.Time;

            Enumerable.Range(0, sizeSqr * sizeSqr)
                .AsParallel()
                .ForAll(val =>
            {
                int x = val / sizeSqr;
                int y = val % sizeSqr;
                Vector3 point = new Vector3(x, 0f, y);

                for (int i = 0; i < waves?.Length; i++)
                {
                    Gerstner(waves[i], time, ref point);
                }

                float h = (point.y + 1f) * 0.5f;
                h = Mathf.SmoothStep(0.2f, 1f, h);
                point.y = 0f;

                ReDraw.Cube(point, new Vector3(1f, h * 3f, 1f), Color.blue.Darken(h));
            });

            sw.Stop();
#if UNITY_EDITOR
            UnityEngine.Debug.Log($"elapsed: {sw.ElapsedTicks / 10_000f} ms");
#endif
        }
    }
}