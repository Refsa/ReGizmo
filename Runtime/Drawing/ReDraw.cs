

using System;
using System.Collections.Generic;
using UnityEngine;

namespace ReGizmo.Drawing
{
    public static class ReDraw
    {
        static bool isSetup = false;
        static GameObject proxyObject;

        static List<IReGizmoDrawer> drawers;

        static ReDraw()
        {
            drawers = new List<IReGizmoDrawer>();

            drawers.Add(
                ReGizmoResolver<ReGizmoCubeDrawer, MeshDrawerShaderData>.Init(new ReGizmoCubeDrawer())
            );

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

        private static void OnUpdate()
        {
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

        public static void Cube(Vector3 position, Color color)
        {
            if (ReGizmoResolver<ReGizmoCubeDrawer, MeshDrawerShaderData>.TryGet(out var drawer))
            {
                ref var shaderData = ref drawer.GetShaderData();

                shaderData.Position = position;
                shaderData.Color = color;
                shaderData.Scale = Vector3.one;
                shaderData.Rotation = Vector3.zero;
            }
        }
    }
}