using ReGizmo.Utils;

namespace ReGizmo.Drawing
{
    internal class ReGizmoCubeDrawer : ReGizmoMeshDrawer
    {
        public ReGizmoCubeDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Cube();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }

    internal class ReGizmoSphereDrawer : ReGizmoMeshDrawer
    {
        public ReGizmoSphereDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Sphere();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }

    internal class ReGizmoQuadDrawer : ReGizmoMeshDrawer
    {
        public ReGizmoQuadDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Quad();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }

    internal class ReGizmoCylinderDrawer : ReGizmoMeshDrawer
    {
        public ReGizmoCylinderDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Cylinder();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }
    
    internal class ReGizmoCapsuleDrawer : ReGizmoMeshDrawer
    {
        public ReGizmoCapsuleDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Capsule();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }

    internal class ReGizmoConeDrawer : ReGizmoMeshDrawer
    {
        public ReGizmoConeDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Cone();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }

    internal class ReGizmoOctahedronDrawer : ReGizmoMeshDrawer
    {
        public ReGizmoOctahedronDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Octahedron();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }

    internal class ReGizmoPyramidDrawer : ReGizmoMeshDrawer
    {
        public ReGizmoPyramidDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Pyramid();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }

    internal class ReGizmoIcosahedronDrawer : ReGizmoMeshDrawer
    {
        public ReGizmoIcosahedronDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Icosahedron();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }
}