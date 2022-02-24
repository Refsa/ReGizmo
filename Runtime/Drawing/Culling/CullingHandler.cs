
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

        protected CullingDebug cullingDebug;

        static CullingHandler()
        {
            CullingCompute = ReGizmoHelpers.LoadCompute("CullCompute");
        }

        public CullingHandler()
        {
            cullingDebug = new CullingDebug();
        }

        public void SetData(CommandBuffer commandBuffer, CameraFrustum cameraFrustum)
        {
            commandBuffer.SetComputeVectorArrayParam(CullingCompute, "_CameraFrustum", cameraFrustum.FrustumPlanes);
            commandBuffer.SetComputeVectorParam(CullingCompute, "_CameraClips", cameraFrustum.ClippingPlanes);
            commandBuffer.SetComputeMatrixParam(CullingCompute, "_ViewMatrix", cameraFrustum.ViewMatrix);
            commandBuffer.SetComputeMatrixParam(CullingCompute, "_ProjectionMatrix", cameraFrustum.ProjectionMatrix);
            commandBuffer.SetComputeMatrixParam(CullingCompute, "_I_VP", cameraFrustum.InverseViewProjectionMatrix);
            commandBuffer.SetComputeMatrixParam(CullingCompute, "_VP", cameraFrustum.ViewProjectionMatrix);
            commandBuffer.SetComputeVectorParam(CullingCompute, "_ScreenParams", cameraFrustum.ScreenParams);
        }

        public abstract void PerformCulling<TShaderData>(
            CommandBuffer commandBuffer, int drawCount,
            ComputeBuffer argsBuffer, int argsBufferOffset,
            ComputeBuffer inputBuffer, ComputeBuffer outputBuffer
        ) where TShaderData : unmanaged;

        public void Dispose()
        {

        }

        protected static int GetKernelID(string name)
        {
            if (CullingCompute == null)
            {
                return 0;
            }

            return CullingCompute.FindKernel(name);
        }
    }
}