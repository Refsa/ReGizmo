using UnityEditor;
using UnityEngine;

namespace ReGizmo.Editor
{
    public static class ReGizmoEditorUtils
    {
        public const string RuntimeDefineSymbol = "REGIZMO_RUNTIME";

        public static bool RuntimeEnabled => PlayerSettings
            .GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone).Contains(RuntimeDefineSymbol);

        public static void ToggleRuntimeScriptDefine()
        {
            string scriptingDefines = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);
            if (RuntimeEnabled)
            {
                scriptingDefines = scriptingDefines.Replace(RuntimeDefineSymbol, "");
            }
            else
            {
                scriptingDefines += ";" + RuntimeDefineSymbol;
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, scriptingDefines);
        }

        #region B64_Logo

        static Texture2D logoTexture;
        public static Texture2D LogoTexture = logoTexture ?? (LogoTexture = DecodeLogo());

        public static Texture2D DecodeLogo()
        {
            const string Logo =
                "iVBORw0KGgoAAAANSUhEUgAAAEEAAABBCAYAAACO98lFAAAAAXNSR0IArs4c6QAAAm5JREFUeJztmrFOwzAQhn8j1JlnyNSJTrwHG+orwMxcOneGV6jYeA8mmJj6CjB3MQNy66RJY/vOdxG6X6rSVo3P/vqfz04CmEwmk8k0OV19PnrtPjjN4DGAn+uNWl/UAvc5QAuEStBzKaABQjxgyhwgDcLmBAAXWoGnJIMAgwDAIABQmBj9B0arg1vI9kskWMrAhyQBpGqAvsHHgxoqkWPnceuyRqPdQeQOIP59aCsca8BgnxhjAG4BR+10tw1Kag2JFUIXAGfbNUGwQagJoK9dThAsECQA9LXPBYIMQRJAXxwOECQIGgD64lFBsKSDNADuuMUQatbtHIX4FDfYBgqFEKbigiCqG8wJMAgACiBMLRWCKClhTkClrXSqvv3m8F7TVmpOCLZ9vr1vfdaQajoEAOGoJRUIq1njAeDh7QXxEfOlihvUnPB00wA4AnB3S62uyEMILgD+QLgFXAuAghuyJ2XKOiEGsN7vTs+PAXxtxfpWVJlKgo0CCCoEQflzRNIhGQDQHrhQalSHkAUgSBhE8UItxX5FAGIlpgZ1P1PNCWQAgJgjSEv2oX+ABUCsM47g2NWy33dgBwAMOoJrv0HuZNyRsAoEGAHEigG8bg9fT+Lahv+AD6/YCVU0X/o4XtVYOVrNGrGOteIwTZZkG7X2Au+7Y8MVLNq64xXvNwqW2LFIE2N3Eqx5+/zklt9Y+cxwSTKEbq4PVYEuCCqMbhsthw2ByEyT7A3Qer9zqWWw9Nmj7POGBp2YJkW7wKDUMijy9FofiEQIxVebc9YBuXOFdN0vhrCaNb5kQVRlgMRSyVYd1KR0cdZkMplM/12/PhNfb7Vl5jYAAAAASUVORK5CYII=";

            Texture2D logoTexture = new Texture2D(65 * 3, 65 * 3, TextureFormat.ARGB32, 0, true);
            logoTexture.filterMode = FilterMode.Point;

            byte[] bytes = System.Convert.FromBase64String(Logo);

            ImageConversion.LoadImage(logoTexture, bytes, false);

            logoTexture.Apply();

            return logoTexture;
        }

        #endregion
    }
}