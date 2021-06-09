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
            for (int x = 0; x < testSizeSqr; x++)
            {
                for (int y = 0; y < testSizeSqr; y++)
                {
                    var color = Random.ColorHSV();

                    Vector3 p1 = Random.insideUnitSphere * 250f;
                    Vector3 p2 = Random.insideUnitSphere * 250f;
                    ReDraw.Line(p1, p2, color);
                }
            }
        }
    }
}