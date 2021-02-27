using ReGizmo.Core;
using UnityEditor;
using UnityEngine;

namespace ReGizmo.Editor
{
    [InitializeOnLoad]
    static class ReGizmoEditor
    {
        static ReGizmoEditor()
        {
            ReGizmo.Core.ReGizmo.Initialize();

            Setup();
        }

        static void Setup()
        {
            DeAttachEventHooks();
            AttachEventHooks();
        }

        static void AttachEventHooks()
        {
            EditorApplication.playModeStateChanged += OnPlaymodeChanged;
            AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReloaded;

            if (!Application.isPlaying)
            {
                EditorApplication.update += OnUpdate;
            }
        }

        static void DeAttachEventHooks()
        {
            EditorApplication.playModeStateChanged -= OnPlaymodeChanged;
            AssemblyReloadEvents.beforeAssemblyReload -= OnBeforeAssemblyReloaded;

            EditorApplication.update -= OnUpdate;
        }

        static void OnPlaymodeChanged(PlayModeStateChange change)
        {
            if (change == PlayModeStateChange.ExitingEditMode)
            {
                EditorApplication.update -= OnUpdate;
            }
            else if (change == PlayModeStateChange.EnteredEditMode)
            {
                Setup();
                ReGizmo.Core.ReGizmo.Initialize();
            }
        }

        static void OnBeforeAssemblyReloaded()
        {
            ReGizmo.Core.ReGizmo.Dispose();
        }

        static void OnUpdate()
        {
            ReGizmo.Core.ReGizmo.OnUpdate();
        }
    }
}