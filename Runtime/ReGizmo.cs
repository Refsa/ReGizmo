using System;
using System.Collections.Generic;
using System.Linq;
using ReGizmo.Drawing;
using ReGizmo.Utils;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Rendering;
using Debug = UnityEngine.Debug;

#if RG_URP
using UnityEngine.Rendering.Universal;
using CameraData = ReGizmo.Drawing.CameraData;
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

        internal static event System.Action OnRender;

        static List<IReGizmoDrawer> drawers;
        static Dictionary<Camera, CameraData> activeCameras;

        static bool interrupted;
        static bool isActive;
        static bool shouldReset;
        static bool isSetup = false;

        public static bool IsSetup => isSetup;

#if !UNITY_EDITOR && REGIZMO_RUNTIME
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
            isActive = true;
#endif
        }

        public static void Setup()
        {
            Dispose();
            activeCameras = new Dictionary<Camera, CameraData>();

#if RG_URP
            Core.URP.ReGizmoRenderFeature.OnPassExecute += OnPassExecute;
#elif RG_HDRP
            RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
#endif

            ComputeBufferPool.Init();

            drawers = new List<IReGizmoDrawer>()
            {
                // Lines
                ReGizmoResolver<LineDrawer>.Init(new LineDrawer()),
                ReGizmoResolver<PolyLineDrawer>.Init(new PolyLineDrawer()),
                ReGizmoResolver<GridDrawer>.Init(new GridDrawer()),

                // Textures
                ReGizmoResolver<IconsDrawer>.Init(new IconsDrawer()),
                ReGizmoResolver<SpritesDrawer>.Init(new SpritesDrawer()),

                // Fonts
                ReGizmoResolver<TextDrawer>.Init(new TextDrawer(ReGizmoSettings.Font)),
                ReGizmoResolver<SDFTextDrawer>.Init(new SDFTextDrawer(ReGizmoSettings.SDFFont)),

                // 3D
                ReGizmoResolver<CubeDrawer>.Init(new CubeDrawer()),
                ReGizmoResolver<SphereDrawer>.Init(new SphereDrawer()),
                ReGizmoResolver<ConeDrawer>.Init(new ConeDrawer()),
                ReGizmoResolver<CylinderDrawer>.Init(new CylinderDrawer()),
                ReGizmoResolver<IcosahedronDrawer>.Init(new IcosahedronDrawer()),
                ReGizmoResolver<OctahedronDrawer>.Init(new OctahedronDrawer()),
                ReGizmoResolver<QuadDrawer>.Init(new QuadDrawer()),
                ReGizmoResolver<PyramidDrawer>.Init(new PyramidDrawer()),
                ReGizmoResolver<CapsuleDrawer>.Init(new CapsuleDrawer()),

                ReGizmoResolver<CustomMeshDrawer>.Init(new CustomMeshDrawer()),
                ReGizmoResolver<CustomMeshWireframeDrawer>.Init(new CustomMeshWireframeDrawer()),

#if REGIZMO_DEV
                ReGizmoResolver<AABBDebugDrawer>.Init(new AABBDebugDrawer()),
#endif

                // 2D
                ReGizmoResolver<CircleDrawer>.Init(new CircleDrawer()),
                ReGizmoResolver<TriangleDrawer>.Init(new TriangleDrawer()),
            };

#if !UNITY_EDITOR
            if (Application.isPlaying)
            {
                PlayerLoopInject.Setup();
                SetupRuntimeHooks();
            }
#endif

            isSetup = true;
            interrupted = false;
        }

        static void SetupRuntimeHooks()
        {
            PlayerLoopInject.Inject(PlayerLoopInjectionPoint.PostUpdate, OnUpdate);
            Application.quitting += Dispose;
        }

#if RG_URP
        private static void OnPassExecute(ScriptableRenderContext context, bool isGameView)
        {
            foreach (var cameraData in activeCameras)
            {
                context.ExecuteCommandBuffer(cameraData.Value.CommandBuffer);
            }
        }
#elif RG_HDRP
        private static void OnEndCameraRendering(ScriptableRenderContext context, Camera camera)
        {
            foreach (var cameraData in activeCameras)
            {
                context.ExecuteCommandBuffer(cameraData.Value.CommandBuffer);
            }
        }
#endif

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
#elif RG_HDRP
                RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
#endif
                foreach (var camera in activeCameras)
                {
                    camera.Value.SetActive(state);
                }

                activeCameras.Clear();
            }
        }

        public static void OnUpdate()
        {
            if (drawers == null) return;
            if (!isActive)
            {
                foreach (var drawer in drawers) drawer.Clear();
                return;
            }

            OnRender?.Invoke();

#if UNITY_EDITOR
            Profiler.BeginSample("ReGizmo::OnUpdate");
#endif

            {
                foreach (var camera in Camera.allCameras)
                {
                    if (!activeCameras.ContainsKey(camera))
                    {
#if RG_LEGACY
                        activeCameras.Add(camera, CameraData.Legacy(camera, CAMERA_EVENT));
#else
                        activeCameras.Add(camera, CameraData.SRP(camera));
#endif
                    }
                }
            }

#if UNITY_EDITOR
            if (UnityEditor.SceneView.lastActiveSceneView != null)
            {
                var camera = UnityEditor.SceneView.lastActiveSceneView.camera;
                if (!activeCameras.ContainsKey(camera))
                {
#if RG_LEGACY
                    activeCameras.Add(camera, CameraData.Legacy(camera, CAMERA_EVENT));
#else
                    activeCameras.Add(camera, CameraData.SRP(camera));
#endif
                }
            }
#endif 

            Profiler.BeginSample("ReGizmo::OnUpdate::PreRender");

            bool rebuild = false;
            foreach (var cameraData in activeCameras.Values)
            {
                if (!cameraData.PreRender())
                {
                    rebuild = true;
                }
            }

            if (rebuild)
            {
                activeCameras = activeCameras.Where(kvp => kvp.Key != null).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            }
            Profiler.EndSample();

            Profiler.BeginSample("ReGizmo::OnUpdate::Render");
            foreach (var drawer in drawers)
            {
                Profiler.BeginSample("ReGizmo::OnUpdate::PushSharedData");
                drawer.PushSharedData();
                Profiler.EndSample();

                foreach (var cameraData in activeCameras.Values)
                {
                    cameraData.Render(drawer);
                }
                drawer.Clear();
            }
            Profiler.EndSample();

            Profiler.BeginSample("ReGizmo::OnUpdate::PostRender");
            foreach (var cameraData in activeCameras.Values)
            {
                cameraData.PostRender();
            }
            Profiler.EndSample();

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
                foreach (var cameraData in activeCameras.Values)
                {
                    cameraData.DeAttach();
                    cameraData.Dispose();
                }

                activeCameras.Clear();
            }

            if (drawers != null)
            {
                foreach (var drawer in drawers)
                {
                    drawer.Dispose();
                }

                drawers.Clear();
            }

            ComputeBufferPool.FreeAll();
        }
    }
}