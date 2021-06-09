using System.Linq;
using System.Threading.Tasks;
using ReGizmo.Drawing;
using UnityEngine;

namespace ReGizmo.Samples.Performance
{
#if !REGIZMO_DEV
    [AddComponentMenu("")]
#endif
    public class LinePerformanceTest : PerformanceTest
    {
        protected override void RunInternal()
        {
            /* Enumerable.Range(0, testSizeSqr * testSizeSqr)
            .AsParallel()
            .ForAll(val =>
            {
                int x = val / testSizeSqr;
                int y = val % testSizeSqr;

                var color = new Color((float)x / testSizeSqr, (float)y / testSizeSqr, 0, 1);
                Vector3 p1 = new Vector3(x, y, x + y);
                Vector3 p2 = -p1;
                ReDraw.Line(p1, p2, color);
            }); */

            for (int i = 0; i < testSizeSqr * testSizeSqr; i++)
            {
                int x = i / testSizeSqr;
                int y = i % testSizeSqr;

                var color = new Color((float)x / testSizeSqr, (float)y / testSizeSqr, 0, 1);
                Vector3 p1 = new Vector3(x, y, x + y);
                Vector3 p2 = -p1;
                ReDraw.Line(p1, p2, color);

                // var color = Random.ColorHSV();
                // Vector3 p1 = Random.insideUnitSphere * 250f;
                // Vector3 p2 = Random.insideUnitSphere * 250f;
                // ReDraw.Line(p1, p2, color);
            }
        }
    }
}