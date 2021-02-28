using UnityEditor;
using ReGizmo;
using UnityEngine;
using System.IO;

namespace ReGizmo.Generator
{
    public static class GeneratorEditor
    {
        static readonly string OUTPUT_PATH = GetOutputPath();
        static readonly string TEMPLATE_PATH = GetTemplatesPath();

        [MenuItem("ReGizmo/Generate Overrides")]
        public static void GenerateOverrides()
        {
            /* var generators = new DrawGenerator[]{
                new CubeDrawGenerator(),
            };

            foreach (var gen in generators)
            {
                gen.Generate(OUTPUT_PATH);
            } */

            GenerateMeshDrawerOverrides();
            // GenerateCustomMeshDrawerOverrides();
            AssetDatabase.Refresh();
        }

        static void GenerateCustomMeshDrawerOverrides()
        {
            string meshTemplate = File.ReadAllText(TEMPLATE_PATH + "MeshDrawingTemplate.txt");

            string content = meshTemplate
                .Replace("$NAME", "Mesh")
                .Replace("$DRAWER_NAME", "ReGizmoCustomMeshDrawer")
                .Replace("$PREFIX_PARAMS", "Mesh mesh, ");
            File.WriteAllText(OUTPUT_PATH + $"MeshDraw.generated.cs", content);
        }

        static void GenerateMeshDrawerOverrides()
        {
            string meshTemplate = File.ReadAllText(TEMPLATE_PATH + "MeshDrawingTemplate.txt");

            (string, string)[] targets = new (string, string)[]
            {
                ("Cube", "ReGizmoCubeDrawer"),
                ("Sphere", "ReGizmoSphereDrawer"),
                ("Quad", "ReGizmoQuadDrawer"),
                ("Cylinder", "ReGizmoCylinderDrawer"),
                ("Cone", "ReGizmoConeDrawer"),
                ("Octahedron", "ReGizmoOctahedronDrawer"),
                ("Pyramid", "ReGizmoPyramidDrawer"),
                ("Icosahedron", "ReGizmoIcosahedronDrawer"),
            };

            foreach (var target in targets)
            {
                string content = meshTemplate
                    .Replace("$NAME", target.Item1)
                    .Replace("$DRAWER_NAME", target.Item2)
                    .Replace("$PREFIX_PARAMS", "");
                File.WriteAllText(OUTPUT_PATH + $"{target.Item1}Draw.generated.cs", content);
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

        static string GetTemplatesPath()
        {
            try
            {
                var dirs = System.IO.Directory.GetDirectories(Application.dataPath, @"*ReGizmo*", System.IO.SearchOption.AllDirectories);

                if (dirs.Length == 0) return "";

                string dir = dirs[0];

                int assetsStartIndex = dir.IndexOf("/Assets");
                if (assetsStartIndex == -1) return "";

                dir = dir.Substring(assetsStartIndex).TrimStart('/');

                return dir + "/Editor/Generator/Templates/";
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }
            return "";
        }
    }
}