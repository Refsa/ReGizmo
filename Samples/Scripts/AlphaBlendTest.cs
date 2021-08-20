using ReGizmo.Drawing;
using UnityEngine;

namespace ReGizmo.Samples
{
    internal class AlphaBlendTest : DrawSampleBase
    {
        protected override void Draw()
        {
            using (new TransformScope(transform))
            {
                ReDraw.Quad(Vector3.zero, Quaternion.identity, Vector3.one * 2f, Color.red.WithAlpha(0.33f));
                ReDraw.Quad(Vector3.back + Vector3.right + Vector3.down, Quaternion.identity, Vector3.one * 2f, Color.green.WithAlpha(0.33f));
                // ReDraw.TextSDF("AlphaBlend", Vector3.back + Vector3.right + Vector3.down, 24f, Color.white);
                ReDraw.Quad(Vector3.back * 2 + Vector3.right * 2 + Vector3.down * 2, Quaternion.identity, Vector3.one * 2f, Color.blue.WithAlpha(0.33f));
            }
        }
    }
}