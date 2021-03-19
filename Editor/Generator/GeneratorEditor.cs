using UnityEditor;
using ReGizmo;
using UnityEngine;
using System.IO;

namespace ReGizmo.Generator
{
    public static class GeneratorEditor
    {
        static readonly string OUTPUT_PATH = GetOutputPath();

        [MenuItem("Window/ReGizmo/Dev/Generate Overrides")]
        public static void GenerateOverrides()
        {
            GenerateMeshDrawerOverrides();

            new CustomMeshDrawGenerator().Generate(OUTPUT_PATH);

            new TextDrawGenerator().Generate(OUTPUT_PATH);
            new TextSDFDrawGenerator().Generate(OUTPUT_PATH);

            // new LineDrawGenerator().Generate(OUTPUT_PATH);
            new RayDrawGenerator().Generate(OUTPUT_PATH);

            new IconDrawGenerator().Generate(OUTPUT_PATH);

            AssetDatabase.Refresh();
        }

        static void GenerateMeshDrawerOverrides()
        {
            MeshDrawGenerator[] targets = new MeshDrawGenerator[]
            {
                new MeshDrawGenerator("Cube", "ReGizmoCubeDrawer"),
                new MeshDrawGenerator("Sphere", "ReGizmoSphereDrawer"),
                new MeshDrawGenerator("Quad", "ReGizmoQuadDrawer"),
                new MeshDrawGenerator("Cylinder", "ReGizmoCylinderDrawer"),
                new MeshDrawGenerator("Capsule", "ReGizmoCapsuleDrawer"),
                new MeshDrawGenerator("Cone", "ReGizmoConeDrawer"),
                new MeshDrawGenerator("Octahedron", "ReGizmoOctahedronDrawer"),
                new MeshDrawGenerator("Pyramid", "ReGizmoPyramidDrawer"),
                new MeshDrawGenerator("Icosahedron", "ReGizmoIcosahedronDrawer"),
            };

            foreach (var target in targets)
            {
                target.Generate(OUTPUT_PATH);
            }
        }

        static string GetOutputPath()
        {
            try
            {
                var dirs = System.IO.Directory.GetDirectories(Application.dataPath, @"*ReGizmo*", System.IO.SearchOption.AllDirectories);

                if (dirs.Length == 0) return "";

                string dir = dirs[0];

                int assetsStartIndex = dir.IndexOf("/Assets");
                if (assetsStartIndex == -1) return "";

                dir = dir.Substring(assetsStartIndex).TrimStart('/');

                return dir + "/Runtime/Generated/";
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }
            return "";
        }
    }
}