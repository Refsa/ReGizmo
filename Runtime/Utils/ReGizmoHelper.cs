using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace ReGizmo
{
    internal static class ReGizmoHelpers
    {
        public const string ShaderFontSuperSamplingKeyword = "SDF_SS";

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

        public static ComputeShader LoadCompute(string name)
        {
            return LoadAssetByName<ComputeShader>(name);
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

        public static Font LoadFontByName(string name)
        {
            Font font = null;

#if UNITY_EDITOR
            string localResourcesFolder = ReGizmoHelpers.GetProjectResourcesPath() + @"Font/" + name + ".ttf";
            font = ReGizmoHelpers.LoadFont(localResourcesFolder);
            if (font == null)
                localResourcesFolder = ReGizmoHelpers.GetProjectResourcesPath() + @"Font/" + name + ".otf";
            font = ReGizmoHelpers.LoadFont(localResourcesFolder);
#else
            // HACK: Should probably just load the asset directly
            foreach (Font f in Resources.LoadAll("", typeof(Font)))
            {
                if (f.name == name)
                {
                    font = f;
                    break;
                }
            }
#endif

            return font;
        }

        public static T LoadAssetByName<T>(string name) where T : UnityEngine.Object
        {
            T obj = null;

#if UNITY_EDITOR
            var assets = AssetDatabase.FindAssets($"{name} t:{typeof(T).Name}");

            if (assets != null && assets.Length > 0)
            {
                obj = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(assets[0]));
            }
#else
            // HACK: Should probably just load the asset directly
            foreach (T t in Resources.LoadAll("", typeof(T)))
            {
                if (t.name == name)
                {
                    obj = t;
                    break;
                }
            }
#endif

            return obj;
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

        public static Texture2D LoadTextureByName(string name)
        {
            Texture2D texture = null;

#if UNITY_EDITOR
            string localResourcesFolder = GetProjectResourcesPath() + @"Textures/Icons/" + name + ".png";
            texture = LoadTexture(localResourcesFolder);
            if (texture == null) localResourcesFolder = GetProjectResourcesPath() + @"Textures/Icons/" + name + ".png";
            texture = LoadTexture(localResourcesFolder);
#else
            // HACK: Should probably just load the asset directly
            foreach (Texture2D t in Resources.LoadAll("", typeof(Texture2D)))
            {
                if (t.name == name)
                {
                    texture = t;
                    break;
                }
            }
#endif

            return texture;
        }

#if UNITY_EDITOR
        public static string GetProjectResourcesPath()
        {
            try
            {
                var dirs = System.IO.Directory.GetDirectories(Application.dataPath, @"*ReGizmo*",
                    System.IO.SearchOption.AllDirectories);

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