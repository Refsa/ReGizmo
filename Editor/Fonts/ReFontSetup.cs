using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime;
using System.Runtime.InteropServices;
using ReGizmo.Core.Fonts;
using ReGizmo.Editor.MSDF;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using Debug = UnityEngine.Debug;

namespace ReGizmo.Editor
{
    internal class ReFontSetup : EditorWindow
    {
#if REGIZMO_DEV
        [MenuItem("Window/ReGizmo/SDF Font Setup")]
#endif
        public static void Open()
        {
            var window = ReFontSetup.GetWindow<ReFontSetup>();

            float height = 7f * EditorGUIUtility.singleLineHeight + 24f;
            window.minSize = new Vector2(400, height);
        }

        Font targetFont;
        ReSDFData targetData;

        MSDF.Format format;
        int size = 64;
        int pixelRange = 4;

        void OnGUI()
        {
            GUILayout.Label("ReGizmo Font Setup", EditorStyles.largeLabel);

            GUILayout.Space(24);

            targetFont = (Font)EditorGUILayout.ObjectField("Target Font", targetFont, typeof(Font), false);

            if (targetFont == null) return;

            format = (MSDF.Format)EditorGUILayout.EnumPopup("Type", format);
            size = EditorGUILayout.IntSlider("Size", size, 1, 1024);
            pixelRange = EditorGUILayout.IntSlider("Pixel Range", pixelRange, 1, 128);

            if (GUILayout.Button("Generate"))
            {
                targetData = CreateSDFAsset(targetFont, format, size, pixelRange);
            }
        }

        [MenuItem("Assets/Create/Create SDF", validate = true)]
        public static bool ValidateSelectedAsset(MenuCommand cmd)
        {
            int instanceID = Selection.activeInstanceID;
            string assetPath = AssetDatabase.GetAssetPath(instanceID);
            var obj = AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.Object));

            return obj != null && obj.GetType() == typeof(Font);
        }

        [MenuItem("Assets/Create/Create SDF", validate = false)]
        public static void RunCreateSDFAsset()
        {
            int instanceID = Selection.activeInstanceID;
            string assetPath = AssetDatabase.GetAssetPath(instanceID);
            var obj = AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.Object));

            if (obj is Font font)
            {
                CreateSDFAsset(font, Format.mtsdf, 128, 4);
            }
            else
            {
                throw new ArgumentException("Target asset is not a Font file");
            }
        }

        static ReSDFData CreateSDFAsset(Font targetFont, MSDF.Format format, int size, int pxRange)
        {
            string savePath =
                EditorUtility.SaveFolderPanel("Font Save Path", "SDF", "");
            string localSavePath = savePath.Replace(Application.dataPath, "Assets/") + "/";

            if (string.IsNullOrEmpty(savePath)) 
            {
                throw new DirectoryNotFoundException(savePath);
            }

            string atlasFileName = targetFont.name.Replace(" ", "_") + "_Atlas.png";
            string dataFileName = targetFont.name.Replace(" ", "_") + "_Atlas_Data.json";

            string atlasSavePath = localSavePath + atlasFileName;
            string atlasDataSavePath = localSavePath + dataFileName;
            string sdfAssetPath = localSavePath + targetFont.name.Replace(" ", "_") + ".asset";

            (Texture2D mainTex, TextAsset mainJson) = Generate(
                targetFont, format, size, pxRange,
                savePath, atlasFileName, dataFileName);

            ReSDFData sdfAsset = null;

            if (AssetDatabase.LoadAssetAtPath<ReSDFData>(sdfAssetPath) is ReSDFData current)
            {
                sdfAsset = current; 
                sdfAsset.Setup(mainTex, mainJson.text);
                ReGizmoEditorUtils.SaveAsset(sdfAsset);
            }
            else
            {
                sdfAsset = ScriptableObject.CreateInstance<ReSDFData>();
                sdfAsset.Setup(mainTex, mainJson.text);
                AssetDatabase.CreateAsset(sdfAsset, sdfAssetPath);
            }

            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(mainJson));
            AssetDatabase.Refresh();
            Core.ReGizmo.Reload();

            return sdfAsset;
        }

        static void CleanupOldAssets(string atlasSavePath, string atlasDataSavePath)
        {
            if (AssetDatabase.LoadAssetAtPath<Texture2D>(atlasSavePath) != null)
            {
                AssetDatabase.DeleteAsset(atlasSavePath);
            }

            if (AssetDatabase.LoadAssetAtPath<TextAsset>(atlasDataSavePath))
            {
                AssetDatabase.DeleteAsset(atlasDataSavePath);
            }
        }

        static (Texture2D, TextAsset) Generate(
            Font targetFont, MSDF.Format format, int size, int pxRange,
            string savePath, string atlasFileName, string dataFileName
            )
        {
            string atlasPath = savePath + "/" + atlasFileName;
            atlasPath = atlasPath.Replace(Application.dataPath, "Assets");
            string dataPath = savePath + "/" + dataFileName;
            dataPath = dataPath.Replace(Application.dataPath, "Assets");

            // TODO: Paths used in this needs to be changed
            using (var process = new Process())
            {
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.WorkingDirectory = savePath;
                // TODO: Hardcoded path
                process.StartInfo.FileName =
                    Application.dataPath.Replace("Assets", "Assets/ReGizmo/Editor/Fonts/msdf/msdf-atlas-gen.exe");
                process.StartInfo.CreateNoWindow = true;

                string fontAssetPath =
                    Application.dataPath.Replace("Assets", AssetDatabase.GetAssetPath(targetFont));
                process.StartInfo.Arguments =
                    $"-font \"{fontAssetPath}\" -type {format.ToString()} -pots -format png -size {size} -pxrange {pxRange} -imageout {atlasFileName} -json {dataFileName}";

                process.Start();
                process.WaitForExit();
            }

            AssetDatabase.Refresh();

            TextureImporter atlasImporter = (TextureImporter)AssetImporter.GetAtPath(atlasPath);
            atlasImporter.npotScale = TextureImporterNPOTScale.ToNearest;
            atlasImporter.wrapMode = TextureWrapMode.Clamp;
            atlasImporter.filterMode = FilterMode.Bilinear;
            atlasImporter.compressionQuality = 100;
            atlasImporter.textureCompression = TextureImporterCompression.Uncompressed;
            atlasImporter.isReadable = true;
            atlasImporter.mipmapEnabled = false;
            AssetDatabase.ImportAsset(atlasPath);
            AssetDatabase.Refresh();

            var atlasImage = AssetDatabase.LoadAssetAtPath<Texture2D>(atlasPath);
            var atlasData = AssetDatabase.LoadAssetAtPath<TextAsset>(dataPath);

            return (atlasImage, atlasData);
        }
    }
}