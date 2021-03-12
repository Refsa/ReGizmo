using System;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace ReGizmo.Editor
{
    internal class BuildHook : IPostprocessBuildWithReport, IPreprocessBuildWithReport
    {
        public int callbackOrder { get; } = 10000;

        public static event Action onBeforeBuild;
        public static event Action onAfterBuild;

        public void OnPostprocessBuild(BuildReport report)
        {
            onAfterBuild?.Invoke();
        }

        public void OnPreprocessBuild(BuildReport report)
        {
            onBeforeBuild?.Invoke();
        }
    }
}