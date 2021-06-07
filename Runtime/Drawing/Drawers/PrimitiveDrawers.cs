using ReGizmo.Utils;

namespace ReGizmo.Drawing
{
    internal class CubeDrawer : MeshDrawer
    {
        public CubeDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Cube();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }

    internal class SphereDrawer : MeshDrawer
    {
        public SphereDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Sphere();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }

    internal class QuadDrawer : MeshDrawer
    {
        public QuadDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Quad();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }

    internal class CylinderDrawer : MeshDrawer
    {
        public CylinderDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Cylinder();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }
    
    internal class CapsuleDrawer : MeshDrawer
    {
        public CapsuleDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Capsule();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }

    internal class ConeDrawer : MeshDrawer
    {
        public ConeDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Cone();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }

    internal class OctahedronDrawer : MeshDrawer
    {
        public OctahedronDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Octahedron();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }

    internal class PyramidDrawer : MeshDrawer
    {
        public PyramidDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Pyramid();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }

    internal class IcosahedronDrawer : MeshDrawer
    {
        public IcosahedronDrawer() : base()
        {
            mesh = ReGizmoPrimitives.Icosahedron();
            material = ReGizmoHelpers.PrepareMaterial("Hidden/ReGizmo/Mesh");
        }
    }
}