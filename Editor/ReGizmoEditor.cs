using ReGizmo.Drawing;
using UnityEditor;
using UnityEngine;

namespace ReGizmo.Editor
{
    [InitializeOnLoad]
    static class ReGizmoEditor
    {
        static ReGizmoEditor()
        {
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
            AssemblyReloadEvents.afterAssemblyReload += OnAfterAssemblyReloaded;

            if (!Application.isPlaying)
            {
                EditorApplication.update += OnUpdate;
            }
        }

        static void DeAttachEventHooks()
        {
            EditorApplication.playModeStateChanged -= OnPlaymodeChanged;
            AssemblyReloadEvents.beforeAssemblyReload -= OnBeforeAssemblyReloaded;
            AssemblyReloadEvents.afterAssemblyReload -= OnAfterAssemblyReloaded;

            EditorApplication.update -= OnUpdate;
        }

        static void OnPlaymodeChanged(PlayModeStateChange change)
        {
            if (change == PlayModeStateChange.ExitingPlayMode || change == PlayModeStateChange.ExitingEditMode)
            {
                ReDraw.Dispose();
            }
            else if (change == PlayModeStateChange.EnteredEditMode)
            { 
                Setup();
                ReDraw.Setup();
            }
        }

        static void OnBeforeAssemblyReloaded()
        {
            ReDraw.Dispose();
        }

        static void OnAfterAssemblyReloaded()
        {

        }

        static void OnUpdate()
        {
            ReDraw.OnUpdate();
        }
    }
}