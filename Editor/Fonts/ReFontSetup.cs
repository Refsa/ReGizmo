using System;
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
    public class ReFontSetup : EditorWindow
    {
        [MenuItem("Window/ReGizmo/SDF Font Setup")]
        public static void Open()
        {
            var window = ReFontSetup.GetWindow<ReFontSetup>();

            float height = 7f * EditorGUIUtility.singleLineHeight + 24f;
            window.minSize = new Vector2(400, height);
        }

        Font targetFont;

        MSDF.Format format;
        int size = 128;
        int pixelRange = 2;

        void OnGUI()
        {
            GUILayout.Label("ReGizmo Font Setup", EditorStyles.largeLabel);

            GUILayout.Space(24);

            targetFont = (Font) EditorGUILayout.ObjectField("Target Font", targetFont, typeof(Font), false);

            if (targetFont == null) return;

            format = (MSDF.Format) EditorGUILayout.EnumPopup("Type", format);
            size = EditorGUILayout.IntSlider("Size", size, 1, 1024);
            pixelRange = EditorGUILayout.IntSlider("Pixel Range", pixelRange, 1, 128);

            if (GUILayout.Button("Generate"))
            {
                CreateSDFAsset(targetFont, format, size, pixelRange);
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
        public static void CreateSDFAsset()
        {
            int instanceID = Selection.activeInstanceID;
            string assetPath = AssetDatabase.GetAssetPath(instanceID);
            var obj = AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.Object));

            if (obj is Font font)
            {
                CreateSDFAsset(font, Format.mtsdf, 128, 2);
            }
            else
            {
                throw new ArgumentException("Target asset is not a Font file");
            }
        }

        static void CreateSDFAsset(Font targetFont, MSDF.Format format, int size, int pxRange)
        {
            string savePath =
                EditorUtility.SaveFolderPanel("Font Save Path", "SDF", "");
            string localSavePath = savePath.Replace(Application.dataPath, "Assets/") + "/";

            if (string.IsNullOrEmpty(savePath))
            {
                return;
            }

            string atlasName = targetFont.name.Replace(" ", "_") + "_Atlas.png";
            string atlasDataName = targetFont.name.Replace(" ", "_") + "_Atlas_Data.json";

            string atlasSavePath = localSavePath + atlasName;
            string atlasDataSavePath = localSavePath + atlasDataName;
            string sdfAssetPath = localSavePath + targetFont.name.Replace(" ", "_") + ".asset";

            if (AssetDatabase.LoadAssetAtPath<Texture2D>(atlasSavePath) != null)
            {
                AssetDatabase.DeleteAsset(atlasSavePath);
            }

            if (AssetDatabase.LoadAssetAtPath<TextAsset>(atlasDataSavePath))
            {
                AssetDatabase.DeleteAsset(atlasDataSavePath);
            }

            using (var process = new Process())
            {
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.WorkingDirectory = savePath;
                process.StartInfo.FileName =
                    Application.dataPath.Replace("Assets", "Assets/ReGizmo/Editor/Fonts/msdf/msdf-atlas-gen.exe");
                process.StartInfo.CreateNoWindow = true;

                string fontAssetPath =
                    Application.dataPath.Replace("Assets", AssetDatabase.GetAssetPath(targetFont));
                process.StartInfo.Arguments =
                    $"-font \"{fontAssetPath}\" -type {format.ToString()} -pots -format png -size {size} -pxrange {pxRange} -imageout {atlasName} -json {atlasDataName}";

                process.Start();
                process.WaitForExit();
            }

            AssetDatabase.Refresh();


            TextureImporter atlasImporter = (TextureImporter) AssetImporter.GetAtPath(atlasSavePath);
            atlasImporter.npotScale = TextureImporterNPOTScale.ToLarger;
            atlasImporter.wrapMode = TextureWrapMode.Clamp;
            atlasImporter.filterMode = FilterMode.Trilinear;
            atlasImporter.compressionQuality = 100;
            atlasImporter.textureCompression = TextureImporterCompression.Uncompressed;
            AssetDatabase.ImportAsset(atlasSavePath);
            AssetDatabase.Refresh();

            var atlasImage = AssetDatabase.LoadAssetAtPath<Texture2D>(atlasSavePath);
            var atlasData = AssetDatabase.LoadAssetAtPath<TextAsset>(atlasDataSavePath);
  
            ReSDFData sdfAsset = null;

            if (AssetDatabase.LoadAssetAtPath<ReSDFData>(sdfAssetPath) is ReSDFData current)
            {
                sdfAsset = current;
                sdfAsset.Setup(atlasImage, atlasData.text);
            }
            else
            {
                sdfAsset = ScriptableObject.CreateInstance<ReSDFData>();
                sdfAsset.Setup(atlasImage, atlasData.text);
                AssetDatabase.CreateAsset(sdfAsset, sdfAssetPath);
            }

            AssetDatabase.DeleteAsset(atlasDataSavePath);
            AssetDatabase.Refresh();
        }
    }
}