

using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Rendering;

namespace ReGizmo.Core
{
    public static class ReGizmo
    {
        static bool isSetup = false;
        static GameObject proxyObject;

        static List<IReGizmoDrawer> drawers;
        static bool interrupted = false;

        static CommandBuffer commandBuffer;

        [RuntimeInitializeOnLoadMethod]
        public static void Initialize()
        {
            Dispose();
            Setup();
        }

        public static void Setup()
        {
            commandBuffer = new CommandBuffer();

            string assetPath = ReGizmoHelpers.GetProjectResourcesPath();

            drawers = new List<IReGizmoDrawer>()
            {
                ReGizmoResolver<ReGizmoLineDrawer>.Init(new ReGizmoLineDrawer()),

                ReGizmoResolver<ReGizmoCubeDrawer>.Init(new ReGizmoCubeDrawer()),
                ReGizmoResolver<ReGizmoSphereDrawer>.Init(new ReGizmoSphereDrawer()),
                ReGizmoResolver<ReGizmoConeDrawer>.Init(new ReGizmoConeDrawer()),
                ReGizmoResolver<ReGizmoCylinderDrawer>.Init(new ReGizmoCylinderDrawer()),
                ReGizmoResolver<ReGizmoIcosahedronDrawer>.Init(new ReGizmoIcosahedronDrawer()),
                ReGizmoResolver<ReGizmoOctahedronDrawer>.Init(new ReGizmoOctahedronDrawer()),
                ReGizmoResolver<ReGizmoQuadDrawer>.Init(new ReGizmoQuadDrawer()),
                ReGizmoResolver<ReGizmoPyramidDrawer>.Init(new ReGizmoPyramidDrawer()),

                ReGizmoResolver<ReGizmoCustomMeshDrawer>.Init(new ReGizmoCustomMeshDrawer()),

                ReGizmoResolver<ReGizmoIconsDrawer>.Init(new ReGizmoIconsDrawer()),

                ReGizmoResolver<ReGizmoFontDrawer>.Init(new ReGizmoFontDrawer(ReGizmoHelpers.LoadFontByName("MonoSpatial"))),
            };

            if (Application.isPlaying)
            {
                SetupProxyObject();
            }

            isSetup = true;
            interrupted = false;
        }

        static void SetupProxyObject()
        {
            proxyObject = new GameObject("ReGizmoProxy");
            GameObject.DontDestroyOnLoad(proxyObject);

            var proxyComp = proxyObject.AddComponent<ReGizmoProxy>();

            proxyComp.inUpdate += OnUpdate;
        }

        public static void Dispose()
        {
            commandBuffer?.Dispose();

            if (drawers == null) return;

            foreach (var drawer in drawers)
            {
                drawer.Dispose();
            }

            drawers = null;
        }

        public static void Interrupt()
        {
            interrupted = true;
        }

        public static void OnUpdate()
        {
            if (drawers == null || interrupted) return;

#if UNITY_EDITOR
            Profiler.BeginSample("ReGizmo::OnUpdate");
#endif

            Camera gameCamera = Camera.main;
#if UNITY_EDITOR
            Camera sceneViewCamera = UnityEditor.SceneView.lastActiveSceneView.camera;
#endif

            foreach (var drawer in drawers)
            {
                if (interrupted) break;
                try
                {
#if UNITY_EDITOR
                    if (sceneViewCamera != null)
                    {
                        drawer.Render(sceneViewCamera);
                    }
#endif

                    drawer.Render(gameCamera);
                }
                catch { }

                drawer.Clear();
            }

#if UNITY_EDITOR
            Profiler.EndSample();
#endif
        }
    }
}