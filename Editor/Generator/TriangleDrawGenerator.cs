
using ReGizmo.Drawing;
using UnityEngine;

namespace ReGizmo.Generator
{
    internal class TriangleDrawGenerator : DrawGenerator
    {
        public TriangleDrawGenerator()
        {
            methodName = "Triangle";
            fileName = $"TriangleDraw.generated.cs";

            methodShell =
@"
        public static void Triangle($PARAMS)
        {
            if (ReGizmoResolver<ReGizmoTriangleDrawer>.TryGet(out var drawer))
            {
                ref var data = ref drawer.GetShaderData();

                data.Position = $PARAM_1;
                data.Normal = $PARAM_2;
                data.Width = $PARAM_4.Value;
                data.Thickness = $PARAM_5;
                data.Color = new Vector3($PARAM_6.r, $PARAM_6.g, $PARAM_6.b);
                data.Flags = (int)$PARAM_3 | $PARAM_4.SizeMode;
            }
        }";
            variables = new Variable[] {
                new Variable(typeof(Vector3), "position", "currentPosition", "position"),
                new Variable(typeof(Vector3), "normal", "Vector3.up", "normal"),
                new Variable(typeof(DrawMode), "drawMode", "DrawMode.BillboardFree", "drawMode"),
                new Variable(typeof(Size), "width", "Size.Pixels(32f)", "width"),
                new Variable(typeof(float), "thickness", "1f", "thickness"),
                new Variable(typeof(Color), "color", "currentColor", "color"),
            };
        }

        protected override string GenerateInternal()
        {
            return GenerateHelper("");
        }
    }
}