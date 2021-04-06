using UnityEngine;

namespace ReGizmo.Drawing
{
    public partial class ReDraw
    {
        public static void Line(Vector3 p1, Vector3 p2, Color color1, Color color2, float width1, float width2)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p1;
                shaderData.Color = color1;
                shaderData.Width = width1;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p2;
                shaderData.Color = color2;
                shaderData.Width = width2;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2, Color color, float width1, float width2)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p1;
                shaderData.Color = color;
                shaderData.Width = width1;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p2;
                shaderData.Color = color;
                shaderData.Width = width2;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2, Color color, float width)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p1;
                shaderData.Color = color;
                shaderData.Width = width;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p2;
                shaderData.Color = color;
                shaderData.Width = width;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2, Color color)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p1;
                shaderData.Color = color;
                shaderData.Width = 1f;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p2;
                shaderData.Color = color;
                shaderData.Width = 1f;
            }
        }

        public static void Line(Vector3 p1, Vector3 p2)
        {
            if (ReGizmoResolver<ReGizmoLineDrawer>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p1;
                shaderData.Color = currentColor;
                shaderData.Width = 1f;

                shaderData = ref drawer.GetShaderData();
                shaderData.Position = currentPosition + p2;
                shaderData.Color = currentColor;
                shaderData.Width = 1f;
            }
        }

        public static void Raycast(Vector3 origin, Vector3 direction, float distance = float.MaxValue,
            int layerMask = ~0)
        {
            if (Physics.Raycast(origin, direction, out var hit, distance, layerMask))
            {
                ReDraw.Line(origin, hit.point, Color.green, 2f);
                ReDraw.Ray(hit.point, hit.normal * 0.2f, Color.blue, 2f);
            }
            else
            {
                ReDraw.Ray(origin, direction * distance, Color.red, 2f);
            }
        }

        public static void SphereCast(Vector3 origin, Vector3 direction, float radius, float distance = float.MaxValue,
            int layerMask = ~0)
        {
            if (Physics.SphereCast(origin, radius, direction, out var hit, distance, layerMask))
            {
                ReDraw.Ray(origin, direction * hit.distance, Color.green, 2f);
                ReDraw.Sphere(origin + direction * hit.distance, Vector3.one * radius,
                    Color.green.WithAlpha(0.5f));
            }
            else
            {
                ReDraw.Ray(origin, direction * distance, Color.red, 2f);
            }
        }

        public static void BoxCast(Vector3 center, Vector3 direction, Vector3 halfExtents, Quaternion orientation,
            float distance = float.MaxValue, int layerMask = ~0)
        {
            if (Physics.BoxCast(center, halfExtents, direction, out var hit, orientation, distance, layerMask))
            {
                ReDraw.Ray(center, direction * hit.distance, Color.green, 2f);
                ReDraw.Cube(center + direction * hit.distance, orientation, halfExtents * 2,
                    Color.green.WithAlpha(0.5f));
            }
            else
            {
                ReDraw.Ray(center, direction * distance, Color.red, 2f);
            }
        }

        public static void Sprite(Sprite sprite, Vector3 pos, float scale)
        {
            if (ReGizmoResolver<ReGizmoSpritesDrawer>.TryGet(out var drawers))
            {
                ref var data = ref drawers.GetShaderData(sprite);

                data.Position = currentPosition + pos;
                data.Scale = sprite.pixelsPerUnit;

                Vector2 spriteSize = new Vector2(sprite.texture.width, sprite.texture.height);
                Rect spriteRect = sprite.textureRect;
                spriteRect.position /= spriteSize;
                spriteRect.size /= spriteSize;

                data.UVs = new Vector4(
                    spriteRect.xMin, spriteRect.yMin, spriteRect.xMax, spriteRect.yMax
                );
            }
        }
    }
}