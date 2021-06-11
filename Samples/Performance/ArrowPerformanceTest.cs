using System.Linq;
using ReGizmo.Drawing;
using UnityEngine;

namespace ReGizmo.Samples.Performance
{
#if !REGIZMO_DEV
    [AddComponentMenu("")]
#endif
    public class ArrowPerformanceTest : PerformanceTest, ISequentialTest, IParallelTest
    {
        public void RunParallelTest()
        {
            Vector3 center = transform.position;
            Enumerable.Range(0, testSizeSqr * testSizeSqr)
                .AsParallel()
                .ForAll(val =>
                {
                    int x = val / testSizeSqr;
                    int y = val % testSizeSqr;

                    var rng = ThreadRandom.Borrow();
                    Vector3 p1 = (rng.NextFloat3Direction() * rng.NextFloat(0f, 1f)) * 50f;
                    p1 += center;
                    Vector3 p2 = (rng.NextFloat3Direction() * rng.NextFloat(0f, 1f));

                    Color color = new Color();
                    float s = rng.NextFloat(0f, 1f);
                    color.r = rng.NextFloat(0f, 1f) * s;
                    color.g = rng.NextFloat(0f, 1f) * s;
                    color.b = rng.NextFloat(0f, 1f) * s;
                    color.a = 1f;

                    ReDraw.Arrow(p1, p2, ArrowCap.Triangle, rng.NextFloat(1f, 10f), 1f, color);
                    ThreadRandom.Release(rng);
                });
        }

        public void RunSequentialTest()
        {
            for (int x = 0; x < testSizeSqr; x++)
            {
                for (int y = 0; y < testSizeSqr; y++)
                {
                    var color = Random.ColorHSV();

                    Vector3 p1 = Random.insideUnitSphere * 50f;
                    Vector3 p2 = Random.insideUnitSphere;
                    ReDraw.Arrow(p1, p2, ArrowCap.Triangle, Random.Range(1f, 10f), 1f, color);
                }
            }
        }

        protected override void RunInternal()
        {
            for (int x = 0; x < testSizeSqr; x++)
            {
                for (int y = 0; y < testSizeSqr; y++)
                {
                    var color = Random.ColorHSV();

                    Vector3 p1 = Random.insideUnitSphere * 50f;
                    Vector3 p2 = Random.insideUnitSphere;
                    ReDraw.Arrow(p1, p2, ArrowCap.Triangle, Random.Range(1f, 10f), 1f, color);
                }
            }
        }
    }
}