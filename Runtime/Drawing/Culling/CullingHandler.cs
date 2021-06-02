
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

        protected int[] drawCounter;
        protected ComputeBuffer countCopyBuffer;

        static CullingHandler()
        {
            CullingCompute = ReGizmoHelpers.LoadCompute("Assets/ReGizmo/Runtime/Resources/Compute/CullCompute.compute");
        }

        public CullingHandler()
        {
            drawCounter = new int[1] { 0 };
            countCopyBuffer = ComputeBufferPool.Get(1, sizeof(int), ComputeBufferType.IndirectArguments);
        }

        public void SetData(Vector4[] cameraFrustum, Vector2 clippingPlanes)
        {
            CullingCompute.SetVectorArray("_CameraFrustum", cameraFrustum);
            CullingCompute.SetVector("_CameraClips", clippingPlanes);
        }

        public abstract int PerformCulling<TShaderData>(int drawCount, ComputeBuffer inputBufer, ComputeBuffer outputBuffer)
            where TShaderData : unmanaged;

        public void Dispose()
        {
            ComputeBufferPool.Free(countCopyBuffer);
        }
    }
}