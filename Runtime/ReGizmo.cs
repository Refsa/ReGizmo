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
        const CameraEvent CAMERA_EVENT = CameraEvent.AfterImageEffects;

        internal static event System.Action OnRender;

        static List<IReGizmoDrawer> drawers;
        static Dictionary<Camera, CameraData> activeCameras;

        static bool interrupted;
        static bool isActive;
        static bool shouldReset;
        static bool isSetup = false;

        internal static bool IsSetup => isSetup;
        internal static bool IsActive => isActive;

#if !UNITY_EDITOR && REGIZMO_RUNTIME
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
#endif
        public static void Initialize()
        {
#if !UNITY_EDITOR && !REGIZMO_RUNTIME
            throw new System.InvalidOperationException("ReGizmo runtime is not enabled!");
#endif

            // Debug.Log("#### ReGizmo Init ####"); 

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

            SetActive(true);

            ComputeBufferPool.Init();

            // Initialize drawers
            {
                // 3D Primitives
                ReGizmoResolver<CubeDrawer>.Init(() => new CubeDrawer());
                ReGizmoResolver<SphereDrawer>.Init(() => new SphereDrawer());
                ReGizmoResolver<ConeDrawer>.Init(() => new ConeDrawer());
                ReGizmoResolver<CylinderDrawer>.Init(() => new CylinderDrawer());
                ReGizmoResolver<IcosahedronDrawer>.Init(() => new IcosahedronDrawer());
                ReGizmoResolver<OctahedronDrawer>.Init(() => new OctahedronDrawer());
                ReGizmoResolver<QuadDrawer>.Init(() => new QuadDrawer());
                ReGizmoResolver<PyramidDrawer>.Init(() => new PyramidDrawer());
                ReGizmoResolver<CapsuleDrawer>.Init(() => new CapsuleDrawer());

                // 3D Custom
                ReGizmoResolver<CustomMeshDrawer>.Init(() => new CustomMeshDrawer());
                ReGizmoResolver<CustomMeshWireframeDrawer>.Init(() => new CustomMeshWireframeDrawer());

                // Textures
                ReGizmoResolver<IconsDrawer>.Init(() => new IconsDrawer());
                ReGizmoResolver<SpritesDrawer>.Init(() => new SpritesDrawer());
                ReGizmoResolver<TexturesDrawer>.Init(() => new TexturesDrawer());

                // Fonts
                ReGizmoResolver<TextDrawer>.Init(() => new TextDrawer(ReGizmoSettings.Font));
                ReGizmoResolver<SDFTextDrawer>.Init(() => new SDFTextDrawer(ReGizmoSettings.SDFFont));

                // 2D
                ReGizmoResolver<CircleDrawer>.Init(() => new CircleDrawer());
                ReGizmoResolver<TriangleDrawer>.Init(() => new TriangleDrawer());

                // Lines
                ReGizmoResolver<LineDrawer>.Init(() => new LineDrawer());
                ReGizmoResolver<PolyLineDrawer>.Init(() => new PolyLineDrawer());
                ReGizmoResolver<GridDrawer>.Init(() => new GridDrawer());

                // Debug
#if REGIZMO_DEV
                ReGizmoResolver<AABBDebugDrawer>.Init(() => new AABBDebugDrawer());
#endif
            }

            drawers = new List<IReGizmoDrawer>()
            {
                // ## SORTED ##
                ReGizmoResolver<CubeDrawer>.DrawerSorted,
                ReGizmoResolver<SphereDrawer>.DrawerSorted,
                ReGizmoResolver<ConeDrawer>.DrawerSorted,
                ReGizmoResolver<CylinderDrawer>.DrawerSorted,
                ReGizmoResolver<IcosahedronDrawer>.DrawerSorted,
                ReGizmoResolver<OctahedronDrawer>.DrawerSorted,
                ReGizmoResolver<QuadDrawer>.DrawerSorted,
                ReGizmoResolver<PyramidDrawer>.DrawerSorted,
                ReGizmoResolver<CapsuleDrawer>.DrawerSorted,
                ReGizmoResolver<CustomMeshDrawer>.DrawerSorted,
                ReGizmoResolver<CustomMeshWireframeDrawer>.DrawerSorted,
                ReGizmoResolver<IconsDrawer>.DrawerSorted,
                ReGizmoResolver<SpritesDrawer>.DrawerSorted,
                ReGizmoResolver<TextDrawer>.DrawerSorted,
                ReGizmoResolver<SDFTextDrawer>.DrawerSorted,
                ReGizmoResolver<CircleDrawer>.DrawerSorted,
                ReGizmoResolver<TriangleDrawer>.DrawerSorted,
                ReGizmoResolver<LineDrawer>.DrawerSorted,
                ReGizmoResolver<PolyLineDrawer>.DrawerSorted,
                ReGizmoResolver<GridDrawer>.DrawerSorted,
                ReGizmoResolver<TexturesDrawer>.DrawerSorted,

                // ## OVERLAY ##
                // ReGizmoResolver<CubeDrawer>.DrawerOverlay,
                // ReGizmoResolver<SphereDrawer>.DrawerOverlay,
                // ReGizmoResolver<ConeDrawer>.DrawerOverlay,
                // ReGizmoResolver<CylinderDrawer>.DrawerOverlay,
                // ReGizmoResolver<IcosahedronDrawer>.DrawerOverlay,
                // ReGizmoResolver<OctahedronDrawer>.DrawerOverlay,
                // ReGizmoResolver<QuadDrawer>.DrawerOverlay,
                // ReGizmoResolver<PyramidDrawer>.DrawerOverlay,
                // ReGizmoResolver<CapsuleDrawer>.DrawerOverlay,
                // ReGizmoResolver<CustomMeshDrawer>.DrawerOverlay,
                // ReGizmoResolver<CustomMeshWireframeDrawer>.DrawerOverlay,
                // ReGizmoResolver<IconsDrawer>.DrawerOverlay,
                // ReGizmoResolver<SpritesDrawer>.DrawerOverlay,
                // ReGizmoResolver<TextDrawer>.DrawerOverlay,
                // ReGizmoResolver<SDFTextDrawer>.DrawerOverlay,
                // ReGizmoResolver<CircleDrawer>.DrawerOverlay,
                // ReGizmoResolver<TriangleDrawer>.DrawerOverlay,
                // ReGizmoResolver<LineDrawer>.DrawerOverlay,
                // ReGizmoResolver<PolyLineDrawer>.DrawerOverlay,
                // ReGizmoResolver<GridDrawer>.DrawerOverlay,

#if REGIZMO_DEV
                ReGizmoResolver<AABBDebugDrawer>.DrawerSorted,
                ReGizmoResolver<AABBDebugDrawer>.DrawerOverlay,
#endif
            };

#if !UNITY_EDITOR
            PlayerLoopInject.Setup();
            SetupRuntimeHooks();
#endif

            isSetup = true;
            interrupted = false;
        }

        static void SetupRuntimeHooks()
        {
            PlayerLoopInject.Inject(PlayerLoopInjectionPoint.PostUpdate, OnUpdate);
            PlayerLoopInject.Inject(PlayerLoopInjectionPoint.EndOfFrame, OnFrameCleanup);
            Application.quitting += Dispose;
        }

#if RG_URP
        private static void OnURPPassExecute(in ScriptableRenderContext context, Camera camera, in Framebuffer framebuffer, CommandBuffer cmd)
        {
            activeCameras ??= new Dictionary<Camera, CameraData>();

            if (!activeCameras.TryGetValue(camera, out var cameraData))
            {
                return;
            }

            cameraData.CommandBufferOverride(cmd);
            cameraData.SetFramebuffer(framebuffer);
            Render(cameraData, clearCommandBuffer: false);
            cameraData.SetFramebuffer(new Framebuffer());
            cameraData.RemoveCommandBuffer();
        }
#elif RG_HDRP
        static void OnHDRPPassExecute(in ScriptableRenderContext context, CommandBuffer cmd, Camera camera, in Framebuffer framebuffer)
        {
            if (!activeCameras.TryGetValue(camera, out var cameraData))
            {
                return;
            }

            cameraData.CommandBufferOverride(cmd);
            cameraData.SetFramebuffer(framebuffer);
            Render(cameraData, clearCommandBuffer: false);
            cameraData.RemoveCommandBuffer();

#if UNITY_EDITOR
            OnFrameCleanup();
#endif
        }

        static void OnHDRPPassCleanup()
        {
            if (activeCameras != null)
            {
                foreach (var kvp in activeCameras)
                {
                    kvp.Value.RemoveCommandBuffer();
                    kvp.Value.Dispose();
                }
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
                Core.URP.ReGizmoURPRenderFeature.OnPassExecute += OnURPPassExecute;
                Core.URP.ReGizmoURPRenderFeature.OnPassCleanup += OnFrameCleanup;
#elif RG_HDRP
                Core.HDRP.ReGizmoHDRPRenderPass.OnPassExecute += OnHDRPPassExecute;
                Core.HDRP.ReGizmoHDRPRenderPass.OnPassCleanup += OnHDRPPassCleanup;
#endif
            }
            else
            {
#if RG_URP
                Core.URP.ReGizmoURPRenderFeature.OnPassExecute -= OnURPPassExecute;
                Core.URP.ReGizmoURPRenderFeature.OnPassCleanup -= OnFrameCleanup;
#elif RG_HDRP
                Core.HDRP.ReGizmoHDRPRenderPass.OnPassExecute -= OnHDRPPassExecute;
                Core.HDRP.ReGizmoHDRPRenderPass.OnPassCleanup -= OnHDRPPassCleanup;
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

            Profiler.BeginSample("ReGizmo::OnUpdate");

            {
                foreach (var camera in Camera.allCameras)
                {
                    if (!activeCameras.ContainsKey(camera))
                    {
                        activeCameras.Add(camera, new CameraData(camera, CAMERA_EVENT));
                    }
                }
            }

#if UNITY_EDITOR
            if (UnityEditor.SceneView.lastActiveSceneView != null)
            {
                var camera = UnityEditor.SceneView.lastActiveSceneView.camera;
                if (!activeCameras.ContainsKey(camera))
                {
                    activeCameras.Add(camera, new CameraData(camera, CAMERA_EVENT));
                }
            }
#endif

            activeCameras = activeCameras.Where(kvp => kvp.Key != null && kvp.Value.Camera != null).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            foreach (var drawer in drawers)
            {
                drawer.PushSharedData();
                drawer.Clear();
            }

#if RG_LEGACY
            foreach (var cameraData in activeCameras)
            {
                Render(cameraData.Value);
            }
#endif

            if (shouldReset)
            {
                shouldReset = false;
                Setup();
            }

            interrupted = false;

            Profiler.EndSample();
        }

        static void OnFrameCleanup()
        {
            if (drawers != null)
            {
                foreach (var drawer in drawers)
                {
                    drawer.Clear();
                }
            }

            if (activeCameras != null)
            {
                foreach (var cameraData in activeCameras.Values)
                {
                    cameraData.FrameCleanup();
                }
            }
        }

        static void Render(CameraData cameraData, bool clearCommandBuffer = true)
        {
            if (!cameraData.FrameSetup(clearCommandBuffer))
            {
                return;
            }

            cameraData.PreRender(drawers);
            cameraData.Render(drawers);
            cameraData.PostRender();
            cameraData.FrameCleanup();
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
#if RG_URP
            Core.URP.ReGizmoURPRenderFeature.OnPassExecute -= OnURPPassExecute;
#elif RG_HDRP
            Core.HDRP.ReGizmoHDRPRenderPass.OnPassExecute -= OnHDRPPassExecute;
            Core.HDRP.ReGizmoHDRPRenderPass.OnPassCleanup -= OnHDRPPassCleanup;
#endif

            if (activeCameras != null)
            {
                foreach (var cameraData in activeCameras.Values)
                {
                    cameraData.DeAttach();
                    cameraData.Dispose();
                }

                activeCameras.Clear();
                activeCameras = null;
            }

            if (drawers != null)
            {
                foreach (var drawer in drawers)
                {
                    drawer.Dispose();
                }

                drawers.Clear();
                drawers = null;
            }

            ComputeBufferPool.FreeAll();
        }
    }
}