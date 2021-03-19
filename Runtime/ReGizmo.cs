using System;
using System.Collections.Generic;
using System.Diagnostics;
using ReGizmo.Core.Fonts;
using ReGizmo.Drawing;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Rendering;
using Debug = UnityEngine.Debug;

namespace ReGizmo.Core
{
    public static class ReGizmo
    {
        static bool isSetup = false;
        static GameObject proxyObject;

        static List<IReGizmoDrawer> drawers;
        static HashSet<Camera> activeCameras;

        static bool interrupted;
        static bool isActive;
        static bool shouldDispose;
        static bool shouldReset;

        public static event Action wasDisposed;
        public static bool IsSetup => isSetup;

#if !UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
#endif
        public static void Initialize()
        {
            if (isSetup) return;
#if !UNITY_EDITOR && !REGIZMO_RUNTIME
            throw new System.InvalidOperationException("ReGizmo runtime is not enabled!");
#endif

#if UNITY_EDITOR || REGIZMO_RUNTIME
            ReGizmoSettings.Load();

            Dispose();
            Setup();
            activeCameras = new HashSet<Camera>();
            isActive = true;
#endif
        }

        public static void RunDispose()
        {
            shouldDispose = true;
            interrupted = true;
        }

        public static void Reload()
        {
            Interrupt();
            shouldReset = true;
        }

        public static void SetActive(bool state)
        {
            isActive = state;
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
                ReGizmoResolver<ReGizmoCustomMeshWireframeDrawer>.Init(new ReGizmoCustomMeshWireframeDrawer()),

                ReGizmoResolver<ReGizmoIconsDrawer>.Init(new ReGizmoIconsDrawer()),

                ReGizmoResolver<ReGizmoSDFFontDrawer>.Init(
                    new ReGizmoSDFFontDrawer(ReGizmoSettings.SDFFont)),

                ReGizmoResolver<ReGizmoFontDrawer>.Init(
                    new ReGizmoFontDrawer(ReGizmoSettings.Font)),
            };

            if (Application.isPlaying)
//#if !UNITY_EDITOR
            {
                SetupProxyObject();
            }
//#endif

            isSetup = true;
            interrupted = false;
        }

        static void SetupProxyObject()
        {
            ReGizmoProxy proxyComp = null;
            if (GameObject.FindObjectOfType<ReGizmoProxy>() is ReGizmoProxy proxy)
            {
                proxyObject = proxy.gameObject;
                proxyComp = proxy;
            }
            else
            {
                proxyObject = new GameObject("ReGizmoProxy");
                proxyComp = proxyObject.AddComponent<ReGizmoProxy>();
            }

            GameObject.DontDestroyOnLoad(proxyObject);

            proxyComp.inUpdate += OnUpdate;
            proxyComp.inDestroy += Dispose;

            proxyComp.inEnable += () => SetActive(true);
            proxyComp.inDisable += () => SetActive(false);
        }

        public static void Dispose()
        {
            if (drawers == null) return;

            foreach (var drawer in drawers)
            {
                drawer.Dispose();
            }

            drawers = null;

            wasDisposed?.Invoke();
        }

        public static void Interrupt()
        {
            interrupted = true;
            activeCameras.Clear();
        }

        public static void OnUpdate()
        {
            if (drawers == null) return;
            if (!isActive)
            {
                foreach (var drawer in drawers) drawer.Clear();
                return;
            }

#if UNITY_EDITOR
            Profiler.BeginSample("ReGizmo::OnUpdate");
#endif

            if (Application.isPlaying)
            {
                activeCameras.Add(Camera.main);
            }

#if UNITY_EDITOR
            if (UnityEditor.SceneView.lastActiveSceneView != null)
            {
                activeCameras.Add(UnityEditor.SceneView.lastActiveSceneView.camera);
            }
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
                        if (camera != null) drawer.Render(camera);
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }
                }

                drawer.Clear();
            }

            if (shouldDispose)
            {
                Dispose();
                shouldDispose = false;
            }

            if (shouldReset)
            {
                shouldReset = false;
                Dispose();
                Setup();
            }

            interrupted = false;

#if UNITY_EDITOR
            Profiler.EndSample();
#endif
        }
    }
}