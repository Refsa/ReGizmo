using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal struct CircleDrawData
    {
        public Vector3 Position;
        public Vector3 Normal;
        public float Radius;
        public Vector4 Color;
        public int Flags;
    }

    internal class CircleDrawer : ReGizmoDrawer<CircleDrawData>
    {
        public CircleDrawer() : base()
        {
            material = new Material(ReGizmoHelpers.LoadShader("Hidden/ReGizmo/CircleShader"));
        }

        protected override void RenderInternal(CommandBuffer cmd, UniqueDrawData uniqueDrawData, bool depth)
        {
            uniqueDrawData.SetInstanceCount(1);
            uniqueDrawData.SetVertexCount(uniqueDrawData.DrawCount);

            if (depth)
            {
                cmd.DrawProceduralIndirect(
                    Matrix4x4.identity,
                    material, 1,
                    MeshTopology.Points,
                    uniqueDrawData.ArgsBuffer, 0,
                    uniqueDrawData.MaterialPropertyBlock
                );
            }
            else
            {
                cmd.DrawProceduralIndirect(
                    Matrix4x4.identity,
                    material, 0,
                    MeshTopology.Points,
                    uniqueDrawData.ArgsBuffer, 0,
                    uniqueDrawData.MaterialPropertyBlock
                );
            }
        }

        protected override void RenderWithPassInternal(CommandBuffer cmd, UniqueDrawData uniqueDrawData, int pass)
        {
            uniqueDrawData.SetInstanceCount(1);
            uniqueDrawData.SetVertexCount(uniqueDrawData.DrawCount);

            cmd.DrawProceduralIndirect(
                Matrix4x4.identity,
                material, pass,
                MeshTopology.Points,
                uniqueDrawData.ArgsBuffer, 0,
                uniqueDrawData.MaterialPropertyBlock
            );
        }
    }
}