using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ReGizmo
{
    internal static class ReGizmoHelpers
    {
        public static Material PrepareMaterial(string shaderName)
        {
            Shader shader = LoadShader(shaderName);
            Material material = new Material(shader);
            material.enableInstancing = true;

            return material;
        }

        public static Shader LoadShader(string name)
        {
            Shader shader = Shader.Find(name);

            if (shader == null)
                throw new System.ArgumentNullException($"Could not find shader of name {name}");

            return shader;
        }

        public static ComputeShader LoadCompute(string path)
        {
#if UNITY_EDITOR
            ComputeShader compute = AssetDatabase.LoadAssetAtPath(path, typeof(ComputeShader)) as ComputeShader;
#else
            ComputeShader compute = Resources.Load<ComputeShader>(path);
#endif
            return compute;
        }

        public static Font LoadFont(string path)
        {
#if UNITY_EDITOR
            Font font = AssetDatabase.LoadAssetAtPath(path, typeof(Font)) as Font;
#else
            Font font = Resources.Load<Font>(path);
#endif

            return font;
        }

        public static Texture2D LoadTexture(string path)
        {
#if UNITY_EDITOR
            Texture2D texture = AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D)) as Texture2D;
#else
            Texture2D texture = Resources.Load<Texture2D>(path);
#endif

            return texture;
        }

#if UNITY_EDITOR
        public static string GetProjectResourcesPath()
        {
            try
            {
                var dirs = System.IO.Directory.GetDirectories(Application.dataPath, @"*ReGizmo*", System.IO.SearchOption.AllDirectories);

                if (dirs.Length == 0) return "";

                string dir = dirs[0];

                int assetsStartIndex = dir.IndexOf("/Assets");
                if (assetsStartIndex == -1) return "";

                dir = dir.Substring(assetsStartIndex).TrimStart('/');

                return dir + "/Runtime/Resources/";
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }
            return "";
        }
#else
        public static string GetProjectResourcesPath()
        {
            return "";
        }
#endif
    }
}