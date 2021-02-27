
using ReGizmo.Utility;

namespace ReGizmo.Drawing
{
    internal class ReGizmoCubeDrawer : ReGizmoMeshDrawer
    {
        public ReGizmoCubeDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Cube();
            material = ReGizmoHelpers.PrepareMaterial("ReGizmo/DMIIDefault");
            renderArguments[0] = mesh.GetIndexCount(0);
        }
    }
}