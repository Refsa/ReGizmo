using ReGizmo.Utils;

namespace ReGizmo.Drawing
{
    internal class ReGizmoCubeDrawer : MeshDrawer
    {
        public ReGizmoCubeDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Cube();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }

    internal class ReGizmoSphereDrawer : MeshDrawer
    {
        public ReGizmoSphereDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Sphere();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }

    internal class ReGizmoQuadDrawer : MeshDrawer
    {
        public ReGizmoQuadDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Quad();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }

    internal class ReGizmoCylinderDrawer : MeshDrawer
    {
        public ReGizmoCylinderDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Cylinder();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }
    
    internal class ReGizmoCapsuleDrawer : MeshDrawer
    {
        public ReGizmoCapsuleDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Capsule();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }

    internal class ReGizmoConeDrawer : MeshDrawer
    {
        public ReGizmoConeDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Cone();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }

    internal class ReGizmoOctahedronDrawer : MeshDrawer
    {
        public ReGizmoOctahedronDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Octahedron();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }

    internal class ReGizmoPyramidDrawer : MeshDrawer
    {
        public ReGizmoPyramidDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Pyramid();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }

    internal class ReGizmoIcosahedronDrawer : MeshDrawer
    {
        public ReGizmoIcosahedronDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Icosahedron();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }
}