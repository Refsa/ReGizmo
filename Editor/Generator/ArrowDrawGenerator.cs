
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
            Vector3 endPoint = position + direction * $PARAM_1;
            ReDraw.Line(position, endPoint, $PARAM_4, $PARAM_3);

            switch (cap)
            {
                case ArrowCap.Triangle:
                    ReDraw.Triangle(endPoint, direction, DrawMode.BillboardAligned, $PARAM_2, 0f, $PARAM_4);
                    break;
                case ArrowCap.Cone:
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.up, direction);
                    ReDraw.Cone(endPoint, rotation, Vector3.one * $PARAM_2.Value + Vector3.up * $PARAM_2.Value, $PARAM_4);
                    break;
                case ArrowCap.Sphere:
                    ReDraw.Sphere(endPoint, Quaternion.identity, Vector3.one * $PARAM_2.Value, $PARAM_4);
                    break;
                case ArrowCap.Cube:
                    rotation = Quaternion.FromToRotation(Vector3.up, direction);
                    ReDraw.Cube(endPoint, rotation, Vector3.one * $PARAM_2.Value, $PARAM_4);
                    break;
            }
        }";
            variables = new Variable[] {
                new Variable(typeof(float), "length", "1f", "length"),
                new Variable(typeof(Size), "arrowSize", "Size.Units(0.2f)", "arrowSize"),
                new Variable(typeof(float), "lineWidth", "1f", "lineWidth"),
                new Variable(typeof(Color), "color", "currentColor", "color"),
            };
        }

        protected override string GenerateInternal()
        {
            return GenerateHelper("Vector3 position, Vector3 direction, ArrowCap cap");
        }
    }
}