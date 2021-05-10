
using ReGizmo.Drawing;
using UnityEngine;

namespace ReGizmo.Generator
{
    internal class ArrowDrawGenerator : DrawGenerator
    {
        public ArrowDrawGenerator()
        {
            methodName = "Arrow";
            fileName = $"ArrowDraw.generated.cs";

            methodShell =
@"
        public static void Arrow($PARAMS)
        {
            ReDraw.Line(position, position + direction * $PARAM_1, $PARAM_4, $PARAM_3);
            ReDraw.Triangle(position + direction * $PARAM_1, direction, DrawMode.BillboardAligned, $PARAM_2, 0f, $PARAM_4);
        }";
            variables = new Variable[] {
                new Variable(typeof(float), "length", "1f", "length"),
                new Variable(typeof(Size), "arrowSize", "Size.Pixels(8f)", "arrowSize"),
                new Variable(typeof(float), "lineWidth", "1f", "lineWidth"),
                new Variable(typeof(Color), "color", "currentColor", "color"),
            };
        }

        protected override string GenerateInternal()
        {
            return GenerateHelper("Vector3 position, Vector3 direction");
        }
    }
}