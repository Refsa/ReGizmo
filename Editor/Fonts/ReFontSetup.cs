using System;
using System.Diagnostics;
using System.IO;
using System.Runtime;
using System.Runtime.InteropServices;
using ReGizmo.Core.Fonts;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using Debug = UnityEngine.Debug;

namespace ReGizmo.Editor
{
    public class ReFontSetup : EditorWindow
    {
        [MenuItem("ReGizmo/FontSetup")]
        public static void Open()
        {
            var window = ReFontSetup.GetWindow<ReFontSetup>();
        }

        Font targetFont;

        void OnGUI()
        {
            GUILayout.Label("ReGizmo Font Setup", EditorStyles.largeLabel);

            GUILayout.Space(24);

            targetFont = (Font) EditorGUILayout.ObjectField("Target Font", targetFont, typeof(Font), false);

            if (targetFont != null && GUILayout.Button("Generate"))
            {
                string savePath =
                    EditorUtility.SaveFolderPanel("Font Save Path", "SDF", "");

                if (string.IsNullOrEmpty(savePath))
                {
                    return;
                }

                string atlasName = targetFont.name + "_Atlas.png";
                string atlasDataName = targetFont.name + "_Atlas_Data.json";

                using (var process = new Process())
                {
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.WorkingDirectory = savePath;
                    process.StartInfo.FileName =
                        Application.dataPath.Replace("Assets", "Assets/ReGizmo/Editor/Fonts/msdf-atlas-gen.exe");
                    process.StartInfo.CreateNoWindow = true;

                    string fontAssetPath =
                        Application.dataPath.Replace("Assets", AssetDatabase.GetAssetPath(targetFont));
                    process.StartInfo.Arguments =
                        $"-font {fontAssetPath} -type mtsdf -format png -scanline -size 128 -imageout {atlasName} -json {atlasDataName}";

                    process.Start();
                    process.WaitForExit();
                }

                AssetDatabase.Refresh();

                string localSavePath = savePath.Replace(Application.dataPath, "Assets/") + "/";

                TextureImporter atlasImporter = (TextureImporter) AssetImporter.GetAtPath(localSavePath + atlasName);
                atlasImporter.npotScale = TextureImporterNPOTScale.ToLarger;
                atlasImporter.wrapMode = TextureWrapMode.Clamp;
                atlasImporter.filterMode = FilterMode.Trilinear;
                atlasImporter.compressionQuality = 100;
                atlasImporter.textureCompression = TextureImporterCompression.Uncompressed;
                AssetDatabase.ImportAsset(localSavePath + atlasName);
                AssetDatabase.Refresh();

                var atlasImage = AssetDatabase.LoadAssetAtPath<Texture2D>(localSavePath + atlasName);
                var atlasData = AssetDatabase.LoadAssetAtPath<TextAsset>(localSavePath + atlasDataName);

                var sdfAsset = ScriptableObject.CreateInstance<ReSDFData>();
                sdfAsset.Setup(atlasImage, atlasData.text);

                string sdfAssetPath = localSavePath + targetFont.name + ".asset";
                
                AssetDatabase.CreateAsset(sdfAsset, sdfAssetPath);
                AssetDatabase.DeleteAsset(localSavePath + atlasDataName);
                AssetDatabase.Refresh();
            }
        }
    }
}