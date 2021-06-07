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

        internal static event System.Action OnRender;

        static bool isSetup = false;
        static GameObject proxyObject;

        static List<IReGizmoDrawer> drawers;
        static Dictionary<Camera, CameraData> activeCameras;

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
            activeCameras = new Dictionary<Camera, CameraData>();
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

        public static void Setup()
        {
            Dispose();

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

            OnRender?.Invoke();

#if UNITY_EDITOR
            Profiler.BeginSample("ReGizmo::OnUpdate");
#endif

#if RG_LEGACY
            {
                var camera = Camera.main;
                if (!activeCameras.ContainsKey(camera))
                {
                    activeCameras.Add(camera, new CameraData(camera, CAMERA_EVENT));
                }
            }
#endif

#if UNITY_EDITOR && RG_LEGACY
            if (UnityEditor.SceneView.lastActiveSceneView != null)
            {
                var camera = UnityEditor.SceneView.lastActiveSceneView.camera;
                if (!activeCameras.ContainsKey(camera))
                {
                    activeCameras.Add(camera, new CameraData(camera, CAMERA_EVENT));
                }
            }
#endif 

            Profiler.BeginSample("ReGizmo::OnUpdate::PreRender");
            foreach (var cameraData in activeCameras.Values)
            {
                cameraData.PreRender();
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
        }
    }
}