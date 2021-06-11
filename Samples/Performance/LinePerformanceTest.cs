using System.Linq;
using System.Threading.Tasks;
using ReGizmo.Drawing;
using UnityEngine;

namespace ReGizmo.Samples.Performance
{
#if !REGIZMO_DEV
    [AddComponentMenu("")]
#endif
    public class LinePerformanceTest : PerformanceTest, ISequentialTest, IParallelTest
    {
        public void RunParallelTest()
        {
            var center = transform.position;

            Enumerable.Range(0, testSizeSqr * testSizeSqr)
            .AsParallel()
            .ForAll(val =>
            {
                int x = val / testSizeSqr;
                int y = val % testSizeSqr;

                var rng = ThreadRandom.Borrow();

                var color = new Color(
                    rng.NextFloat(0f, 1f),
                    rng.NextFloat(0f, 1f),
                    rng.NextFloat(0f, 1f),
                    1f
                );
                Vector3 p1 = (rng.NextFloat3Direction() * rng.NextFloat(0f, 1f)) * 250f;
                Vector3 p2 = (rng.NextFloat3Direction() * rng.NextFloat(0f, 1f)) * 250f;
                p1 += center;
                p2 += center;
                ReDraw.Line(p1, p2, color);

                ThreadRandom.Release(rng);
            });
        }

        public void RunSequentialTest()
        {
            for (int i = 0; i < testSizeSqr * testSizeSqr; i++)
            {
                int x = i / testSizeSqr;
                int y = i % testSizeSqr;

                var color = Random.ColorHSV();
                Vector3 p1 = Random.insideUnitSphere * 250f;
                Vector3 p2 = Random.insideUnitSphere * 250f;
                ReDraw.Line(p1, p2, color);
            }
        }

        protected override void RunInternal()
        {
            for (int i = 0; i < testSizeSqr * testSizeSqr; i++)
            {
                var color = Random.ColorHSV();
                Vector3 p1 = Random.insideUnitSphere * 250f;
                Vector3 p2 = Random.insideUnitSphere * 250f;
                ReDraw.Line(p1, p2, color);
            }
        }
    }
}