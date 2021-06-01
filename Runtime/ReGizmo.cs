using System;
using System.Collections.Generic;
using ReGizmo.Drawing;
using ReGizmo.Utils;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Rendering;
using Debug = UnityEngine.Debug;

#if RG_URP
using UnityEngine.Rendering.Universal;
#elif RG_HDRP
using UnityEngine.Rendering.HighDefinition;
#endif

namespace ReGizmo.Core
{
    public static class ReGizmo
    {
#if RG_LEGACY
        const CameraEvent CAMERA_EVENT = CameraEvent.AfterForwardAlpha;
#endif

        static bool isSetup = false;
        static GameObject proxyObject;

        static List<IReGizmoDrawer> drawers;
        static HashSet<Camera> activeCameras;

        static CommandBufferStack drawBuffers;

        static bool interrupted;
        static bool isActive;
        static bool shouldReset;

        public static bool IsSetup => isSetup;

#if !UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
#endif
        public static void Initialize()
        {
#if !UNITY_EDITOR && !REGIZMO_RUNTIME
            throw new System.InvalidOperationException("ReGizmo runtime is not enabled!");
#endif

#if UNITY_EDITOR || REGIZMO_RUNTIME
            ReGizmoSettings.Load();

            Setup();
            activeCameras = new HashSet<Camera>();
            isActive = true;
#endif
        }

        public static void Reload()
        {
            Interrupt();
            shouldReset = true;
        }

        public static void Interrupt()
        {
            interrupted = true;
            activeCameras?.Clear();
        }

        public static void SetActive(bool state)
        {
            isActive = state;

            if (isActive)
            {
#if RG_URP
                Core.URP.ReGizmoRenderFeature.OnPassExecute += OnPassExecute;
#elif RG_HDRP
                RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
#endif
            }
            else
            {
#if RG_URP
                Core.URP.ReGizmoRenderFeature.OnPassExecute -= OnPassExecute;
#else
                foreach (var camera in activeCameras)
                {
#if RG_HDRP
                    RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
#else
                    drawBuffers.DeAttach(camera);
#endif
                }

                activeCameras.Clear();
#endif
            }
        }

        public static void Setup()
        {
            Dispose();

#if RG_URP
            Core.URP.ReGizmoRenderFeature.OnPassExecute += OnPassExecute;
#elif RG_HDRP
            RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
#endif

            ComputeBufferPool.Init();
            drawBuffers = new CommandBufferStack("ReGizmo");

            drawers = new List<IReGizmoDrawer>()
            {
                // Lines
                ReGizmoResolver<ReGizmoLineDrawer>.Init(new ReGizmoLineDrawer()),
                ReGizmoResolver<ReGizmoPolyLineDrawer>.Init(new ReGizmoPolyLineDrawer()),
                ReGizmoResolver<ReGizmoGridDrawer>.Init(new ReGizmoGridDrawer()),

                // Textures
                ReGizmoResolver<ReGizmoIconsDrawer>.Init(new ReGizmoIconsDrawer()),
                ReGizmoResolver<ReGizmoSpritesDrawer>.Init(new ReGizmoSpritesDrawer()),

                // Fonts
                ReGizmoResolver<ReGizmoFontDrawer>.Init(new ReGizmoFontDrawer(ReGizmoSettings.Font)),
                ReGizmoResolver<ReGizmoSDFFontDrawer>.Init(new ReGizmoSDFFontDrawer(ReGizmoSettings.SDFFont)),

                // 3D
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

                // 2D
                ReGizmoResolver<ReGizmoCircleDrawer>.Init(new ReGizmoCircleDrawer()),
                ReGizmoResolver<ReGizmoTriangleDrawer>.Init(new ReGizmoTriangleDrawer()),
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

#if RG_LEGACY
            proxyComp.inUpdate += OnUpdate;
#endif

            proxyComp.inDestroy += Dispose;
            proxyComp.inEnable += () => SetActive(true);
            proxyComp.inDisable += () => SetActive(false);
        }

#if RG_URP
        private static void OnPassExecute(ScriptableRenderContext context, bool isGameView)
        {
            context.ExecuteCommandBuffer(drawBuffers.Current());
        }
#elif RG_HDRP
        private static void OnEndCameraRendering(ScriptableRenderContext context, Camera arg2)
        {
            context.ExecuteCommandBuffer(drawBuffers.Current());
        }
#endif

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

#if RG_LEGACY
            {
                var camera = Camera.main;
                if (activeCameras.Add(camera))
                {
                    drawBuffers.Attach(camera, CAMERA_EVENT);
                }
            }
#endif

#if UNITY_EDITOR && RG_LEGACY
            if (UnityEditor.SceneView.lastActiveSceneView != null)
            {
                var camera = UnityEditor.SceneView.lastActiveSceneView.camera;
                if (activeCameras.Add(camera))
                {
                    drawBuffers.Attach(camera, CAMERA_EVENT);
                }
            }
#endif

            var cmd = drawBuffers.Current();
            cmd.Clear();

            if (ReGizmoSettings.FontSuperSample)
            {
                cmd.EnableShaderKeyword("SDF_SS");
            }
            else
            {
                cmd.DisableShaderKeyword("SDF_SS");
            }

#if REGIZMO_DEV
            cmd.BeginSample("ReGizmo Draw Buffer");
#endif

            foreach (var drawer in drawers)
            {
                if (drawer.CurrentDrawCount() == 0) continue;

                if (interrupted)
                {
                    drawer.Clear();
                    continue;
                }

                drawer.Render(cmd);
                drawer.Clear();
            }

#if REGIZMO_DEV
            cmd.EndSample("ReGizmo Draw Buffer");
#endif

            if (shouldReset)
            {
                shouldReset = false;
                Setup();
            }

            interrupted = false;

#if UNITY_EDITOR
            Profiler.EndSample();
#endif
        }

        public static void ClearAll()
        {
            foreach (var drawer in drawers)
            {
                drawer.Clear();
            }
        }

        public static void Dispose()
        {
            if (activeCameras != null)
            {
                foreach (var camera in activeCameras)
                {
#if RG_LEGACY
                    drawBuffers.DeAttach(camera);
#endif
                }

                activeCameras.Clear();
            }

            drawBuffers?.Dispose();

            if (drawers != null)
            {
                foreach (var drawer in drawers)
                {
                    drawer.Dispose();
                }

                drawers.Clear();
            }
        }
    }
}