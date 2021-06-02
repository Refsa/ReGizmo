
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

        public void SetData(CameraFrustum cameraFrustum)
        {
            CullingCompute.SetVectorArray("_CameraFrustum", cameraFrustum.FrustumPlanes);
            CullingCompute.SetVector("_CameraClips", cameraFrustum.ClippingPlanes);
            CullingCompute.SetMatrix("_ViewMatrix", cameraFrustum.ViewMatrix);
            CullingCompute.SetMatrix("_ProjectionMatrix", cameraFrustum.ProjectionMatrix);
            CullingCompute.SetMatrix("_I_VP", cameraFrustum.InverseViewProjectionMatrix);
        }

        public abstract void PerformCulling<TShaderData>(int drawCount, ComputeBuffer argsBuffer, int argsBufferOffset, ComputeBuffer inputBufer, ComputeBuffer outputBuffer)
            where TShaderData : unmanaged;

        public void Dispose()
        {
            
        }
    }
}