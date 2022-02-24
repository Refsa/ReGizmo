using UnityEngine;
using ReGizmo.Drawing;

namespace ReGizmo.Samples
{
    public class DrawWireframeMesh : MonoBehaviour
    {
        [SerializeField] Mesh mesh;

        void OnDrawGizmosSelected()
        {
            if (mesh == null) return;
            using (new TransformScope(this.transform))
            {
                ReDraw.WireframeMesh(mesh);
            }
        }
    }
}