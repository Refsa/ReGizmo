

using System.Collections.Generic;
using ReGizmo.Drawing;
using UnityEngine;

namespace ReGizmo.Core
{
    public static class ReGizmo
    {
        static bool isSetup = false;
        static GameObject proxyObject;

        static List<IReGizmoDrawer> drawers;

        [RuntimeInitializeOnLoadMethod]
        public static void Initialize()
        {
            if (drawers != null) 
            {
                Dispose();
            }

            Setup();
        }

        public static void Setup()
        {
            string assetPath = ReGizmoHelpers.GetProjectResourcesPath();

            drawers = new List<IReGizmoDrawer>()
            {
                ReGizmoResolver<ReGizmoCubeDrawer>.Init(new ReGizmoCubeDrawer()),
                ReGizmoResolver<ReGizmoSphereDrawer>.Init(new ReGizmoSphereDrawer()),
                ReGizmoResolver<ReGizmoConeDrawer>.Init(new ReGizmoConeDrawer()),
                ReGizmoResolver<ReGizmoCylinderDrawer>.Init(new ReGizmoCylinderDrawer()),
                ReGizmoResolver<ReGizmoIcosahedronDrawer>.Init(new ReGizmoIcosahedronDrawer()),
                ReGizmoResolver<ReGizmoOctahedronDrawer>.Init(new ReGizmoOctahedronDrawer()),
                ReGizmoResolver<ReGizmoQuadDrawer>.Init(new ReGizmoQuadDrawer()),
                ReGizmoResolver<ReGizmoPyramidDrawer>.Init(new ReGizmoPyramidDrawer()),

                ReGizmoResolver<ReGizmoCustomMeshDrawer>.Init(new ReGizmoCustomMeshDrawer()),

                ReGizmoResolver<ReGizmoLineDrawer>.Init(new ReGizmoLineDrawer()),

                ReGizmoResolver<ReGizmoIconsDrawer>.Init(new ReGizmoIconsDrawer()),

                ReGizmoResolver<ReGizmoFontDrawer>.Init(new ReGizmoFontDrawer(ReGizmoHelpers.LoadFontByName("MonoSpatial"))),
            };

            if (Application.isPlaying)
            {
                SetupProxyObject();
            }

            isSetup = true;
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
            if (drawers == null) return;

            foreach (var drawer in drawers)
            {
                drawer.Dispose();
            }

            drawers = null;
        }

        public static void OnUpdate()
        {
            if (drawers == null) return;

            Camera gameCamera = Camera.main;
            Camera sceneViewCamera = UnityEditor.SceneView.lastActiveSceneView.camera;

            foreach (var drawer in drawers)
            {
                if (sceneViewCamera != null)
                {
                    drawer.Render(sceneViewCamera);
                }
                drawer.Render(gameCamera);

                drawer.Clear();
            }
        }
    }
}