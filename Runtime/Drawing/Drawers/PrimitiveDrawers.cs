using ReGizmo.Utils;

namespace ReGizmo.Drawing
{
    internal class CubeDrawer : MeshDrawer
    {
        public CubeDrawer() : base(ReGizmoPrimitives.Cube()) { }
    }

    internal class SphereDrawer : MeshDrawer
    {
        public SphereDrawer() : base(ReGizmoPrimitives.Sphere()) { }
    }

    internal class QuadDrawer : MeshDrawer
    {
        public QuadDrawer() : base(ReGizmoPrimitives.Quad()) { }
    }

    internal class CylinderDrawer : MeshDrawer
    {
        public CylinderDrawer() : base(ReGizmoPrimitives.Cylinder()) { }
    }

    internal class CapsuleDrawer : MeshDrawer
    {
        public CapsuleDrawer() : base(ReGizmoPrimitives.Capsule()) { }
    }

    internal class ConeDrawer : MeshDrawer
    {
        public ConeDrawer() : base(ReGizmoPrimitives.Cone()) { }
    }

    internal class OctahedronDrawer : MeshDrawer
    {
        public OctahedronDrawer() : base(ReGizmoPrimitives.Octahedron()) { }
    }

    internal class PyramidDrawer : MeshDrawer
    {
        public PyramidDrawer() : base(ReGizmoPrimitives.Pyramid()) { }
    }

    internal class IcosahedronDrawer : MeshDrawer
    {
        public IcosahedronDrawer() : base(ReGizmoPrimitives.Icosahedron()) { }
    }
}