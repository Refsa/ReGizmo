
using ReGizmo.Core;
using ReGizmo.Utils;
using UnityEngine;
using UnityEngine.Rendering;

namespace ReGizmo.Drawing
{
    internal class CullingData
    {
        public int KernelID;
        public string InputName;
        public string OutputName;
    }

    internal abstract class CullingHandler : System.IDisposable
    {
        public static readonly ComputeShader CullingCompute;

        static CullingHandler()
        {
            CullingCompute = ReGizmoHelpers.LoadCompute("CullCompute");
        }

        public CullingHandler()
        {

        }

        public void SetData(CommandBuffer commandBuffer, CameraFrustum cameraFrustum)
        {
            commandBuffer.SetComputeVectorArrayParam(CullingCompute, "_CameraFrustum", cameraFrustum.FrustumPlanes);
            commandBuffer.SetComputeVectorParam(CullingCompute, "_CameraClips", cameraFrustum.ClippingPlanes);
            commandBuffer.SetComputeMatrixParam(CullingCompute, "_ViewMatrix", cameraFrustum.ViewMatrix);
            commandBuffer.SetComputeMatrixParam(CullingCompute, "_ProjectionMatrix", cameraFrustum.ProjectionMatrix);
            commandBuffer.SetComputeMatrixParam(CullingCompute, "_I_VP", cameraFrustum.InverseViewProjectionMatrix);
        }

        public abstract void PerformCulling<TShaderData>(CommandBuffer commandBuffer, int drawCount, ComputeBuffer argsBuffer, int argsBufferOffset, ComputeBuffer inputBufer, ComputeBuffer outputBuffer)
            where TShaderData : unmanaged;

        public void Dispose()
        {

        }
    }
}