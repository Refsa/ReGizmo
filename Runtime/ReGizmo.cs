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

#if !UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
#endif
        public static void Initialize()
        {
            Debug.Log("########## Setting up ReGizmo ##########");
            Dispose();
            Setup();
        }

        public static void Setup()
        {
            ComputeBufferPool.Init();

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
                ReGizmoResolver<ReGizmoCapsuleDrawer>.Init(new ReGizmoCapsuleDrawer()),

                ReGizmoResolver<ReGizmoCustomMeshDrawer>.Init(new ReGizmoCustomMeshDrawer()),

                ReGizmoResolver<ReGizmoIconsDrawer>.Init(new ReGizmoIconsDrawer()),

                ReGizmoResolver<ReGizmoFontDrawer>.Init(
                    new ReGizmoFontDrawer(ReGizmoHelpers.LoadFontByName("MonoSpatial"))),
            };

            // if (Application.isPlaying)
#if !UNITY_EDITOR
            {
                SetupProxyObject();
            }
#endif

            isSetup = true;
            interrupted = false;
        }

        static void SetupProxyObject()
        {
            proxyObject = new GameObject("ReGizmoProxy");
            GameObject.DontDestroyOnLoad(proxyObject);

            var proxyComp = proxyObject.AddComponent<ReGizmoProxy>();

            proxyComp.inUpdate += OnUpdate;
            proxyComp.inDestroy += Dispose;
        }

        public static void Dispose()
        {
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
            activeCameras.Clear();
        }

        static HashSet<Camera> activeCameras = new HashSet<Camera>();

        public static void OnUpdate()
        {
            if (drawers == null || interrupted) return;

#if UNITY_EDITOR
            Profiler.BeginSample("ReGizmo::OnUpdate");
#endif

            activeCameras.Add(Camera.main);
            
#if UNITY_EDITOR
            activeCameras.Add(UnityEditor.SceneView.currentDrawingSceneView.camera);
#endif

            foreach (var drawer in drawers)
            {
                if (drawer.CurrentDrawCount() == 0) continue;

                if (interrupted)
                {
                    drawer.Clear();
                    break;
                }

                foreach (var camera in activeCameras)
                {
                    try
                    {
                        if (camera != null || !camera.isActiveAndEnabled) drawer.Render(camera);
                    }
                    catch
                    {
                    }
                }

                drawer.Clear();
            }

#if UNITY_EDITOR
            Profiler.EndSample();
#endif
        }
    }
}