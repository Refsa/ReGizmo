
using ReGizmo.Utility;

namespace ReGizmo.Drawing
{
    internal class ReGizmoCubeDrawer : ReGizmoMeshDrawer
    {
        public ReGizmoCubeDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Cube();
            material = ReGizmoHelpers.PrepareMaterial("ReGizmo/DMIIDepthSort");
            renderArguments[0] = mesh.GetIndexCount(0);
        }
    }

    internal class ReGizmoSphereDrawer : ReGizmoMeshDrawer
    {
        public ReGizmoSphereDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Sphere();
            material = ReGizmoHelpers.PrepareMaterial("ReGizmo/DMIIDepthSort");
            renderArguments[0] = mesh.GetIndexCount(0);
        }
    }

    internal class ReGizmoQuadDrawer : ReGizmoMeshDrawer
    {
        public ReGizmoQuadDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Quad();
            material = ReGizmoHelpers.PrepareMaterial("ReGizmo/DMIIDepthSort");
            renderArguments[0] = mesh.GetIndexCount(0);
        }
    }

    internal class ReGizmoCylinderDrawer : ReGizmoMeshDrawer
    {
        public ReGizmoCylinderDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Cylinder();
            material = ReGizmoHelpers.PrepareMaterial("ReGizmo/DMIIDepthSort");
            renderArguments[0] = mesh.GetIndexCount(0);
        }
    }

    internal class ReGizmoConeDrawer : ReGizmoMeshDrawer
    {
        public ReGizmoConeDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Cone();
            material = ReGizmoHelpers.PrepareMaterial("ReGizmo/DMIIDepthSort");
            renderArguments[0] = mesh.GetIndexCount(0);
        }
    }

    internal class ReGizmoOctahedronDrawer : ReGizmoMeshDrawer
    {
        public ReGizmoOctahedronDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Octahedron();
            material = ReGizmoHelpers.PrepareMaterial("ReGizmo/DMIIDepthSort");
            renderArguments[0] = mesh.GetIndexCount(0);
        }
    }

    internal class ReGizmoPyramidDrawer : ReGizmoMeshDrawer
    {
        public ReGizmoPyramidDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Pyramid();
            material = ReGizmoHelpers.PrepareMaterial("ReGizmo/DMIIDepthSort");
            renderArguments[0] = mesh.GetIndexCount(0);
        }
    }

    internal class ReGizmoIcosahedronDrawer : ReGizmoMeshDrawer
    {
        public ReGizmoIcosahedronDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Icosahedron();
            material = ReGizmoHelpers.PrepareMaterial("ReGizmo/DMIIDepthSort");
            renderArguments[0] = mesh.GetIndexCount(0);
        }
    }
}