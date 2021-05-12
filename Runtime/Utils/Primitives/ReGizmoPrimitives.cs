using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReGizmo.Utils
{
    internal static class ReGizmoPrimitives
    {
        static readonly Vector3 DefaultSize = Vector3.one;

        static Vector3 FindSurfaceNormal(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            Vector3 edge1 = p2 - p1;
            Vector3 edge2 = p3 - p2;

            return Vector3.Cross(edge1, edge2).normalized;
        }

        public static Mesh Quad()
        {
            Shape quadShape = new Shape();

            Vector3 halfSize = Vector2.one / 2f;
            Vector3 c1 = new Vector3(-halfSize.x, -halfSize.y, 0f);
            Vector3 c2 = new Vector3(halfSize.x, -halfSize.y, 0f);
            Vector3 c3 = new Vector3(halfSize.x, halfSize.y, 0f);
            Vector3 c4 = new Vector3(-halfSize.x, halfSize.y, 0f);

            return
                quadShape
                    .AddTriangle(c1, c2, c3)
                    .AddTriangle(c1, c3, c4)
                    .ToMesh();
        }

        public static Mesh Quad(Vector2 size)
        {
            Vector3 halfSize = size / 2f;

            Vector3 c1 = new Vector3(-halfSize.x, -halfSize.y, 0f);
            Vector3 c2 = new Vector3(halfSize.x, -halfSize.y, 0f);
            Vector3 c3 = new Vector3(halfSize.x, halfSize.y, 0f);
            Vector3 c4 = new Vector3(-halfSize.x, halfSize.y, 0f);

            Vector3[] vertices =
                new[]
                {
                    c1, c2, c3, c4
                };

            int[] indices =
                new[]
                {
                    0, 1, 2, 0, 2, 3
                };

            Vector3[] normals =
                new[]
                {
                    Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward
                };

            Vector2[] uvs =
                new[]
                {
                    new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(1f, 1f), new Vector2(0f, 1f)
                };

            return CreateMesh(vertices, indices, normals, uvs);
        }

        public static Mesh Cube()
        {
            return Cube(DefaultSize);
        }

        public static Mesh Cube(Vector3 size)
        {
            if (size == Vector3.zero) size = DefaultSize;

            Vector3[] vertices = new Vector3[]
            {
                new Vector3(0.5f, -0.5f, 0.5f), new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(0.5f, 0.5f, 0.5f),
                new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(0.5f, 0.5f, -0.5f), new Vector3(-0.5f, 0.5f, -0.5f),
                new Vector3(0.5f, -0.5f, -0.5f), new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(0.5f, 0.5f, 0.5f),
                new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(0.5f, 0.5f, -0.5f), new Vector3(-0.5f, 0.5f, -0.5f),
                new Vector3(0.5f, -0.5f, -0.5f), new Vector3(0.5f, -0.5f, 0.5f), new Vector3(-0.5f, -0.5f, 0.5f),
                new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(-0.5f, -0.5f, 0.5f), new Vector3(-0.5f, 0.5f, 0.5f),
                new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(0.5f, -0.5f, -0.5f),
                new Vector3(0.5f, 0.5f, -0.5f), new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0.5f, -0.5f, 0.5f)
            };

            int[] indices = new[]
            {
                0, 2, 3, 0, 3, 1, 8, 4, 5, 8, 5, 9, 10, 6, 7, 10, 7, 11, 12, 13, 14, 12, 14, 15, 16, 17, 18, 16, 18, 19,
                20, 21, 22, 20, 22, 23
            };

            Vector3[] normals = new Vector3[]
            {
                new Vector3(0.0f, 0.0f, 1.0f), new Vector3(0.0f, 0.0f, 1.0f), new Vector3(0.0f, 0.0f, 1.0f),
                new Vector3(0.0f, 0.0f, 1.0f), new Vector3(0.0f, 1.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f),
                new Vector3(0.0f, 0.0f, -1.0f), new Vector3(0.0f, 0.0f, -1.0f), new Vector3(0.0f, 1.0f, 0.0f),
                new Vector3(0.0f, 1.0f, 0.0f), new Vector3(0.0f, 0.0f, -1.0f), new Vector3(0.0f, 0.0f, -1.0f),
                new Vector3(0.0f, -1.0f, 0.0f), new Vector3(0.0f, -1.0f, 0.0f), new Vector3(0.0f, -1.0f, 0.0f),
                new Vector3(0.0f, -1.0f, 0.0f), new Vector3(-1.0f, 0.0f, 0.0f), new Vector3(-1.0f, 0.0f, 0.0f),
                new Vector3(-1.0f, 0.0f, 0.0f), new Vector3(-1.0f, 0.0f, 0.0f), new Vector3(1.0f, 0.0f, 0.0f),
                new Vector3(1.0f, 0.0f, 0.0f), new Vector3(1.0f, 0.0f, 0.0f), new Vector3(1.0f, 0.0f, 0.0f)
            };

            return CreateMesh(vertices, indices, normals);
        }

        public static Mesh Pyramid()
        {
            return Pyramid(DefaultSize, 1f);
        }

        public static Mesh Pyramid(Vector2 size, float height)
        {
            if (size == Vector2.zero) size = DefaultSize;

            Vector2 halfSize = size / 2f;
            Vector3 bA = new Vector3(-halfSize.x, 0f, -halfSize.y);
            Vector3 bB = new Vector3(-halfSize.x, 0f, halfSize.x);
            Vector3 bC = new Vector3(halfSize.x, 0f, halfSize.y);
            Vector3 bD = new Vector3(halfSize.x, 0f, -halfSize.y);

            Vector3 top = new Vector3(0f, height, 0f);

            Vector3[] vertices = new Vector3[5]
            {
                bA, bB, bC, bD,
                top
            };

            int[] indices = new int[18]
            {
                0, 2, 1, 0, 3, 2,
                4, 0, 1,
                4, 1, 2,
                4, 2, 3,
                4, 3, 0
            };

            return CreateMesh(vertices, indices);
        }

        public static Mesh Octahedron(float radius = 1f)
        {
            Vector3 bA = new Vector3(-radius, 0f, -radius);
            Vector3 bB = new Vector3(-radius, 0f, radius);
            Vector3 bC = new Vector3(radius, 0f, radius);
            Vector3 bD = new Vector3(radius, 0f, -radius);

            Vector3 top = new Vector3(0f, radius, 0f);

            Vector3[] vertices = new Vector3[6]
            {
                bA, bB, bC, bD,
                top, -top
            };

            int[] indices = new int[30]
            {
                0, 2, 1, 0, 3, 2,
                4, 0, 1,
                4, 1, 2,
                4, 2, 3,
                4, 3, 0,
                5, 1, 0,
                5, 2, 1,
                5, 3, 2,
                5, 0, 3
            };

            return CreateMesh(vertices, indices);
        }

        public static Mesh Capsule()
        {
            Vector3[] vertices = new[]
            {
                new Vector3(-0.2885871f, -0.7887394f, 0.2885871f), new Vector3(-0.2885871f, -0.7890102f, -0.288587f),
                new Vector3(0.2885871f, -0.7887395f, 0.2885871f), new Vector3(0.2885871f, -0.7890103f, -0.2885871f),
                new Vector3(-0.288587f, 0.7887397f, 0.2885871f), new Vector3(-0.288587f, 0.7887397f, -0.288587f),
                new Vector3(0.2885871f, 0.7887397f, 0.2885871f), new Vector3(0.2885871f, 0.7887397f, -0.288587f),
                new Vector3(9.853311E-08f, -1f, 5.559791E-08f), new Vector3(-3.498129E-08f, 1f, -3.023278E-08f),
                new Vector3(-0.5f, 0.5001526f, 2.698768E-08f), new Vector3(-1.59078E-08f, 0.5004234f, -0.5f),
                new Vector3(0.5f, 0.5001526f, 2.698768E-08f), new Vector3(-1.59078E-08f, 0.5001526f, 0.5f),
                new Vector3(9.853311E-08f, -0.8535981f, 0.3534455f),
                new Vector3(0.3534455f, -0.8535981f, 5.559791E-08f),
                new Vector3(9.853311E-08f, -0.8538688f, -0.3534455f),
                new Vector3(-0.3534455f, -0.8535981f, 5.559791E-08f),
                new Vector3(-0.3534456f, 0.8535982f, 2.698768E-08f),
                new Vector3(-1.59078E-08f, 0.8535982f, -0.3534455f), new Vector3(0.3534456f, 0.8535982f, 2.698768E-08f),
                new Vector3(-1.59078E-08f, 0.8535982f, 0.3534456f), new Vector3(-0.3534454f, 0.5004234f, -0.3534455f),
                new Vector3(-0.3534454f, 0.5001526f, 0.3534455f), new Vector3(0.3534455f, 0.5004234f, -0.3534455f),
                new Vector3(0.3534455f, 0.5001526f, 0.3534455f), new Vector3(-0.2408286f, -0.8098695f, 0.3097169f),
                new Vector3(-0.1705907f, -0.8323771f, 0.3322245f), new Vector3(-0.08829313f, -0.8480403f, 0.3478877f),
                new Vector3(0.2408287f, -0.8098693f, 0.3097169f), new Vector3(0.1705909f, -0.8323771f, 0.3322245f),
                new Vector3(0.08829332f, -0.8480403f, 0.3478877f), new Vector3(0.3097169f, -0.8098695f, 0.2408286f),
                new Vector3(0.3322245f, -0.8323771f, 0.1705908f), new Vector3(0.3478877f, -0.8480403f, 0.08829328f),
                new Vector3(0.3097169f, -0.8101402f, -0.2408286f), new Vector3(0.3322245f, -0.8326479f, -0.1705908f),
                new Vector3(0.3478877f, -0.8483111f, -0.08829316f), new Vector3(0.2408287f, -0.8101401f, -0.3097168f),
                new Vector3(0.1705909f, -0.8326479f, -0.3322244f), new Vector3(0.08829332f, -0.8483111f, -0.3478877f),
                new Vector3(-0.2408286f, -0.8101402f, -0.3097168f), new Vector3(-0.1705907f, -0.8326479f, -0.3322244f),
                new Vector3(-0.08829313f, -0.8483111f, -0.3478877f), new Vector3(-0.3097168f, -0.8101402f, -0.2408286f),
                new Vector3(-0.3322245f, -0.8326479f, -0.1705908f), new Vector3(-0.3478877f, -0.8483111f, -0.08829316f),
                new Vector3(-0.3097168f, -0.8098695f, 0.2408286f), new Vector3(-0.3322245f, -0.8323771f, 0.1705908f),
                new Vector3(-0.3478877f, -0.8480403f, 0.08829328f), new Vector3(-0.3097168f, 0.8098695f, 0.2408287f),
                new Vector3(-0.3322245f, 0.8323771f, 0.1705909f), new Vector3(-0.3478877f, 0.8480405f, 0.08829328f),
                new Vector3(-0.3097168f, 0.8098695f, -0.2408286f), new Vector3(-0.3322245f, 0.8323771f, -0.1705908f),
                new Vector3(-0.3478877f, 0.8480405f, -0.08829317f), new Vector3(-0.2408286f, 0.8098695f, -0.3097168f),
                new Vector3(-0.1705908f, 0.832377f, -0.3322245f), new Vector3(-0.08829321f, 0.8480405f, -0.3478877f),
                new Vector3(0.2408287f, 0.8098695f, -0.3097168f), new Vector3(0.1705909f, 0.8323771f, -0.3322245f),
                new Vector3(0.08829325f, 0.8480405f, -0.3478877f), new Vector3(0.3097169f, 0.8098695f, -0.2408286f),
                new Vector3(0.3322246f, 0.832377f, -0.1705908f), new Vector3(0.3478878f, 0.8480405f, -0.08829317f),
                new Vector3(0.3097169f, 0.8098695f, 0.2408287f), new Vector3(0.3322246f, 0.832377f, 0.1705909f),
                new Vector3(0.3478878f, 0.8480405f, 0.08829328f), new Vector3(0.2408287f, 0.8098695f, 0.3097169f),
                new Vector3(0.1705909f, 0.8323771f, 0.3322246f), new Vector3(0.08829325f, 0.8480405f, 0.3478878f),
                new Vector3(-0.2408286f, 0.8098695f, 0.3097169f), new Vector3(-0.1705908f, 0.832377f, 0.3322246f),
                new Vector3(-0.08829319f, 0.8480405f, 0.3478878f), new Vector3(-0.3097168f, -0.7412519f, -0.3097168f),
                new Vector3(-0.3322245f, -0.6710142f, -0.3322245f), new Vector3(-0.3478877f, -0.5887166f, -0.3478877f),
                new Vector3(-0.3097168f, 0.7409813f, -0.3097168f), new Vector3(-0.3322245f, 0.6707435f, -0.3322245f),
                new Vector3(-0.3478878f, 0.5884459f, -0.3478877f), new Vector3(-0.3097168f, 0.7409813f, 0.3097169f),
                new Vector3(-0.3322245f, 0.6707435f, 0.3322246f), new Vector3(-0.3478878f, 0.5884459f, 0.3478878f),
                new Vector3(-0.3097168f, -0.7409812f, 0.3097169f), new Vector3(-0.3322245f, -0.6707435f, 0.3322246f),
                new Vector3(-0.3478877f, -0.5884458f, 0.3478878f), new Vector3(0.3097169f, -0.7412519f, -0.3097168f),
                new Vector3(0.3322245f, -0.6710142f, -0.3322245f), new Vector3(0.3478878f, -0.5887166f, -0.3478877f),
                new Vector3(0.3097169f, 0.7409813f, -0.3097168f), new Vector3(0.3322245f, 0.6707435f, -0.3322245f),
                new Vector3(0.3478878f, 0.5884459f, -0.3478877f), new Vector3(0.3097169f, -0.7409812f, 0.3097169f),
                new Vector3(0.3322245f, -0.6707435f, 0.3322246f), new Vector3(0.3478878f, -0.5884458f, 0.3478878f),
                new Vector3(0.3097169f, 0.7409813f, 0.3097169f), new Vector3(0.3322245f, 0.6707435f, 0.3322246f),
                new Vector3(0.3478878f, 0.5884459f, 0.3478878f), new Vector3(9.853311E-08f, -0.9136059f, 0.2808985f),
                new Vector3(9.853311E-08f, -0.9599168f, 0.196123f), new Vector3(9.853311E-08f, -0.9896768f, 0.1010615f),
                new Vector3(0.2808986f, -0.913606f, 5.559791E-08f), new Vector3(0.196123f, -0.9599168f, 5.472919E-08f),
                new Vector3(0.1010616f, -0.9896768f, 5.542417E-08f),
                new Vector3(9.853311E-08f, -0.9138767f, -0.2808984f),
                new Vector3(9.853311E-08f, -0.9601876f, -0.1961229f),
                new Vector3(9.853311E-08f, -0.9899476f, -0.1010614f),
                new Vector3(-0.2808985f, -0.913606f, 5.559791E-08f), new Vector3(-0.196123f, -0.9599168f, 5.59454E-08f),
                new Vector3(-0.1010613f, -0.9896768f, 5.542417E-08f),
                new Vector3(-0.2808985f, 0.9136058f, 2.698768E-08f), new Vector3(-0.196123f, 0.9599169f, 2.629271E-08f),
                new Vector3(-0.1010614f, 0.989677f, 2.698768E-08f), new Vector3(-1.59078E-08f, 0.9136058f, -0.2808984f),
                new Vector3(-1.59078E-08f, 0.9599169f, -0.196123f), new Vector3(-1.59078E-08f, 0.989677f, -0.1010614f),
                new Vector3(0.2808986f, 0.9136058f, 2.698768E-08f), new Vector3(0.1961231f, 0.9599169f, 2.803014E-08f),
                new Vector3(0.1010614f, 0.989677f, 2.698768E-08f), new Vector3(-1.59078E-08f, 0.9136058f, 0.2808985f),
                new Vector3(-1.59078E-08f, 0.9599169f, 0.196123f), new Vector3(-1.59078E-08f, 0.989677f, 0.1010615f),
                new Vector3(-0.4134531f, -0.781051f, 5.559791E-08f),
                new Vector3(-0.4597643f, -0.6962755f, 5.472919E-08f),
                new Vector3(-0.4895243f, -0.6012138f, 5.559791E-08f), new Vector3(-0.4134532f, 0.5004234f, -0.2808984f),
                new Vector3(-0.4597643f, 0.5004234f, -0.196123f), new Vector3(-0.4895244f, 0.5004234f, -0.1010614f),
                new Vector3(-0.4134532f, 0.7810512f, 2.698768E-08f),
                new Vector3(-0.4597644f, 0.6962755f, 2.803014E-08f),
                new Vector3(-0.4895244f, 0.6012139f, 2.698768E-08f), new Vector3(-0.4134532f, 0.5001526f, 0.2808985f),
                new Vector3(-0.4597643f, 0.5001526f, 0.196123f), new Vector3(-0.4895244f, 0.5001526f, 0.1010615f),
                new Vector3(9.853311E-08f, -0.7813218f, -0.4134532f),
                new Vector3(6.038614E-08f, -0.6965463f, -0.4597643f),
                new Vector3(6.038614E-08f, -0.6014845f, -0.4895244f), new Vector3(0.2808985f, 0.5004234f, -0.4134532f),
                new Vector3(0.1961231f, 0.5004234f, -0.4597643f), new Vector3(0.1010615f, 0.5004234f, -0.4895243f),
                new Vector3(-1.59078E-08f, 0.7810512f, -0.4134532f),
                new Vector3(-1.59078E-08f, 0.6962755f, -0.4597643f),
                new Vector3(-1.59078E-08f, 0.6012139f, -0.4895243f), new Vector3(-0.2808985f, 0.5004234f, -0.4134532f),
                new Vector3(-0.196123f, 0.5004234f, -0.4597643f), new Vector3(-0.1010614f, 0.5004234f, -0.4895243f),
                new Vector3(0.4134532f, -0.7810512f, 5.559791E-08f),
                new Vector3(0.4597643f, -0.6962755f, 5.664037E-08f),
                new Vector3(0.4895242f, -0.6012138f, 5.559791E-08f), new Vector3(0.4134531f, 0.5001526f, 0.2808985f),
                new Vector3(0.4597642f, 0.5001526f, 0.196123f), new Vector3(0.4895243f, 0.5001526f, 0.1010615f),
                new Vector3(0.4134531f, 0.7810512f, 2.698768E-08f), new Vector3(0.4597644f, 0.6962755f, 2.629271E-08f),
                new Vector3(0.4895243f, 0.6012142f, 2.698768E-08f), new Vector3(0.4134531f, 0.5004234f, -0.2808984f),
                new Vector3(0.4597642f, 0.5004234f, -0.196123f), new Vector3(0.4895243f, 0.5004234f, -0.1010614f),
                new Vector3(9.853311E-08f, -0.781051f, 0.4134532f), new Vector3(6.038614E-08f, -0.6962755f, 0.4597644f),
                new Vector3(6.038614E-08f, -0.6012138f, 0.4895243f), new Vector3(-0.2808985f, 0.5001526f, 0.4134532f),
                new Vector3(-0.196123f, 0.5001526f, 0.4597644f), new Vector3(-0.1010614f, 0.5001526f, 0.4895243f),
                new Vector3(-1.59078E-08f, 0.7810512f, 0.4134532f), new Vector3(-1.59078E-08f, 0.6962755f, 0.4597644f),
                new Vector3(-1.59078E-08f, 0.6012139f, 0.4895244f), new Vector3(0.2808985f, 0.5001526f, 0.4134532f),
                new Vector3(0.1961231f, 0.5001526f, 0.4597644f), new Vector3(0.1010615f, 0.5001526f, 0.4895243f),
                new Vector3(-0.249161f, -0.8546735f, 0.249161f), new Vector3(-0.1753408f, -0.8864079f, 0.2644048f),
                new Vector3(-0.0904505f, -0.9068053f, 0.276224f), new Vector3(-0.2644047f, -0.8864079f, 0.1753409f),
                new Vector3(-0.1844288f, -0.9265553f, 0.1844288f), new Vector3(-0.09475736f, -0.9516049f, 0.1925076f),
                new Vector3(-0.276224f, -0.9068053f, 0.09045061f), new Vector3(-0.1925075f, -0.9516048f, 0.09475747f),
                new Vector3(-0.09901319f, -0.9799862f, 0.0990133f), new Vector3(0.2491609f, -0.8546735f, 0.249161f),
                new Vector3(0.2644047f, -0.8864079f, 0.1753409f), new Vector3(0.276224f, -0.9068053f, 0.09045061f),
                new Vector3(0.1753409f, -0.8864079f, 0.2644048f), new Vector3(0.1844289f, -0.9265556f, 0.1844288f),
                new Vector3(0.1925076f, -0.9516048f, 0.09475747f), new Vector3(0.09045061f, -0.9068052f, 0.276224f),
                new Vector3(0.09475756f, -0.9516049f, 0.1925076f), new Vector3(0.09901338f, -0.9799861f, 0.0990133f),
                new Vector3(0.2491609f, -0.8549443f, -0.2491609f), new Vector3(0.1753409f, -0.8866786f, -0.2644047f),
                new Vector3(0.09045061f, -0.907076f, -0.276224f), new Vector3(0.2644047f, -0.8866787f, -0.1753408f),
                new Vector3(0.1844289f, -0.9268264f, -0.1844288f), new Vector3(0.09475756f, -0.9518757f, -0.1925075f),
                new Vector3(0.276224f, -0.9070761f, -0.09045049f), new Vector3(0.1925076f, -0.9518756f, -0.09475736f),
                new Vector3(0.09901338f, -0.9802569f, -0.09901319f), new Vector3(-0.249161f, -0.8549443f, -0.2491609f),
                new Vector3(-0.2644047f, -0.8866786f, -0.1753408f), new Vector3(-0.276224f, -0.9070761f, -0.09045049f),
                new Vector3(-0.1753408f, -0.8866786f, -0.2644047f), new Vector3(-0.1844288f, -0.9268261f, -0.1844288f),
                new Vector3(-0.1925075f, -0.9518756f, -0.09475736f), new Vector3(-0.0904505f, -0.9070761f, -0.276224f),
                new Vector3(-0.09475736f, -0.9518757f, -0.1925075f),
                new Vector3(-0.09901319f, -0.9802569f, -0.09901319f), new Vector3(-0.2491609f, 0.8546736f, 0.249161f),
                new Vector3(-0.2644047f, 0.8864081f, 0.1753409f), new Vector3(-0.276224f, 0.9068054f, 0.09045061f),
                new Vector3(-0.1753409f, 0.8864081f, 0.2644048f), new Vector3(-0.1844288f, 0.9265556f, 0.1844289f),
                new Vector3(-0.1925076f, 0.951605f, 0.0947575f), new Vector3(-0.09045056f, 0.9068054f, 0.2762241f),
                new Vector3(-0.09475743f, 0.951605f, 0.1925077f), new Vector3(-0.09901326f, 0.9799863f, 0.09901333f),
                new Vector3(-0.2491609f, 0.8546736f, -0.2491609f), new Vector3(-0.1753409f, 0.8864081f, -0.2644047f),
                new Vector3(-0.09045057f, 0.9068054f, -0.276224f), new Vector3(-0.2644047f, 0.8864081f, -0.1753408f),
                new Vector3(-0.1844288f, 0.9265556f, -0.1844288f), new Vector3(-0.09475744f, 0.951605f, -0.1925076f),
                new Vector3(-0.276224f, 0.9068054f, -0.09045051f), new Vector3(-0.1925076f, 0.951605f, -0.09475739f),
                new Vector3(-0.09901327f, 0.9799863f, -0.09901321f), new Vector3(0.249161f, 0.8546736f, -0.2491609f),
                new Vector3(0.2644048f, 0.8864081f, -0.1753408f), new Vector3(0.2762241f, 0.9068054f, -0.09045049f),
                new Vector3(0.175341f, 0.8864081f, -0.2644047f), new Vector3(0.1844289f, 0.9265556f, -0.1844288f),
                new Vector3(0.1925076f, 0.951605f, -0.09475739f), new Vector3(0.09045061f, 0.9068054f, -0.276224f),
                new Vector3(0.09475747f, 0.951605f, -0.1925076f), new Vector3(0.09901331f, 0.9799862f, -0.09901321f),
                new Vector3(0.249161f, 0.8546736f, 0.249161f), new Vector3(0.175341f, 0.8864081f, 0.2644048f),
                new Vector3(0.09045054f, 0.9068054f, 0.276224f), new Vector3(0.2644048f, 0.8864081f, 0.1753409f),
                new Vector3(0.1844289f, 0.9265556f, 0.1844289f), new Vector3(0.09475749f, 0.951605f, 0.1925077f),
                new Vector3(0.2762241f, 0.9068054f, 0.09045063f), new Vector3(0.1925076f, 0.951605f, 0.0947575f),
                new Vector3(0.09901331f, 0.9799862f, 0.09901333f), new Vector3(-0.3545209f, -0.7493135f, 0.249161f),
                new Vector3(-0.3862553f, -0.7645572f, 0.1753409f), new Vector3(-0.4066527f, -0.7763765f, 0.09045061f),
                new Vector3(-0.3862554f, -0.6754933f, 0.2644048f), new Vector3(-0.426403f, -0.6845815f, 0.1844289f),
                new Vector3(-0.4514524f, -0.6926602f, 0.09475749f), new Vector3(-0.4066527f, -0.5906032f, 0.276224f),
                new Vector3(-0.4514525f, -0.5949099f, 0.1925076f), new Vector3(-0.4798335f, -0.5991659f, 0.09901333f),
                new Vector3(-0.3545209f, -0.7495843f, -0.2491609f), new Vector3(-0.3862554f, -0.6757641f, -0.2644047f),
                new Vector3(-0.4066527f, -0.590874f, -0.276224f), new Vector3(-0.3862553f, -0.764828f, -0.1753408f),
                new Vector3(-0.426403f, -0.6848523f, -0.1844288f), new Vector3(-0.4514525f, -0.5951807f, -0.1925075f),
                new Vector3(-0.4066527f, -0.7766473f, -0.09045049f), new Vector3(-0.4514524f, -0.692931f, -0.09475737f),
                new Vector3(-0.4798335f, -0.5994366f, -0.0990132f), new Vector3(-0.3545209f, 0.7493137f, -0.2491609f),
                new Vector3(-0.3862553f, 0.7645574f, -0.1753408f), new Vector3(-0.4066527f, 0.7763767f, -0.09045049f),
                new Vector3(-0.3862553f, 0.6754936f, -0.2644047f), new Vector3(-0.426403f, 0.6845816f, -0.1844288f),
                new Vector3(-0.4514525f, 0.6926603f, -0.09475737f), new Vector3(-0.4066528f, 0.5906031f, -0.276224f),
                new Vector3(-0.4514525f, 0.5949101f, -0.1925075f), new Vector3(-0.4798335f, 0.5991659f, -0.0990132f),
                new Vector3(-0.3545209f, 0.7493137f, 0.249161f), new Vector3(-0.3862553f, 0.6754936f, 0.2644048f),
                new Vector3(-0.4066528f, 0.5906031f, 0.2762241f), new Vector3(-0.3862553f, 0.7645574f, 0.1753409f),
                new Vector3(-0.426403f, 0.6845815f, 0.1844289f), new Vector3(-0.4514525f, 0.5949101f, 0.1925076f),
                new Vector3(-0.4066528f, 0.7763766f, 0.09045063f), new Vector3(-0.4514525f, 0.6926603f, 0.09475749f),
                new Vector3(-0.4798335f, 0.5991659f, 0.09901333f), new Vector3(-0.249161f, -0.7495843f, -0.3545209f),
                new Vector3(-0.1753409f, -0.7648281f, -0.3862552f), new Vector3(-0.0904505f, -0.7766473f, -0.4066527f),
                new Vector3(-0.2644048f, -0.6757641f, -0.3862552f), new Vector3(-0.1844288f, -0.6848523f, -0.426403f),
                new Vector3(-0.09475733f, -0.6929309f, -0.4514524f), new Vector3(-0.276224f, -0.590874f, -0.4066527f),
                new Vector3(-0.1925075f, -0.5951807f, -0.4514524f), new Vector3(-0.09901319f, -0.5994366f, -0.4798335f),
                new Vector3(0.2491609f, -0.7495843f, -0.3545209f), new Vector3(0.2644047f, -0.6757641f, -0.3862552f),
                new Vector3(0.276224f, -0.590874f, -0.4066527f), new Vector3(0.1753409f, -0.7648281f, -0.3862552f),
                new Vector3(0.184429f, -0.6848523f, -0.426403f), new Vector3(0.1925076f, -0.5951807f, -0.4514524f),
                new Vector3(0.09045064f, -0.7766473f, -0.4066527f), new Vector3(0.09475752f, -0.692931f, -0.4514524f),
                new Vector3(0.09901334f, -0.5994366f, -0.4798335f), new Vector3(0.249161f, 0.7493137f, -0.3545209f),
                new Vector3(0.175341f, 0.7645574f, -0.3862552f), new Vector3(0.09045053f, 0.7763767f, -0.4066527f),
                new Vector3(0.2644048f, 0.6754936f, -0.3862552f), new Vector3(0.1844289f, 0.6845816f, -0.4264029f),
                new Vector3(0.09475747f, 0.6926603f, -0.4514524f), new Vector3(0.2762241f, 0.5906032f, -0.4066527f),
                new Vector3(0.1925076f, 0.5949101f, -0.4514524f), new Vector3(0.09901327f, 0.5991659f, -0.4798335f),
                new Vector3(-0.2491609f, 0.7493136f, -0.3545209f), new Vector3(-0.2644047f, 0.6754936f, -0.3862552f),
                new Vector3(-0.276224f, 0.5906032f, -0.4066527f), new Vector3(-0.1753409f, 0.7645574f, -0.3862552f),
                new Vector3(-0.1844288f, 0.6845815f, -0.4264029f), new Vector3(-0.1925076f, 0.5949101f, -0.4514524f),
                new Vector3(-0.09045057f, 0.7763765f, -0.4066527f), new Vector3(-0.0947574f, 0.6926603f, -0.4514524f),
                new Vector3(-0.09901327f, 0.5991659f, -0.4798335f), new Vector3(0.3545209f, -0.7495843f, -0.2491609f),
                new Vector3(0.3862552f, -0.7648281f, -0.1753408f), new Vector3(0.4066526f, -0.7766473f, -0.09045049f),
                new Vector3(0.3862552f, -0.6757641f, -0.2644047f), new Vector3(0.4264029f, -0.6848523f, -0.1844288f),
                new Vector3(0.4514523f, -0.692931f, -0.09475737f), new Vector3(0.4066526f, -0.590874f, -0.276224f),
                new Vector3(0.4514523f, -0.5951807f, -0.1925075f), new Vector3(0.4798335f, -0.5994366f, -0.0990132f),
                new Vector3(0.3545209f, -0.7493135f, 0.249161f), new Vector3(0.3862552f, -0.6754933f, 0.2644048f),
                new Vector3(0.4066526f, -0.5906032f, 0.276224f), new Vector3(0.3862552f, -0.7645573f, 0.1753409f),
                new Vector3(0.4264029f, -0.6845815f, 0.1844289f), new Vector3(0.4514523f, -0.59491f, 0.1925076f),
                new Vector3(0.4066526f, -0.7763765f, 0.09045061f), new Vector3(0.4514523f, -0.6926602f, 0.09475749f),
                new Vector3(0.4798335f, -0.5991659f, 0.09901333f), new Vector3(0.354521f, 0.7493137f, 0.249161f),
                new Vector3(0.3862552f, 0.7645573f, 0.1753409f), new Vector3(0.4066527f, 0.7763767f, 0.09045061f),
                new Vector3(0.3862552f, 0.6754936f, 0.2644048f), new Vector3(0.4264029f, 0.6845816f, 0.1844289f),
                new Vector3(0.4514524f, 0.6926603f, 0.09475749f), new Vector3(0.4066527f, 0.5906032f, 0.2762241f),
                new Vector3(0.4514524f, 0.5949101f, 0.1925076f), new Vector3(0.4798335f, 0.5991659f, 0.09901333f),
                new Vector3(0.354521f, 0.7493137f, -0.2491609f), new Vector3(0.3862552f, 0.6754936f, -0.2644047f),
                new Vector3(0.4066527f, 0.5906032f, -0.276224f), new Vector3(0.3862552f, 0.7645573f, -0.1753408f),
                new Vector3(0.4264029f, 0.6845815f, -0.1844288f), new Vector3(0.4514524f, 0.5949101f, -0.1925075f),
                new Vector3(0.4066527f, 0.7763766f, -0.09045051f), new Vector3(0.4514524f, 0.6926603f, -0.09475737f),
                new Vector3(0.4798335f, 0.5991659f, -0.0990132f), new Vector3(0.249161f, -0.7493135f, 0.3545209f),
                new Vector3(0.1753409f, -0.7645573f, 0.3862553f), new Vector3(0.09045064f, -0.7763765f, 0.4066526f),
                new Vector3(0.2644047f, -0.6754933f, 0.3862553f), new Vector3(0.184429f, -0.6845815f, 0.426403f),
                new Vector3(0.09475752f, -0.6926602f, 0.4514524f), new Vector3(0.276224f, -0.5906032f, 0.4066526f),
                new Vector3(0.1925076f, -0.5949099f, 0.4514524f), new Vector3(0.09901334f, -0.5991658f, 0.4798335f),
                new Vector3(-0.249161f, -0.7493135f, 0.3545209f), new Vector3(-0.2644048f, -0.6754933f, 0.3862553f),
                new Vector3(-0.276224f, -0.5906032f, 0.4066526f), new Vector3(-0.1753409f, -0.7645573f, 0.3862553f),
                new Vector3(-0.1844288f, -0.6845815f, 0.426403f), new Vector3(-0.1925075f, -0.59491f, 0.4514524f),
                new Vector3(-0.0904505f, -0.7763765f, 0.4066526f), new Vector3(-0.09475733f, -0.6926602f, 0.4514524f),
                new Vector3(-0.09901319f, -0.5991658f, 0.4798335f), new Vector3(-0.2491609f, 0.7493136f, 0.3545209f),
                new Vector3(-0.1753409f, 0.7645574f, 0.3862553f), new Vector3(-0.09045056f, 0.7763766f, 0.4066527f),
                new Vector3(-0.2644047f, 0.6754936f, 0.3862553f), new Vector3(-0.1844288f, 0.6845816f, 0.426403f),
                new Vector3(-0.09475739f, 0.6926603f, 0.4514524f), new Vector3(-0.276224f, 0.5906032f, 0.4066527f),
                new Vector3(-0.1925076f, 0.5949101f, 0.4514524f), new Vector3(-0.09901326f, 0.5991659f, 0.4798335f),
                new Vector3(0.249161f, 0.7493137f, 0.3545209f), new Vector3(0.2644048f, 0.6754936f, 0.3862553f),
                new Vector3(0.2762241f, 0.5906032f, 0.4066527f), new Vector3(0.175341f, 0.7645574f, 0.3862553f),
                new Vector3(0.1844289f, 0.6845815f, 0.426403f), new Vector3(0.1925076f, 0.5949101f, 0.4514524f),
                new Vector3(0.09045062f, 0.7763766f, 0.4066527f), new Vector3(0.09475749f, 0.6926603f, 0.4514524f),
                new Vector3(0.09901328f, 0.5991659f, 0.4798335f), new Vector3(-0.5f, -0.5001526f, 5.559791E-08f),
                new Vector3(2.223917E-08f, -0.5004234f, -0.5f), new Vector3(0.5f, -0.5001526f, 2.698768E-08f),
                new Vector3(2.223917E-08f, -0.5001526f, 0.5f), new Vector3(-0.3534455f, -0.5004234f, -0.3534455f),
                new Vector3(-0.3534455f, -0.5001526f, 0.3534455f), new Vector3(0.3534455f, -0.5004234f, -0.3534455f),
                new Vector3(0.3534455f, -0.5001526f, 0.3534455f), new Vector3(-0.4134531f, -0.5004234f, -0.2808984f),
                new Vector3(-0.4597642f, -0.5004234f, -0.1961229f), new Vector3(-0.4895243f, -0.5004234f, -0.1010614f),
                new Vector3(-0.4134531f, -0.5001526f, 0.2808985f), new Vector3(-0.4597642f, -0.5001526f, 0.196123f),
                new Vector3(-0.4895243f, -0.5001526f, 0.1010615f), new Vector3(0.2808986f, -0.5004234f, -0.4134532f),
                new Vector3(0.196123f, -0.5004234f, -0.4597643f), new Vector3(0.1010615f, -0.5004234f, -0.4895244f),
                new Vector3(-0.2808985f, -0.5004234f, -0.4134532f), new Vector3(-0.196123f, -0.5004234f, -0.4597643f),
                new Vector3(-0.1010613f, -0.5004234f, -0.4895244f), new Vector3(0.4134532f, -0.5001526f, 0.2808985f),
                new Vector3(0.4597643f, -0.5001526f, 0.196123f), new Vector3(0.4895244f, -0.5001526f, 0.1010615f),
                new Vector3(0.4134532f, -0.5004234f, -0.2808984f), new Vector3(0.4597643f, -0.5004234f, -0.1961229f),
                new Vector3(0.4895244f, -0.5004234f, -0.1010614f), new Vector3(-0.2808985f, -0.5001526f, 0.4134532f),
                new Vector3(-0.196123f, -0.5001526f, 0.4597644f), new Vector3(-0.1010613f, -0.5001526f, 0.4895243f),
                new Vector3(0.2808986f, -0.5001526f, 0.4134532f), new Vector3(0.196123f, -0.5001526f, 0.4597644f),
                new Vector3(0.1010615f, -0.5001526f, 0.4895243f), new Vector3(9.853311E-08f, -1f, 5.559791E-08f),
                new Vector3(9.853311E-08f, -1f, 5.559791E-08f), new Vector3(0.3534455f, -0.8535981f, 5.559791E-08f),
                new Vector3(0.2808986f, -0.913606f, 5.559791E-08f), new Vector3(0.196123f, -0.9599168f, 5.472919E-08f),
                new Vector3(0.1010616f, -0.9896768f, 5.542417E-08f), new Vector3(9.853311E-08f, -1f, 5.559791E-08f),
                new Vector3(9.853311E-08f, -1f, 5.559791E-08f), new Vector3(9.853311E-08f, -1f, 5.559791E-08f),
                new Vector3(-3.498129E-08f, 1f, -3.023278E-08f), new Vector3(-3.498129E-08f, 1f, -3.023278E-08f),
                new Vector3(-3.498129E-08f, 1f, -3.023278E-08f), new Vector3(-3.498129E-08f, 1f, -3.023278E-08f),
                new Vector3(0.3534456f, 0.8535982f, 2.698768E-08f), new Vector3(0.2808986f, 0.9136058f, 2.698768E-08f),
                new Vector3(0.1961231f, 0.9599169f, 2.803014E-08f), new Vector3(0.1010614f, 0.989677f, 2.698768E-08f),
                new Vector3(-3.498129E-08f, 1f, -3.023278E-08f), new Vector3(-0.2885871f, -0.7887394f, 0.2885871f),
                new Vector3(-0.3097168f, -0.8098695f, 0.2408286f), new Vector3(-0.3322245f, -0.8323771f, 0.1705908f),
                new Vector3(-0.3478877f, -0.8480403f, 0.08829328f),
                new Vector3(-0.3534455f, -0.8535981f, 5.559791E-08f),
                new Vector3(-0.3097168f, -0.8101402f, -0.2408286f), new Vector3(-0.2885871f, -0.7890102f, -0.288587f),
                new Vector3(-0.3322245f, -0.8326479f, -0.1705908f), new Vector3(-0.3478877f, -0.8483111f, -0.08829316f),
                new Vector3(-0.288587f, 0.7887397f, -0.288587f), new Vector3(-0.3097168f, 0.8098695f, -0.2408286f),
                new Vector3(-0.3322245f, 0.8323771f, -0.1705908f), new Vector3(-0.3478877f, 0.8480405f, -0.08829317f),
                new Vector3(-0.3534456f, 0.8535982f, 2.698768E-08f), new Vector3(-0.3097168f, 0.8098695f, 0.2408287f),
                new Vector3(-0.288587f, 0.7887397f, 0.2885871f), new Vector3(-0.3322245f, 0.8323771f, 0.1705909f),
                new Vector3(-0.3478877f, 0.8480405f, 0.08829328f), new Vector3(-0.2885871f, -0.7890102f, -0.288587f),
                new Vector3(-0.3097168f, -0.7412519f, -0.3097168f), new Vector3(-0.2408286f, -0.8101402f, -0.3097168f),
                new Vector3(-0.1705907f, -0.8326479f, -0.3322244f), new Vector3(-0.08829313f, -0.8483111f, -0.3478877f),
                new Vector3(9.853311E-08f, -0.8538688f, -0.3534455f),
                new Vector3(-0.3322245f, -0.6710142f, -0.3322245f), new Vector3(-0.3478877f, -0.5887166f, -0.3478877f),
                new Vector3(-0.3534455f, -0.5004234f, -0.3534455f), new Vector3(0.2408287f, -0.8101401f, -0.3097168f),
                new Vector3(0.2885871f, -0.7890103f, -0.2885871f), new Vector3(0.1705909f, -0.8326479f, -0.3322244f),
                new Vector3(0.08829332f, -0.8483111f, -0.3478877f), new Vector3(0.2885871f, 0.7887397f, -0.288587f),
                new Vector3(0.2408287f, 0.8098695f, -0.3097168f), new Vector3(0.1705909f, 0.8323771f, -0.3322245f),
                new Vector3(0.08829325f, 0.8480405f, -0.3478877f), new Vector3(-1.59078E-08f, 0.8535982f, -0.3534455f),
                new Vector3(-0.2408286f, 0.8098695f, -0.3097168f), new Vector3(-0.3097168f, 0.7409813f, -0.3097168f),
                new Vector3(-0.288587f, 0.7887397f, -0.288587f), new Vector3(-0.3322245f, 0.6707435f, -0.3322245f),
                new Vector3(-0.3478878f, 0.5884459f, -0.3478877f), new Vector3(-0.3534454f, 0.5004234f, -0.3534455f),
                new Vector3(-0.1705908f, 0.832377f, -0.3322245f), new Vector3(-0.08829321f, 0.8480405f, -0.3478877f),
                new Vector3(0.2885871f, -0.7890103f, -0.2885871f), new Vector3(0.3097169f, -0.7412519f, -0.3097168f),
                new Vector3(0.3097169f, -0.8101402f, -0.2408286f), new Vector3(0.3322245f, -0.8326479f, -0.1705908f),
                new Vector3(0.3478877f, -0.8483111f, -0.08829316f), new Vector3(0.3534455f, -0.8535981f, 5.559791E-08f),
                new Vector3(0.3322245f, -0.6710142f, -0.3322245f), new Vector3(0.3478878f, -0.5887166f, -0.3478877f),
                new Vector3(0.3534455f, -0.5004234f, -0.3534455f), new Vector3(0.3097169f, -0.8098695f, 0.2408286f),
                new Vector3(0.2885871f, -0.7887395f, 0.2885871f), new Vector3(0.3322245f, -0.8323771f, 0.1705908f),
                new Vector3(0.3478877f, -0.8480403f, 0.08829328f), new Vector3(0.3534455f, -0.8535981f, 5.559791E-08f),
                new Vector3(0.4134532f, -0.7810512f, 5.559791E-08f),
                new Vector3(0.4597643f, -0.6962755f, 5.664037E-08f),
                new Vector3(0.4895242f, -0.6012138f, 5.559791E-08f), new Vector3(0.5f, -0.5001526f, 2.698768E-08f),
                new Vector3(0.2885871f, 0.7887397f, 0.2885871f), new Vector3(0.3097169f, 0.8098695f, 0.2408287f),
                new Vector3(0.3322246f, 0.832377f, 0.1705909f), new Vector3(0.3478878f, 0.8480405f, 0.08829328f),
                new Vector3(0.3534456f, 0.8535982f, 2.698768E-08f), new Vector3(0.3097169f, 0.8098695f, -0.2408286f),
                new Vector3(0.3097169f, 0.7409813f, -0.3097168f), new Vector3(0.2885871f, 0.7887397f, -0.288587f),
                new Vector3(0.3322245f, 0.6707435f, -0.3322245f), new Vector3(0.3478878f, 0.5884459f, -0.3478877f),
                new Vector3(0.3534455f, 0.5004234f, -0.3534455f), new Vector3(0.3322246f, 0.832377f, -0.1705908f),
                new Vector3(0.3478878f, 0.8480405f, -0.08829317f), new Vector3(0.3534456f, 0.8535982f, 2.698768E-08f),
                new Vector3(0.4134531f, 0.7810512f, 2.698768E-08f), new Vector3(0.4597644f, 0.6962755f, 2.629271E-08f),
                new Vector3(0.4895243f, 0.6012142f, 2.698768E-08f), new Vector3(0.5f, 0.5001526f, 2.698768E-08f),
                new Vector3(0.2885871f, -0.7887395f, 0.2885871f), new Vector3(0.3097169f, -0.7409812f, 0.3097169f),
                new Vector3(0.2408287f, -0.8098693f, 0.3097169f), new Vector3(0.1705909f, -0.8323771f, 0.3322245f),
                new Vector3(0.08829332f, -0.8480403f, 0.3478877f), new Vector3(9.853311E-08f, -0.8535981f, 0.3534455f),
                new Vector3(0.3322245f, -0.6707435f, 0.3322246f), new Vector3(0.3478878f, -0.5884458f, 0.3478878f),
                new Vector3(0.3534455f, -0.5001526f, 0.3534455f), new Vector3(-0.2408286f, -0.8098695f, 0.3097169f),
                new Vector3(-0.3097168f, -0.7409812f, 0.3097169f), new Vector3(-0.2885871f, -0.7887394f, 0.2885871f),
                new Vector3(-0.3322245f, -0.6707435f, 0.3322246f), new Vector3(-0.3478877f, -0.5884458f, 0.3478878f),
                new Vector3(-0.3534455f, -0.5001526f, 0.3534455f), new Vector3(-0.1705907f, -0.8323771f, 0.3322245f),
                new Vector3(-0.08829313f, -0.8480403f, 0.3478877f), new Vector3(-0.288587f, 0.7887397f, 0.2885871f),
                new Vector3(-0.3097168f, 0.7409813f, 0.3097169f), new Vector3(-0.2408286f, 0.8098695f, 0.3097169f),
                new Vector3(-0.1705908f, 0.832377f, 0.3322246f), new Vector3(-0.08829319f, 0.8480405f, 0.3478878f),
                new Vector3(-1.59078E-08f, 0.8535982f, 0.3534456f), new Vector3(-0.3322245f, 0.6707435f, 0.3322246f),
                new Vector3(-0.3478878f, 0.5884459f, 0.3478878f), new Vector3(-0.3534454f, 0.5001526f, 0.3534455f),
                new Vector3(0.2408287f, 0.8098695f, 0.3097169f), new Vector3(0.3097169f, 0.7409813f, 0.3097169f),
                new Vector3(0.2885871f, 0.7887397f, 0.2885871f), new Vector3(0.3322245f, 0.6707435f, 0.3322246f),
                new Vector3(0.3478878f, 0.5884459f, 0.3478878f), new Vector3(0.3534455f, 0.5001526f, 0.3534455f),
                new Vector3(0.1705909f, 0.8323771f, 0.3322246f), new Vector3(0.08829325f, 0.8480405f, 0.3478878f),
            };

            int[] indices = new[]
            {
                178, 109, 8, 177, 109, 178, 174, 177, 178, 174, 178, 175, 175, 178, 100, 178, 418, 100, 177, 108, 109,
                173, 177, 174, 176, 108, 177, 173, 176, 177, 171, 174, 175, 170, 173, 174, 170, 174, 171, 48, 176, 173,
                47, 48, 173, 47, 173, 170, 0, 47, 170, 0, 170, 26, 26, 170, 171, 48, 49, 176, 26, 171, 27, 49, 107, 176,
                176, 107, 108, 49, 17, 107, 27, 171, 172, 171, 175, 172, 27, 172, 28, 199, 107, 17, 199, 17, 46, 202,
                108, 107, 202, 107, 199, 172, 175, 99, 175, 100, 99, 28, 172, 98, 172, 99, 98, 28, 98, 14, 198, 199, 46,
                198, 46, 45, 14, 98, 185, 14, 185, 31, 98, 99, 186, 98, 186, 185, 99, 100, 187, 99, 187, 186, 31, 185,
                182, 31, 182, 30, 100, 103, 187, 100, 419, 103, 187, 103, 102, 187, 102, 184, 186, 187, 184, 184, 102,
                101, 185, 186, 183, 186, 184, 183, 185, 183, 182, 184, 101, 181, 183, 184, 181, 181, 101, 15, 181, 15,
                34, 183, 181, 180, 180, 181, 34, 182, 183, 180, 180, 34, 33, 182, 180, 179, 179, 180, 33, 30, 182, 179,
                179, 33, 32, 30, 179, 29, 29, 179, 32, 29, 32, 2, 37, 420, 421, 37, 421, 194, 36, 37, 194, 194, 421,
                422, 36, 194, 191, 35, 36, 191, 194, 422, 195, 191, 194, 195, 195, 422, 423, 35, 191, 188, 3, 35, 188,
                3, 188, 38, 191, 195, 192, 188, 191, 192, 195, 423, 196, 192, 195, 196, 196, 423, 424, 38, 188, 189,
                188, 192, 189, 38, 189, 39, 192, 196, 193, 189, 192, 193, 39, 189, 190, 189, 193, 190, 39, 190, 40, 193,
                196, 106, 196, 425, 106, 40, 190, 104, 40, 104, 16, 190, 193, 105, 193, 106, 105, 190, 105, 104, 16,
                104, 203, 16, 203, 43, 104, 105, 204, 104, 204, 203, 105, 106, 205, 105, 205, 204, 43, 203, 200, 43,
                200, 42, 106, 109, 205, 106, 426, 109, 205, 109, 108, 205, 108, 202, 204, 205, 202, 203, 204, 201, 204,
                202, 201, 203, 201, 200, 201, 202, 199, 201, 199, 198, 200, 201, 198, 42, 200, 197, 200, 198, 197, 197,
                198, 45, 42, 197, 41, 197, 45, 44, 41, 197, 44, 41, 44, 1, 214, 121, 9, 213, 121, 214, 210, 213, 214,
                210, 214, 211, 211, 214, 112, 214, 427, 112, 213, 120, 121, 209, 213, 210, 212, 120, 213, 209, 212, 213,
                207, 210, 211, 206, 209, 210, 206, 210, 207, 72, 212, 209, 71, 72, 209, 71, 209, 206, 4, 71, 206, 4,
                206, 50, 50, 206, 207, 72, 73, 212, 50, 207, 51, 73, 119, 212, 212, 119, 120, 73, 21, 119, 51, 207, 208,
                207, 211, 208, 51, 208, 52, 235, 119, 21, 235, 21, 70, 238, 120, 119, 238, 119, 235, 208, 211, 111, 211,
                112, 111, 52, 208, 110, 208, 111, 110, 52, 110, 18, 234, 235, 70, 234, 70, 69, 18, 110, 221, 18, 221,
                55, 110, 111, 222, 110, 222, 221, 111, 112, 223, 111, 223, 222, 55, 221, 218, 55, 218, 54, 221, 222,
                219, 221, 219, 218, 54, 218, 215, 54, 215, 53, 53, 215, 56, 53, 56, 5, 215, 57, 56, 218, 216, 215, 215,
                216, 57, 218, 219, 216, 216, 58, 57, 216, 217, 58, 219, 217, 216, 217, 19, 58, 222, 220, 219, 219, 220,
                217, 222, 223, 220, 217, 113, 19, 220, 113, 217, 61, 19, 113, 223, 114, 220, 220, 114, 113, 61, 113,
                230, 230, 113, 114, 60, 61, 230, 223, 115, 114, 112, 115, 223, 112, 428, 115, 60, 230, 227, 59, 60, 227,
                230, 114, 231, 231, 114, 115, 227, 230, 231, 59, 227, 224, 7, 59, 224, 7, 224, 62, 231, 115, 232, 232,
                115, 429, 227, 231, 228, 224, 227, 228, 228, 231, 232, 62, 224, 225, 224, 228, 225, 62, 225, 63, 228,
                232, 229, 225, 228, 229, 63, 225, 226, 225, 229, 226, 63, 226, 64, 229, 232, 118, 232, 430, 118, 229,
                118, 117, 226, 229, 117, 226, 117, 116, 64, 226, 116, 64, 116, 20, 65, 68, 6, 65, 233, 68, 233, 69, 68,
                66, 233, 65, 233, 234, 69, 66, 236, 233, 236, 234, 233, 67, 236, 66, 236, 237, 234, 237, 235, 234, 237,
                238, 235, 67, 239, 236, 239, 237, 236, 431, 239, 67, 431, 432, 239, 239, 240, 237, 432, 240, 239, 240,
                238, 237, 432, 433, 240, 240, 241, 238, 433, 241, 240, 241, 120, 238, 433, 434, 241, 241, 121, 120, 434,
                121, 241, 434, 435, 121, 441, 74, 442, 441, 251, 74, 443, 251, 441, 251, 75, 74, 443, 254, 251, 444,
                254, 443, 251, 252, 75, 254, 252, 251, 252, 76, 75, 444, 257, 254, 440, 257, 444, 252, 253, 76, 253,
                390, 76, 254, 255, 252, 257, 255, 254, 255, 253, 252, 440, 122, 257, 439, 122, 440, 253, 394, 390, 22,
                390, 394, 255, 256, 253, 256, 394, 253, 257, 258, 255, 122, 258, 257, 258, 256, 255, 439, 244, 122, 438,
                244, 439, 22, 394, 125, 79, 22, 125, 122, 123, 258, 244, 123, 122, 256, 395, 394, 125, 394, 395, 258,
                259, 256, 123, 259, 258, 259, 395, 256, 438, 243, 244, 437, 243, 438, 79, 125, 266, 78, 79, 266, 244,
                247, 123, 243, 247, 244, 123, 124, 259, 247, 124, 123, 259, 396, 395, 124, 396, 259, 125, 395, 126, 266,
                125, 126, 126, 395, 396, 437, 242, 243, 436, 242, 437, 436, 83, 242, 243, 246, 247, 242, 246, 243, 83,
                245, 242, 242, 245, 246, 83, 84, 245, 247, 250, 124, 246, 250, 247, 124, 386, 396, 250, 386, 124, 126,
                396, 127, 127, 396, 386, 84, 248, 245, 84, 85, 248, 245, 249, 246, 246, 249, 250, 245, 248, 249, 250,
                399, 386, 249, 399, 250, 85, 397, 248, 85, 391, 397, 248, 398, 249, 249, 398, 399, 248, 397, 398, 10,
                386, 399, 127, 386, 10, 267, 126, 127, 266, 126, 267, 131, 397, 391, 131, 391, 23, 133, 399, 398, 10,
                399, 133, 132, 398, 397, 132, 397, 131, 133, 398, 132, 268, 127, 10, 267, 127, 268, 263, 266, 267, 78,
                266, 263, 77, 78, 263, 271, 131, 23, 271, 23, 82, 130, 10, 133, 268, 10, 130, 274, 132, 131, 274, 131,
                271, 277, 133, 132, 130, 133, 277, 277, 132, 274, 263, 267, 264, 264, 267, 268, 77, 263, 260, 260, 263,
                264, 445, 77, 260, 445, 260, 446, 446, 260, 261, 260, 264, 261, 446, 261, 447, 264, 268, 265, 265, 268,
                130, 261, 264, 265, 447, 261, 262, 261, 265, 262, 447, 262, 448, 265, 130, 129, 262, 265, 129, 129, 130,
                277, 448, 262, 128, 262, 129, 128, 448, 128, 449, 129, 277, 276, 128, 129, 276, 276, 277, 274, 449, 128,
                275, 128, 276, 275, 449, 275, 453, 276, 274, 273, 275, 276, 273, 273, 274, 271, 453, 275, 272, 275, 273,
                272, 453, 272, 452, 273, 271, 270, 272, 273, 270, 270, 271, 82, 270, 82, 81, 452, 272, 269, 272, 270,
                269, 269, 270, 81, 452, 269, 450, 269, 81, 80, 450, 269, 80, 450, 80, 451, 463, 86, 464, 463, 287, 86,
                465, 287, 463, 287, 87, 86, 465, 290, 287, 466, 290, 465, 287, 288, 87, 290, 288, 287, 288, 88, 87, 466,
                293, 290, 459, 293, 466, 288, 289, 88, 289, 392, 88, 290, 291, 288, 293, 291, 290, 291, 289, 288, 459,
                134, 293, 458, 134, 459, 289, 400, 392, 24, 392, 400, 291, 292, 289, 292, 400, 289, 293, 294, 291, 134,
                294, 293, 294, 292, 291, 458, 280, 134, 457, 280, 458, 24, 400, 137, 91, 24, 137, 134, 135, 294, 280,
                135, 134, 292, 401, 400, 137, 400, 401, 294, 295, 292, 135, 295, 294, 295, 401, 292, 457, 279, 280, 456,
                279, 457, 91, 137, 302, 90, 91, 302, 280, 283, 135, 279, 283, 280, 135, 136, 295, 283, 136, 135, 295,
                402, 401, 136, 402, 295, 137, 401, 138, 302, 137, 138, 138, 401, 402, 456, 278, 279, 454, 278, 456, 454,
                455, 278, 279, 282, 283, 278, 282, 279, 455, 281, 278, 278, 281, 282, 455, 460, 281, 283, 286, 136, 282,
                286, 283, 136, 387, 402, 286, 387, 136, 138, 402, 139, 139, 402, 387, 460, 284, 281, 460, 461, 284, 281,
                285, 282, 282, 285, 286, 281, 284, 285, 286, 405, 387, 285, 405, 286, 461, 403, 284, 461, 462, 403, 284,
                404, 285, 285, 404, 405, 284, 403, 404, 11, 387, 405, 139, 387, 11, 303, 138, 139, 302, 138, 303, 143,
                403, 462, 143, 462, 477, 145, 405, 404, 11, 405, 145, 144, 404, 403, 144, 403, 143, 145, 404, 144, 304,
                139, 11, 303, 139, 304, 299, 302, 303, 90, 302, 299, 89, 90, 299, 307, 143, 477, 307, 477, 476, 142, 11,
                145, 304, 11, 142, 310, 144, 143, 310, 143, 307, 313, 145, 144, 142, 145, 313, 313, 144, 310, 299, 303,
                300, 300, 303, 304, 89, 299, 296, 296, 299, 300, 467, 89, 296, 467, 296, 468, 468, 296, 297, 296, 300,
                297, 468, 297, 469, 300, 304, 301, 301, 304, 142, 297, 300, 301, 469, 297, 298, 297, 301, 298, 469, 298,
                470, 301, 142, 141, 298, 301, 141, 141, 142, 313, 470, 298, 140, 298, 141, 140, 470, 140, 471, 141, 313,
                312, 140, 141, 312, 312, 313, 310, 471, 140, 311, 140, 312, 311, 471, 311, 479, 312, 310, 309, 311, 312,
                309, 309, 310, 307, 479, 311, 308, 311, 309, 308, 479, 308, 478, 309, 307, 306, 308, 309, 306, 306, 307,
                476, 306, 476, 475, 478, 308, 305, 308, 306, 305, 305, 306, 475, 478, 305, 472, 305, 475, 473, 472, 305,
                473, 472, 473, 474, 484, 146, 485, 484, 316, 146, 483, 316, 484, 316, 147, 146, 483, 315, 316, 482, 315,
                483, 316, 319, 147, 315, 319, 316, 319, 148, 147, 482, 314, 315, 480, 314, 482, 480, 481, 314, 315, 318,
                319, 314, 318, 315, 319, 322, 148, 318, 322, 319, 322, 388, 148, 481, 317, 314, 314, 317, 318, 481, 486,
                317, 318, 321, 322, 317, 321, 318, 322, 411, 388, 321, 411, 322, 515, 388, 411, 486, 320, 317, 317, 320,
                321, 486, 487, 320, 515, 411, 157, 514, 515, 157, 321, 410, 411, 320, 410, 321, 157, 411, 410, 487, 409,
                320, 320, 409, 410, 487, 488, 409, 514, 157, 349, 513, 514, 349, 157, 410, 156, 156, 410, 409, 349, 157,
                156, 155, 409, 488, 156, 409, 155, 155, 488, 508, 513, 349, 348, 512, 513, 348, 349, 156, 346, 346, 156,
                155, 348, 349, 346, 343, 155, 508, 346, 155, 343, 343, 508, 507, 512, 348, 347, 511, 512, 347, 511, 347,
                510, 348, 346, 345, 345, 346, 343, 347, 348, 345, 342, 343, 507, 345, 343, 342, 342, 507, 506, 510, 347,
                344, 347, 345, 344, 344, 345, 342, 510, 344, 509, 341, 342, 506, 344, 342, 341, 509, 344, 341, 341, 506,
                504, 509, 341, 503, 503, 341, 504, 503, 504, 505, 489, 92, 490, 489, 323, 92, 491, 323, 489, 323, 93,
                92, 491, 326, 323, 492, 326, 491, 323, 324, 93, 326, 324, 323, 324, 94, 93, 492, 329, 326, 493, 329,
                492, 493, 494, 329, 324, 325, 94, 325, 393, 94, 326, 327, 324, 329, 327, 326, 327, 325, 324, 494, 330,
                329, 329, 330, 327, 494, 495, 330, 325, 406, 393, 25, 393, 406, 327, 328, 325, 330, 328, 327, 328, 406,
                325, 495, 331, 330, 330, 331, 328, 495, 496, 331, 25, 406, 149, 97, 25, 149, 328, 407, 406, 331, 407,
                328, 149, 406, 407, 496, 408, 331, 331, 408, 407, 496, 497, 408, 97, 149, 338, 96, 97, 338, 149, 407,
                150, 150, 407, 408, 338, 149, 150, 151, 408, 497, 150, 408, 151, 151, 497, 12, 96, 338, 335, 95, 96,
                335, 338, 150, 339, 339, 150, 151, 335, 338, 339, 340, 151, 12, 339, 151, 340, 340, 12, 154, 95, 335,
                332, 498, 95, 332, 498, 332, 499, 335, 339, 336, 336, 339, 340, 332, 335, 336, 337, 340, 154, 336, 340,
                337, 337, 154, 153, 499, 332, 333, 332, 336, 333, 333, 336, 337, 499, 333, 500, 334, 337, 153, 333, 337,
                334, 500, 333, 334, 334, 153, 152, 500, 334, 501, 501, 334, 152, 501, 152, 502, 525, 526, 527, 525, 359,
                526, 531, 359, 525, 359, 528, 526, 531, 362, 359, 532, 362, 531, 359, 360, 528, 362, 360, 359, 360, 529,
                528, 532, 365, 362, 521, 365, 532, 360, 361, 529, 361, 530, 529, 362, 363, 360, 365, 363, 362, 363, 361,
                360, 521, 158, 365, 520, 158, 521, 361, 412, 530, 541, 530, 412, 363, 364, 361, 364, 412, 361, 365, 366,
                363, 158, 366, 365, 366, 364, 363, 520, 352, 158, 519, 352, 520, 541, 412, 161, 540, 541, 161, 158, 159,
                366, 352, 159, 158, 364, 413, 412, 161, 412, 413, 366, 367, 364, 159, 367, 366, 367, 413, 364, 519, 351,
                352, 518, 351, 519, 540, 161, 374, 539, 540, 374, 352, 355, 159, 351, 355, 352, 159, 160, 367, 355, 160,
                159, 367, 414, 413, 160, 414, 367, 161, 413, 162, 374, 161, 162, 162, 413, 414, 518, 350, 351, 516, 350,
                518, 516, 517, 350, 351, 354, 355, 350, 354, 351, 517, 353, 350, 350, 353, 354, 517, 522, 353, 355, 358,
                160, 354, 358, 355, 160, 389, 414, 358, 389, 160, 162, 414, 163, 163, 414, 389, 522, 356, 353, 522, 523,
                356, 353, 357, 354, 354, 357, 358, 353, 356, 357, 358, 417, 389, 357, 417, 358, 523, 415, 356, 523, 524,
                415, 356, 416, 357, 357, 416, 417, 356, 415, 416, 13, 389, 417, 163, 389, 13, 375, 162, 163, 374, 162,
                375, 167, 415, 524, 167, 524, 547, 169, 417, 416, 13, 417, 169, 168, 416, 415, 168, 415, 167, 169, 416,
                168, 376, 163, 13, 375, 163, 376, 371, 374, 375, 539, 374, 371, 534, 539, 371, 379, 167, 547, 379, 547,
                546, 166, 13, 169, 376, 13, 166, 382, 168, 167, 382, 167, 379, 385, 169, 168, 166, 169, 385, 385, 168,
                382, 371, 375, 372, 372, 375, 376, 534, 371, 368, 368, 371, 372, 533, 534, 368, 533, 368, 535, 535, 368,
                369, 368, 372, 369, 535, 369, 536, 372, 376, 373, 373, 376, 166, 369, 372, 373, 536, 369, 370, 369, 373,
                370, 536, 370, 537, 373, 166, 165, 370, 373, 165, 165, 166, 385, 537, 370, 164, 370, 165, 164, 537, 164,
                538, 165, 385, 384, 164, 165, 384, 384, 385, 382, 538, 164, 383, 164, 384, 383, 538, 383, 549, 384, 382,
                381, 383, 384, 381, 381, 382, 379, 549, 383, 380, 383, 381, 380, 549, 380, 548, 381, 379, 378, 380, 381,
                378, 378, 379, 546, 378, 546, 545, 548, 380, 377, 380, 378, 377, 377, 378, 545, 548, 377, 542, 377, 545,
                543, 542, 377, 543, 542, 543, 544,
            };

            Vector3[] normals = new[]
            {
                new Vector3(-0.5773504f, -0.57735f, 0.5773504f), new Vector3(-0.5773505f, -0.5773501f, -0.5773501f),
                new Vector3(0.5773503f, -0.5773502f, 0.5773504f), new Vector3(0.5773503f, -0.5773501f, -0.5773506f),
                new Vector3(-0.5773504f, 0.5773503f, 0.5773502f), new Vector3(-0.5773504f, 0.5773503f, -0.5773502f),
                new Vector3(0.5773503f, 0.5773502f, 0.5773503f), new Vector3(0.5773504f, 0.5773502f, -0.5773503f),
                new Vector3(0f, -0.9999992f, 0.001340148f), new Vector3(0f, 1f, 0f),
                new Vector3(-0.9987108f, 0.05076177f, 3.399785E-05f), new Vector3(0f, 0.05086477f, -0.9987055f),
                new Vector3(0.9987108f, 0.05076212f, 3.399035E-05f), new Vector3(0f, 0.05072768f, 0.9987125f),
                new Vector3(0f, -0.7071061f, 0.7071074f), new Vector3(0.7071055f, -0.7071072f, 0.001062795f),
                new Vector3(0f, -0.7071066f, -0.7071069f), new Vector3(-0.7071055f, -0.7071072f, 0.001062799f),
                new Vector3(-0.7071064f, 0.7071071f, 0f), new Vector3(0f, 0.7071069f, -0.7071066f),
                new Vector3(0.7071071f, 0.7071064f, 0f), new Vector3(0f, 0.7071066f, 0.7071069f),
                new Vector3(-0.7063879f, 0.04508317f, -0.7063878f), new Vector3(-0.7063919f, 0.04494741f, 0.7063924f),
                new Vector3(0.7063882f, 0.04508324f, -0.7063874f), new Vector3(0.7063923f, 0.04494748f, 0.706392f),
                new Vector3(-0.480549f, -0.6201098f, 0.6201102f), new Vector3(-0.3411001f, -0.6646994f, 0.6646996f),
                new Vector3(-0.1769256f, -0.6959512f, 0.695952f), new Vector3(0.4805491f, -0.6201099f, 0.6201099f),
                new Vector3(0.3411002f, -0.6646994f, 0.6646994f), new Vector3(0.1769256f, -0.6959513f, 0.695952f),
                new Vector3(0.6201102f, -0.6201099f, 0.4805489f), new Vector3(0.6646997f, -0.6646991f, 0.3411002f),
                new Vector3(0.6959521f, -0.695951f, 0.1769261f), new Vector3(0.6201102f, -0.62011f, -0.4805488f),
                new Vector3(0.6646997f, -0.6646991f, -0.3411001f), new Vector3(0.6960831f, -0.696085f, -0.1758805f),
                new Vector3(0.4805489f, -0.6201099f, -0.6201102f), new Vector3(0.3411006f, -0.6646994f, -0.6646992f),
                new Vector3(0.1769258f, -0.6959516f, -0.6959516f), new Vector3(-0.4805489f, -0.6201102f, -0.6201099f),
                new Vector3(-0.3411001f, -0.6646996f, -0.6646993f), new Vector3(-0.1769258f, -0.6959516f, -0.6959516f),
                new Vector3(-0.62011f, -0.6201099f, -0.4805491f), new Vector3(-0.6646993f, -0.6646994f, -0.3411004f),
                new Vector3(-0.696083f, -0.6960853f, -0.1758801f), new Vector3(-0.62011f, -0.6201099f, 0.4805491f),
                new Vector3(-0.6646993f, -0.6646994f, 0.3411005f), new Vector3(-0.6959519f, -0.6959513f, 0.1769257f),
                new Vector3(-0.62011f, 0.6201099f, 0.4805489f), new Vector3(-0.6646993f, 0.6646993f, 0.3411008f),
                new Vector3(-0.6959514f, 0.6959519f, 0.1769255f), new Vector3(-0.62011f, 0.6201099f, -0.4805489f),
                new Vector3(-0.6646993f, 0.6646991f, -0.3411008f), new Vector3(-0.6959515f, 0.6959518f, -0.1769255f),
                new Vector3(-0.4805488f, 0.6201099f, -0.6201102f), new Vector3(-0.3411007f, 0.664699f, -0.6646997f),
                new Vector3(-0.1769257f, 0.6959515f, -0.6959518f), new Vector3(0.480549f, 0.6201099f, -0.62011f),
                new Vector3(0.3411007f, 0.6646991f, -0.6646996f), new Vector3(0.1769254f, 0.6959516f, -0.6959516f),
                new Vector3(0.6201104f, 0.6201097f, -0.4805486f), new Vector3(0.6647001f, 0.6646987f, -0.3411004f),
                new Vector3(0.6959522f, 0.6959511f, -0.1769257f), new Vector3(0.6201104f, 0.6201097f, 0.4805486f),
                new Vector3(0.6647f, 0.6646987f, 0.3411006f), new Vector3(0.6959522f, 0.6959511f, 0.1769257f),
                new Vector3(0.4805491f, 0.6201098f, 0.62011f), new Vector3(0.3411005f, 0.6646991f, 0.6646996f),
                new Vector3(0.1769253f, 0.6959515f, 0.6959518f), new Vector3(-0.4805489f, 0.6201097f, 0.6201102f),
                new Vector3(-0.3411008f, 0.6646989f, 0.6646997f), new Vector3(-0.1769256f, 0.6959513f, 0.6959519f),
                new Vector3(-0.6201099f, -0.4805492f, -0.6201098f), new Vector3(-0.6646992f, -0.3411005f, -0.6646994f),
                new Vector3(-0.6959518f, -0.1769254f, -0.6959516f), new Vector3(-0.6201098f, 0.4805492f, -0.62011f),
                new Vector3(-0.664699f, 0.3411009f, -0.6646994f), new Vector3(-0.6959345f, 0.1770589f, -0.6959348f),
                new Vector3(-0.6201096f, 0.4805492f, 0.62011f), new Vector3(-0.664699f, 0.3411008f, 0.6646996f),
                new Vector3(-0.6959513f, 0.176925f, 0.6959522f), new Vector3(-0.6201097f, -0.4805492f, 0.62011f),
                new Vector3(-0.6646991f, -0.3411005f, 0.6646997f), new Vector3(-0.6959514f, -0.1769252f, 0.6959519f),
                new Vector3(0.6201101f, -0.4805487f, -0.6201102f), new Vector3(0.6646994f, -0.3411006f, -0.6646991f),
                new Vector3(0.6959521f, -0.1769258f, -0.695951f), new Vector3(0.6201102f, 0.4805487f, -0.6201099f),
                new Vector3(0.6646997f, 0.3411005f, -0.664699f), new Vector3(0.6959351f, 0.177059f, -0.6959342f),
                new Vector3(0.6201101f, -0.4805489f, 0.6201099f), new Vector3(0.6646996f, -0.3411004f, 0.6646991f),
                new Vector3(0.6959518f, -0.1769256f, 0.6959515f), new Vector3(0.62011f, 0.4805488f, 0.6201099f),
                new Vector3(0.6646997f, 0.3411004f, 0.6646991f), new Vector3(0.695952f, 0.1769251f, 0.6959514f),
                new Vector3(0f, -0.8287547f, 0.5596121f), new Vector3(0f, -0.9212478f, 0.3889762f),
                new Vector3(0f, -0.9798718f, 0.1996277f), new Vector3(0.5595933f, -0.8287665f, 0.001222175f),
                new Vector3(0.3889485f, -0.9212585f, 0.001304361f), new Vector3(0.1996068f, -0.9798751f, 0.001334521f),
                new Vector3(0f, -0.8287548f, -0.5596118f), new Vector3(0f, -0.9212478f, -0.3889762f),
                new Vector3(0f, -0.9801384f, -0.1983145f), new Vector3(-0.5595933f, -0.8287665f, 0.001222173f),
                new Vector3(-0.3889485f, -0.9212585f, 0.001304359f), new Vector3(-0.1996065f, -0.9798752f, 0.00133452f),
                new Vector3(-0.5596119f, 0.8287549f, 0f), new Vector3(-0.388977f, 0.9212475f, 0f),
                new Vector3(-0.1996275f, 0.9798718f, 0f), new Vector3(0f, 0.8287548f, -0.5596118f),
                new Vector3(0f, 0.9212476f, -0.3889768f), new Vector3(0f, 0.9798719f, -0.1996274f),
                new Vector3(0.5596119f, 0.8287548f, 0f), new Vector3(0.3889765f, 0.9212477f, 0f),
                new Vector3(0.1996274f, 0.9798719f, 0f), new Vector3(0f, 0.8287548f, 0.5596118f),
                new Vector3(0f, 0.9212476f, 0.3889768f), new Vector3(0f, 0.9798719f, 0.1996274f),
                new Vector3(-0.8287613f, -0.5596018f, 0.0008282576f),
                new Vector3(-0.9212518f, -0.3889665f, 0.0005541048f),
                new Vector3(-0.9797957f, -0.2000009f, 0.0002745861f), new Vector3(-0.827427f, 0.04590931f, -0.5596935f),
                new Vector3(-0.9197697f, 0.04781944f, -0.3895342f), new Vector3(-0.978385f, 0.04984403f, -0.2006948f),
                new Vector3(-0.8287547f, 0.5596122f, 0f), new Vector3(-0.9212477f, 0.3889765f, 0f),
                new Vector3(-0.9797882f, 0.2000375f, 3.410011E-05f), new Vector3(-0.8274335f, 0.04577361f, 0.5596952f),
                new Vector3(-0.9197769f, 0.04768372f, 0.389534f), new Vector3(-0.9783834f, 0.04974189f, 0.2007277f),
                new Vector3(0f, -0.5596122f, -0.8287545f), new Vector3(0f, -0.3889765f, -0.9212477f),
                new Vector3(0f, -0.200005f, -0.9797949f), new Vector3(0.5596935f, 0.04590956f, -0.827427f),
                new Vector3(0.389534f, 0.0478196f, -0.9197698f), new Vector3(0.2007285f, 0.04987827f, -0.9783763f),
                new Vector3(0f, 0.5596118f, -0.8287548f), new Vector3(0f, 0.3889762f, -0.9212478f),
                new Vector3(0f, 0.2001389f, -0.9797676f), new Vector3(-0.5596935f, 0.04590972f, -0.827427f),
                new Vector3(-0.3895341f, 0.04781964f, -0.9197698f), new Vector3(-0.2007285f, 0.04987822f, -0.9783763f),
                new Vector3(0.8287613f, -0.5596017f, 0.0008282595f), new Vector3(0.921252f, -0.3889661f, 0.0005541104f),
                new Vector3(0.9797956f, -0.2000014f, 0.0002745824f), new Vector3(0.8274336f, 0.04577388f, 0.5596948f),
                new Vector3(0.9197769f, 0.04768387f, 0.389534f), new Vector3(0.9783834f, 0.04974211f, 0.200728f),
                new Vector3(0.8287549f, 0.5596119f, 0f), new Vector3(0.9212476f, 0.3889766f, 0f),
                new Vector3(0.9797882f, 0.2000377f, 3.410387E-05f), new Vector3(0.8274273f, 0.04590958f, -0.5596932f),
                new Vector3(0.9197697f, 0.04781957f, -0.3895342f), new Vector3(0.978385f, 0.04984423f, -0.2006951f),
                new Vector3(0f, -0.5596122f, 0.8287547f), new Vector3(0f, -0.3889768f, 0.9212476f),
                new Vector3(0f, -0.2000047f, 0.979795f), new Vector3(-0.5596952f, 0.04577408f, 0.8274335f),
                new Vector3(-0.3895338f, 0.04768405f, 0.919777f), new Vector3(-0.2007274f, 0.04974183f, 0.9783835f),
                new Vector3(0f, 0.5596117f, 0.8287549f), new Vector3(0f, 0.3889766f, 0.9212476f),
                new Vector3(0f, 0.2000046f, 0.979795f), new Vector3(0.5596953f, 0.0457739f, 0.8274333f),
                new Vector3(0.3895338f, 0.04768401f, 0.919777f), new Vector3(0.2007274f, 0.04974188f, 0.9783835f),
                new Vector3(-0.4963574f, -0.7122208f, 0.4963575f), new Vector3(-0.3492366f, -0.774617f, 0.5272593f),
                new Vector3(-0.180537f, -0.8148447f, 0.550849f), new Vector3(-0.5272589f, -0.7746171f, 0.3492368f),
                new Vector3(-0.3670154f, -0.8547511f, 0.3670154f), new Vector3(-0.1887284f, -0.9043404f, 0.3828187f),
                new Vector3(-0.5508487f, -0.8148449f, 0.1805373f), new Vector3(-0.3828184f, -0.9043404f, 0.1887285f),
                new Vector3(-0.1964684f, -0.9606249f, 0.1964684f), new Vector3(0.4963577f, -0.7122204f, 0.4963577f),
                new Vector3(0.5272598f, -0.7746167f, 0.3492366f), new Vector3(0.5508491f, -0.8148445f, 0.1805368f),
                new Vector3(0.3492367f, -0.7746165f, 0.5272599f), new Vector3(0.3670155f, -0.8547511f, 0.3670154f),
                new Vector3(0.3828185f, -0.9043406f, 0.1887276f), new Vector3(0.1805366f, -0.8148445f, 0.5508493f),
                new Vector3(0.1887278f, -0.9043405f, 0.3828186f), new Vector3(0.1964683f, -0.960625f, 0.196468f),
                new Vector3(0.4963575f, -0.7122206f, -0.4963576f), new Vector3(0.3492367f, -0.7746166f, -0.5272598f),
                new Vector3(0.1805367f, -0.8148445f, -0.5508491f), new Vector3(0.5272598f, -0.7746167f, -0.3492366f),
                new Vector3(0.3670156f, -0.8547511f, -0.3670154f), new Vector3(0.1887278f, -0.9043405f, -0.3828186f),
                new Vector3(0.550954f, -0.815039f, -0.1793351f), new Vector3(0.382887f, -0.9045779f, -0.187447f),
                new Vector3(0.1964995f, -0.9608853f, -0.1951596f), new Vector3(-0.4963573f, -0.712221f, -0.4963575f),
                new Vector3(-0.5272589f, -0.7746171f, -0.3492367f), new Vector3(-0.5509536f, -0.8150392f, -0.1793355f),
                new Vector3(-0.3492365f, -0.7746171f, -0.5272591f), new Vector3(-0.3670154f, -0.8547511f, -0.3670154f),
                new Vector3(-0.382887f, -0.9045777f, -0.1874478f), new Vector3(-0.180537f, -0.8148447f, -0.5508488f),
                new Vector3(-0.1887284f, -0.9043404f, -0.3828187f), new Vector3(-0.1964996f, -0.9608852f, -0.19516f),
                new Vector3(-0.4963579f, 0.7122203f, 0.4963578f), new Vector3(-0.5272598f, 0.7746165f, 0.3492368f),
                new Vector3(-0.5508491f, 0.8148448f, 0.180536f), new Vector3(-0.349237f, 0.7746164f, 0.5272598f),
                new Vector3(-0.3670156f, 0.8547511f, 0.3670152f), new Vector3(-0.3828191f, 0.9043404f, 0.1887274f),
                new Vector3(-0.1805361f, 0.8148447f, 0.5508493f), new Vector3(-0.1887276f, 0.9043404f, 0.3828189f),
                new Vector3(-0.1964681f, 0.9606251f, 0.196468f), new Vector3(-0.4963578f, 0.7122203f, -0.4963579f),
                new Vector3(-0.3492369f, 0.7746165f, -0.5272598f), new Vector3(-0.1805361f, 0.8148447f, -0.5508493f),
                new Vector3(-0.5272598f, 0.7746165f, -0.3492368f), new Vector3(-0.3670155f, 0.8547511f, -0.3670153f),
                new Vector3(-0.1887276f, 0.9043404f, -0.382819f), new Vector3(-0.5508491f, 0.8148448f, -0.180536f),
                new Vector3(-0.3828191f, 0.9043404f, -0.1887274f), new Vector3(-0.1964682f, 0.9606251f, -0.196468f),
                new Vector3(0.4963579f, 0.7122204f, -0.4963577f), new Vector3(0.5272598f, 0.7746166f, -0.3492367f),
                new Vector3(0.5508491f, 0.8148447f, -0.1805361f), new Vector3(0.3492368f, 0.7746166f, -0.5272597f),
                new Vector3(0.3670152f, 0.8547512f, -0.3670152f), new Vector3(0.3828186f, 0.9043406f, -0.1887276f),
                new Vector3(0.1805359f, 0.8148449f, -0.5508491f), new Vector3(0.1887275f, 0.9043406f, -0.3828187f),
                new Vector3(0.196468f, 0.9606251f, -0.1964681f), new Vector3(0.4963579f, 0.7122203f, 0.4963576f),
                new Vector3(0.3492366f, 0.7746167f, 0.5272596f), new Vector3(0.1805359f, 0.8148449f, 0.5508491f),
                new Vector3(0.5272598f, 0.7746164f, 0.3492369f), new Vector3(0.3670151f, 0.8547512f, 0.3670154f),
                new Vector3(0.1887275f, 0.9043405f, 0.3828189f), new Vector3(0.5508493f, 0.8148447f, 0.1805361f),
                new Vector3(0.3828185f, 0.9043406f, 0.1887276f), new Vector3(0.196468f, 0.9606251f, 0.1964681f),
                new Vector3(-0.7122203f, -0.4963579f, 0.4963578f), new Vector3(-0.7746164f, -0.52726f, 0.3492368f),
                new Vector3(-0.8148445f, -0.5508496f, 0.180536f), new Vector3(-0.7746165f, -0.3492368f, 0.5272598f),
                new Vector3(-0.8547508f, -0.3670157f, 0.3670158f), new Vector3(-0.9043403f, -0.3828192f, 0.1887273f),
                new Vector3(-0.8148445f, -0.1805358f, 0.5508496f), new Vector3(-0.9043404f, -0.1887271f, 0.3828191f),
                new Vector3(-0.960551f, -0.196649f, 0.1966492f), new Vector3(-0.7122203f, -0.4963579f, -0.4963578f),
                new Vector3(-0.7746165f, -0.3492368f, -0.5272598f), new Vector3(-0.8148445f, -0.1805358f, -0.5508494f),
                new Vector3(-0.7746164f, -0.52726f, -0.3492368f), new Vector3(-0.8547508f, -0.3670157f, -0.3670157f),
                new Vector3(-0.9043404f, -0.1887272f, -0.3828191f), new Vector3(-0.814975f, -0.5509228f, -0.1797215f),
                new Vector3(-0.9044408f, -0.3828496f, -0.1881833f), new Vector3(-0.9606047f, -0.196656f, -0.1963801f),
                new Vector3(-0.7122203f, 0.496358f, -0.4963577f), new Vector3(-0.7746162f, 0.5272601f, -0.3492371f),
                new Vector3(-0.8148445f, 0.5508495f, -0.1805363f), new Vector3(-0.7746164f, 0.3492373f, -0.5272597f),
                new Vector3(-0.8547508f, 0.3670158f, -0.3670157f), new Vector3(-0.9043404f, 0.382819f, -0.1887275f),
                new Vector3(-0.814823f, 0.1806695f, -0.5508377f), new Vector3(-0.9043157f, 0.1888608f, -0.3828117f),
                new Vector3(-0.9605377f, 0.19675f, -0.1966133f), new Vector3(-0.7122203f, 0.4963581f, 0.4963577f),
                new Vector3(-0.7746164f, 0.3492372f, 0.5272597f), new Vector3(-0.8148445f, 0.1805359f, 0.5508496f),
                new Vector3(-0.7746162f, 0.5272601f, 0.3492371f), new Vector3(-0.8547508f, 0.3670157f, 0.3670157f),
                new Vector3(-0.9043404f, 0.1887275f, 0.382819f), new Vector3(-0.8148445f, 0.5508494f, 0.1805363f),
                new Vector3(-0.9043405f, 0.3828189f, 0.1887275f), new Vector3(-0.9605509f, 0.1966492f, 0.1966493f),
                new Vector3(-0.4963577f, -0.4963578f, -0.7122204f), new Vector3(-0.3492366f, -0.52726f, -0.7746165f),
                new Vector3(-0.1805362f, -0.5508496f, -0.8148444f), new Vector3(-0.5272598f, -0.3492367f, -0.7746165f),
                new Vector3(-0.3670156f, -0.3670157f, -0.8547509f), new Vector3(-0.1887274f, -0.3828189f, -0.9043405f),
                new Vector3(-0.5508494f, -0.1805364f, -0.8148445f), new Vector3(-0.3828188f, -0.1887277f, -0.9043404f),
                new Vector3(-0.1966492f, -0.1966494f, -0.9605509f), new Vector3(0.4963577f, -0.4963575f, -0.7122207f),
                new Vector3(0.5272598f, -0.3492371f, -0.7746164f), new Vector3(0.5508495f, -0.1805366f, -0.8148443f),
                new Vector3(0.3492372f, -0.5272599f, -0.7746162f), new Vector3(0.3670161f, -0.3670158f, -0.8547505f),
                new Vector3(0.382819f, -0.1887276f, -0.9043403f), new Vector3(0.1805363f, -0.5508496f, -0.8148444f),
                new Vector3(0.1887275f, -0.382819f, -0.9043404f), new Vector3(0.1966494f, -0.1966493f, -0.9605509f),
                new Vector3(0.4963576f, 0.4963577f, -0.7122205f), new Vector3(0.3492368f, 0.5272598f, -0.7746165f),
                new Vector3(0.1805363f, 0.5508493f, -0.8148445f), new Vector3(0.5272598f, 0.349237f, -0.7746165f),
                new Vector3(0.3670156f, 0.3670157f, -0.8547509f), new Vector3(0.1887274f, 0.3828188f, -0.9043405f),
                new Vector3(0.5508376f, 0.1806698f, -0.8148229f), new Vector3(0.3828116f, 0.1888611f, -0.9043157f),
                new Vector3(0.1966459f, 0.1967829f, -0.9605243f), new Vector3(-0.4963574f, 0.4963577f, -0.7122207f),
                new Vector3(-0.5272595f, 0.3492371f, -0.7746167f), new Vector3(-0.5508375f, 0.1806699f, -0.814823f),
                new Vector3(-0.349237f, 0.5272598f, -0.7746165f), new Vector3(-0.3670158f, 0.3670159f, -0.8547507f),
                new Vector3(-0.3828117f, 0.1888611f, -0.9043155f), new Vector3(-0.1805366f, 0.5508492f, -0.8148445f),
                new Vector3(-0.1887276f, 0.382819f, -0.9043404f), new Vector3(-0.196646f, 0.196783f, -0.9605243f),
                new Vector3(0.7122211f, -0.4963572f, -0.4963574f), new Vector3(0.7746168f, -0.5272596f, -0.3492365f),
                new Vector3(0.814975f, -0.5509226f, -0.1797223f), new Vector3(0.774617f, -0.3492366f, -0.5272593f),
                new Vector3(0.8547508f, -0.3670157f, -0.3670157f), new Vector3(0.9044408f, -0.3828493f, -0.1881839f),
                new Vector3(0.8148447f, -0.1805365f, -0.550849f), new Vector3(0.9043402f, -0.188728f, -0.3828193f),
                new Vector3(0.9606044f, -0.1966567f, -0.1963803f), new Vector3(0.712221f, -0.4963574f, 0.4963574f),
                new Vector3(0.7746168f, -0.3492366f, 0.5272594f), new Vector3(0.8148447f, -0.1805365f, 0.5508491f),
                new Vector3(0.7746167f, -0.5272596f, 0.3492366f), new Vector3(0.8547508f, -0.3670157f, 0.3670158f),
                new Vector3(0.9043402f, -0.188728f, 0.3828193f), new Vector3(0.8148444f, -0.5508494f, 0.1805367f),
                new Vector3(0.9043403f, -0.3828189f, 0.1887279f), new Vector3(0.9605508f, -0.1966497f, 0.1966494f),
                new Vector3(0.7122211f, 0.4963575f, 0.4963571f), new Vector3(0.7746168f, 0.5272595f, 0.3492367f),
                new Vector3(0.8148447f, 0.5508491f, 0.1805367f), new Vector3(0.7746168f, 0.3492366f, 0.5272593f),
                new Vector3(0.8547508f, 0.3670157f, 0.3670157f), new Vector3(0.9043403f, 0.382819f, 0.1887279f),
                new Vector3(0.8148447f, 0.1805362f, 0.5508491f), new Vector3(0.9043404f, 0.1887276f, 0.382819f),
                new Vector3(0.9605509f, 0.1966493f, 0.1966496f), new Vector3(0.7122211f, 0.4963574f, -0.4963571f),
                new Vector3(0.7746168f, 0.3492368f, -0.5272592f), new Vector3(0.8148231f, 0.1806699f, -0.5508373f),
                new Vector3(0.774617f, 0.5272593f, -0.3492366f), new Vector3(0.8547509f, 0.3670157f, -0.3670155f),
                new Vector3(0.9043156f, 0.1888611f, -0.3828118f), new Vector3(0.8148447f, 0.550849f, -0.1805367f),
                new Vector3(0.9043402f, 0.3828191f, -0.188728f), new Vector3(0.9605376f, 0.1967501f, -0.1966136f),
                new Vector3(0.4963578f, -0.4963576f, 0.7122204f), new Vector3(0.3492365f, -0.5272599f, 0.7746166f),
                new Vector3(0.1805361f, -0.5508495f, 0.8148445f), new Vector3(0.5272596f, -0.3492364f, 0.7746168f),
                new Vector3(0.3670158f, -0.3670155f, 0.8547509f), new Vector3(0.1887276f, -0.3828191f, 0.9043403f),
                new Vector3(0.5508492f, -0.1805364f, 0.8148445f), new Vector3(0.3828192f, -0.1887278f, 0.9043403f),
                new Vector3(0.1966494f, -0.1966493f, 0.9605509f), new Vector3(-0.4963576f, -0.4963578f, 0.7122205f),
                new Vector3(-0.5272595f, -0.3492365f, 0.7746168f), new Vector3(-0.5508491f, -0.1805363f, 0.8148447f),
                new Vector3(-0.3492363f, -0.5272599f, 0.7746167f), new Vector3(-0.3670155f, -0.3670155f, 0.8547511f),
                new Vector3(-0.3828189f, -0.1887279f, 0.9043403f), new Vector3(-0.180536f, -0.5508495f, 0.8148445f),
                new Vector3(-0.1887275f, -0.382819f, 0.9043404f), new Vector3(-0.1966492f, -0.1966494f, 0.9605509f),
                new Vector3(-0.4963573f, 0.4963578f, 0.7122207f), new Vector3(-0.3492368f, 0.5272597f, 0.7746166f),
                new Vector3(-0.1805364f, 0.5508491f, 0.8148447f), new Vector3(-0.5272595f, 0.3492369f, 0.7746167f),
                new Vector3(-0.3670157f, 0.3670157f, 0.8547508f), new Vector3(-0.1887276f, 0.3828191f, 0.9043403f),
                new Vector3(-0.5508493f, 0.180536f, 0.8148447f), new Vector3(-0.382819f, 0.1887276f, 0.9043404f),
                new Vector3(-0.1966493f, 0.1966492f, 0.9605509f), new Vector3(0.4963576f, 0.4963576f, 0.7122206f),
                new Vector3(0.5272595f, 0.3492368f, 0.7746167f), new Vector3(0.5508494f, 0.180536f, 0.8148447f),
                new Vector3(0.3492366f, 0.5272596f, 0.7746167f), new Vector3(0.3670155f, 0.3670156f, 0.854751f),
                new Vector3(0.3828188f, 0.1887276f, 0.9043404f), new Vector3(0.1805362f, 0.550849f, 0.8148448f),
                new Vector3(0.1887276f, 0.382819f, 0.9043404f), new Vector3(0.1966493f, 0.1966492f, 0.9605509f),
                new Vector3(-0.9987125f, -0.05072746f, 6.794848E-05f), new Vector3(0f, -0.05072817f, -0.9987125f),
                new Vector3(0.9987125f, -0.05072808f, 6.793537E-05f), new Vector3(0f, -0.0507279f, 0.9987125f),
                new Vector3(-0.7063924f, -0.04494783f, -0.7063919f), new Vector3(-0.7063921f, -0.04494784f, 0.7063922f),
                new Vector3(0.7063926f, -0.04494793f, -0.7063917f), new Vector3(0.7063923f, -0.04494796f, 0.706392f),
                new Vector3(-0.8274336f, -0.04577385f, -0.5596949f),
                new Vector3(-0.9197769f, -0.04768356f, -0.3895341f),
                new Vector3(-0.9783971f, -0.04974205f, -0.2006612f), new Vector3(-0.8274335f, -0.04577384f, 0.5596951f),
                new Vector3(-0.9197769f, -0.04768356f, 0.3895341f), new Vector3(-0.9783834f, -0.0497417f, 0.2007278f),
                new Vector3(0.5596951f, -0.04577413f, -0.8274335f), new Vector3(0.3895339f, -0.04768404f, -0.919777f),
                new Vector3(0.2007278f, -0.04974218f, -0.9783834f), new Vector3(-0.5596951f, -0.04577414f, -0.8274335f),
                new Vector3(-0.3895338f, -0.04768404f, -0.919777f), new Vector3(-0.2007278f, -0.0497422f, -0.9783834f),
                new Vector3(0.8274336f, -0.04577433f, 0.5596948f), new Vector3(0.9197768f, -0.04768436f, 0.3895342f),
                new Vector3(0.9783834f, -0.04974244f, 0.2007279f), new Vector3(0.8274337f, -0.04577435f, -0.5596947f),
                new Vector3(0.9197768f, -0.04768436f, -0.3895342f), new Vector3(0.9783971f, -0.04974281f, -0.2006613f),
                new Vector3(-0.5596952f, -0.0457745f, 0.8274335f), new Vector3(-0.3895338f, -0.04768438f, 0.919777f),
                new Vector3(-0.2007273f, -0.04974207f, 0.9783835f), new Vector3(0.5596952f, -0.04577447f, 0.8274333f),
                new Vector3(0.3895338f, -0.04768437f, 0.9197769f), new Vector3(0.2007274f, -0.04974207f, 0.9783835f),
                new Vector3(0f, -0.9999992f, 0.001340148f), new Vector3(0f, -0.9999992f, 0.001340148f),
                new Vector3(0.7071055f, -0.7071072f, 0.001062795f), new Vector3(0.5595933f, -0.8287665f, 0.001222175f),
                new Vector3(0.3889485f, -0.9212585f, 0.001304361f), new Vector3(0.1996068f, -0.9798751f, 0.001334521f),
                new Vector3(0f, -0.9999992f, 0.001340148f), new Vector3(0f, -0.9999992f, 0.001340148f),
                new Vector3(0f, -0.9999992f, 0.001340148f), new Vector3(0f, 1f, 0f), new Vector3(0f, 1f, 0f),
                new Vector3(0f, 1f, 0f), new Vector3(0f, 1f, 0f), new Vector3(0.7071071f, 0.7071064f, 0f),
                new Vector3(0.5596119f, 0.8287548f, 0f), new Vector3(0.3889765f, 0.9212477f, 0f),
                new Vector3(0.1996274f, 0.9798719f, 0f), new Vector3(0f, 1f, 0f),
                new Vector3(-0.5773504f, -0.57735f, 0.5773504f), new Vector3(-0.62011f, -0.6201099f, 0.4805491f),
                new Vector3(-0.6646993f, -0.6646994f, 0.3411005f), new Vector3(-0.6959519f, -0.6959513f, 0.1769257f),
                new Vector3(-0.7071055f, -0.7071072f, 0.001062799f), new Vector3(-0.62011f, -0.6201099f, -0.4805491f),
                new Vector3(-0.5773505f, -0.5773501f, -0.5773501f), new Vector3(-0.6646993f, -0.6646994f, -0.3411004f),
                new Vector3(-0.696083f, -0.6960853f, -0.1758801f), new Vector3(-0.5773504f, 0.5773503f, -0.5773502f),
                new Vector3(-0.62011f, 0.6201099f, -0.4805489f), new Vector3(-0.6646993f, 0.6646991f, -0.3411008f),
                new Vector3(-0.6959515f, 0.6959518f, -0.1769255f), new Vector3(-0.7071064f, 0.7071071f, 0f),
                new Vector3(-0.62011f, 0.6201099f, 0.4805489f), new Vector3(-0.5773504f, 0.5773503f, 0.5773502f),
                new Vector3(-0.6646993f, 0.6646993f, 0.3411008f), new Vector3(-0.6959514f, 0.6959519f, 0.1769255f),
                new Vector3(-0.5773505f, -0.5773501f, -0.5773501f), new Vector3(-0.6201099f, -0.4805492f, -0.6201098f),
                new Vector3(-0.4805489f, -0.6201102f, -0.6201099f), new Vector3(-0.3411001f, -0.6646996f, -0.6646993f),
                new Vector3(-0.1769258f, -0.6959516f, -0.6959516f), new Vector3(0f, -0.7071066f, -0.7071069f),
                new Vector3(-0.6646992f, -0.3411005f, -0.6646994f), new Vector3(-0.6959518f, -0.1769254f, -0.6959516f),
                new Vector3(-0.7063924f, -0.04494783f, -0.7063919f), new Vector3(0.4805489f, -0.6201099f, -0.6201102f),
                new Vector3(0.5773503f, -0.5773501f, -0.5773506f), new Vector3(0.3411006f, -0.6646994f, -0.6646992f),
                new Vector3(0.1769258f, -0.6959516f, -0.6959516f), new Vector3(0.5773504f, 0.5773502f, -0.5773503f),
                new Vector3(0.480549f, 0.6201099f, -0.62011f), new Vector3(0.3411007f, 0.6646991f, -0.6646996f),
                new Vector3(0.1769254f, 0.6959516f, -0.6959516f), new Vector3(0f, 0.7071069f, -0.7071066f),
                new Vector3(-0.4805488f, 0.6201099f, -0.6201102f), new Vector3(-0.6201098f, 0.4805492f, -0.62011f),
                new Vector3(-0.5773504f, 0.5773503f, -0.5773502f), new Vector3(-0.664699f, 0.3411009f, -0.6646994f),
                new Vector3(-0.6959345f, 0.1770589f, -0.6959348f), new Vector3(-0.7063879f, 0.04508317f, -0.7063878f),
                new Vector3(-0.3411007f, 0.664699f, -0.6646997f), new Vector3(-0.1769257f, 0.6959515f, -0.6959518f),
                new Vector3(0.5773503f, -0.5773501f, -0.5773506f), new Vector3(0.6201101f, -0.4805487f, -0.6201102f),
                new Vector3(0.6201102f, -0.62011f, -0.4805488f), new Vector3(0.6646997f, -0.6646991f, -0.3411001f),
                new Vector3(0.6960831f, -0.696085f, -0.1758805f), new Vector3(0.7071055f, -0.7071072f, 0.001062795f),
                new Vector3(0.6646994f, -0.3411006f, -0.6646991f), new Vector3(0.6959521f, -0.1769258f, -0.695951f),
                new Vector3(0.7063926f, -0.04494793f, -0.7063917f), new Vector3(0.6201102f, -0.6201099f, 0.4805489f),
                new Vector3(0.5773503f, -0.5773502f, 0.5773504f), new Vector3(0.6646997f, -0.6646991f, 0.3411002f),
                new Vector3(0.6959521f, -0.695951f, 0.1769261f), new Vector3(0.7071055f, -0.7071072f, 0.001062795f),
                new Vector3(0.8287613f, -0.5596017f, 0.0008282595f), new Vector3(0.921252f, -0.3889661f, 0.0005541104f),
                new Vector3(0.9797956f, -0.2000014f, 0.0002745824f),
                new Vector3(0.9987125f, -0.05072808f, 6.793537E-05f), new Vector3(0.5773503f, 0.5773502f, 0.5773503f),
                new Vector3(0.6201104f, 0.6201097f, 0.4805486f), new Vector3(0.6647f, 0.6646987f, 0.3411006f),
                new Vector3(0.6959522f, 0.6959511f, 0.1769257f), new Vector3(0.7071071f, 0.7071064f, 0f),
                new Vector3(0.6201104f, 0.6201097f, -0.4805486f), new Vector3(0.6201102f, 0.4805487f, -0.6201099f),
                new Vector3(0.5773504f, 0.5773502f, -0.5773503f), new Vector3(0.6646997f, 0.3411005f, -0.664699f),
                new Vector3(0.6959351f, 0.177059f, -0.6959342f), new Vector3(0.7063882f, 0.04508324f, -0.7063874f),
                new Vector3(0.6647001f, 0.6646987f, -0.3411004f), new Vector3(0.6959522f, 0.6959511f, -0.1769257f),
                new Vector3(0.7071071f, 0.7071064f, 0f), new Vector3(0.8287549f, 0.5596119f, 0f),
                new Vector3(0.9212476f, 0.3889766f, 0f), new Vector3(0.9797882f, 0.2000377f, 3.410387E-05f),
                new Vector3(0.9987108f, 0.05076212f, 3.399035E-05f), new Vector3(0.5773503f, -0.5773502f, 0.5773504f),
                new Vector3(0.6201101f, -0.4805489f, 0.6201099f), new Vector3(0.4805491f, -0.6201099f, 0.6201099f),
                new Vector3(0.3411002f, -0.6646994f, 0.6646994f), new Vector3(0.1769256f, -0.6959513f, 0.695952f),
                new Vector3(0f, -0.7071061f, 0.7071074f), new Vector3(0.6646996f, -0.3411004f, 0.6646991f),
                new Vector3(0.6959518f, -0.1769256f, 0.6959515f), new Vector3(0.7063923f, -0.04494796f, 0.706392f),
                new Vector3(-0.480549f, -0.6201098f, 0.6201102f), new Vector3(-0.6201097f, -0.4805492f, 0.62011f),
                new Vector3(-0.5773504f, -0.57735f, 0.5773504f), new Vector3(-0.6646991f, -0.3411005f, 0.6646997f),
                new Vector3(-0.6959514f, -0.1769252f, 0.6959519f), new Vector3(-0.7063921f, -0.04494784f, 0.7063922f),
                new Vector3(-0.3411001f, -0.6646994f, 0.6646996f), new Vector3(-0.1769256f, -0.6959512f, 0.695952f),
                new Vector3(-0.5773504f, 0.5773503f, 0.5773502f), new Vector3(-0.6201096f, 0.4805492f, 0.62011f),
                new Vector3(-0.4805489f, 0.6201097f, 0.6201102f), new Vector3(-0.3411008f, 0.6646989f, 0.6646997f),
                new Vector3(-0.1769256f, 0.6959513f, 0.6959519f), new Vector3(0f, 0.7071066f, 0.7071069f),
                new Vector3(-0.664699f, 0.3411008f, 0.6646996f), new Vector3(-0.6959513f, 0.176925f, 0.6959522f),
                new Vector3(-0.7063919f, 0.04494741f, 0.7063924f), new Vector3(0.4805491f, 0.6201098f, 0.62011f),
                new Vector3(0.62011f, 0.4805488f, 0.6201099f), new Vector3(0.5773503f, 0.5773502f, 0.5773503f),
                new Vector3(0.6646997f, 0.3411004f, 0.6646991f), new Vector3(0.695952f, 0.1769251f, 0.6959514f),
                new Vector3(0.7063923f, 0.04494748f, 0.706392f), new Vector3(0.3411005f, 0.6646991f, 0.6646996f),
                new Vector3(0.1769253f, 0.6959515f, 0.6959518f),
            };

            return CreateMesh(vertices, indices, normals);
        }

        public static Mesh Cylinder(float radius = 1f, float height = 1f, int resolution = 10)
        {
            Vector3[] vertices = new[]
            {
                new Vector3(-0.4755286f, -1f, -0.1545086f), new Vector3(-0.4045088f, -1f, -0.2938928f),
                new Vector3(-0.2938928f, -1f, -0.4045087f), new Vector3(-0.1545086f, -1f, -0.4755285f),
                new Vector3(0f, -1f, -0.5000002f), new Vector3(0.1545086f, -1f, -0.4755285f),
                new Vector3(0.2938927f, -1f, -0.4045087f), new Vector3(0.4045086f, -1f, -0.2938927f),
                new Vector3(0.4755284f, -1f, -0.1545085f), new Vector3(0.5000001f, -1f, 0f),
                new Vector3(0.4755284f, -1f, 0.1545085f), new Vector3(0.4045086f, -1f, 0.2938927f),
                new Vector3(0.2938927f, -1f, 0.4045086f), new Vector3(0.1545085f, -1f, 0.4755283f),
                new Vector3(1.490116E-08f, -1f, 0.5000001f), new Vector3(-0.1545085f, -1f, 0.4755283f),
                new Vector3(-0.2938926f, -1f, 0.4045085f), new Vector3(-0.4045085f, -1f, 0.2938927f),
                new Vector3(-0.4755283f, -1f, 0.1545085f), new Vector3(-0.5f, -1f, 0f),
                new Vector3(-0.4755286f, 1f, -0.1545086f), new Vector3(-0.4045088f, 1f, -0.2938928f),
                new Vector3(-0.2938928f, 1f, -0.4045087f), new Vector3(-0.1545086f, 1f, -0.4755285f),
                new Vector3(0f, 1f, -0.5000002f), new Vector3(0.1545086f, 1f, -0.4755285f),
                new Vector3(0.2938927f, 1f, -0.4045087f), new Vector3(0.4045086f, 1f, -0.2938927f),
                new Vector3(0.4755284f, 1f, -0.1545085f), new Vector3(0.5000001f, 1f, 0f),
                new Vector3(0.4755284f, 1f, 0.1545085f), new Vector3(0.4045086f, 1f, 0.2938927f),
                new Vector3(0.2938927f, 1f, 0.4045086f), new Vector3(0.1545085f, 1f, 0.4755283f),
                new Vector3(1.490116E-08f, 1f, 0.5000001f), new Vector3(-0.1545085f, 1f, 0.4755283f),
                new Vector3(-0.2938926f, 1f, 0.4045085f), new Vector3(-0.4045085f, 1f, 0.2938927f),
                new Vector3(-0.4755283f, 1f, 0.1545085f), new Vector3(-0.5f, 1f, 0f), new Vector3(0f, -1f, 0f),
                new Vector3(0f, 1f, 0f), new Vector3(0.5000001f, -1f, 0f), new Vector3(0.5000001f, 1f, 0f),
                new Vector3(-0.5f, -1f, 0f), new Vector3(-0.5f, 1f, 0f), new Vector3(-0.4755286f, 1f, -0.1545086f),
                new Vector3(-0.4755286f, -1f, -0.1545086f), new Vector3(-0.4045088f, -1f, -0.2938928f),
                new Vector3(-0.4755286f, -1f, -0.1545086f), new Vector3(-0.2938928f, -1f, -0.4045087f),
                new Vector3(-0.1545086f, -1f, -0.4755285f), new Vector3(0f, -1f, -0.5000002f),
                new Vector3(0.1545086f, -1f, -0.4755285f), new Vector3(0.2938927f, -1f, -0.4045087f),
                new Vector3(0.4045086f, -1f, -0.2938927f), new Vector3(0.4755284f, -1f, -0.1545085f),
                new Vector3(0.5000001f, -1f, 0f), new Vector3(0.4755284f, -1f, 0.1545085f),
                new Vector3(0.4045086f, -1f, 0.2938927f), new Vector3(0.2938927f, -1f, 0.4045086f),
                new Vector3(0.1545085f, -1f, 0.4755283f), new Vector3(1.490116E-08f, -1f, 0.5000001f),
                new Vector3(-0.1545085f, -1f, 0.4755283f), new Vector3(-0.2938926f, -1f, 0.4045085f),
                new Vector3(-0.4045085f, -1f, 0.2938927f), new Vector3(-0.4755283f, -1f, 0.1545085f),
                new Vector3(-0.5f, -1f, 0f), new Vector3(-0.4755286f, 1f, -0.1545086f),
                new Vector3(-0.4045088f, 1f, -0.2938928f), new Vector3(-0.2938928f, 1f, -0.4045087f),
                new Vector3(-0.1545086f, 1f, -0.4755285f), new Vector3(0f, 1f, -0.5000002f),
                new Vector3(0.1545086f, 1f, -0.4755285f), new Vector3(0.2938927f, 1f, -0.4045087f),
                new Vector3(0.4045086f, 1f, -0.2938927f), new Vector3(0.4755284f, 1f, -0.1545085f),
                new Vector3(0.5000001f, 1f, 0f), new Vector3(0.4755284f, 1f, 0.1545085f),
                new Vector3(0.4045086f, 1f, 0.2938927f), new Vector3(0.2938927f, 1f, 0.4045086f),
                new Vector3(0.1545085f, 1f, 0.4755283f), new Vector3(1.490116E-08f, 1f, 0.5000001f),
                new Vector3(-0.1545085f, 1f, 0.4755283f), new Vector3(-0.2938926f, 1f, 0.4045085f),
                new Vector3(-0.4045085f, 1f, 0.2938927f), new Vector3(-0.4755283f, 1f, 0.1545085f),
                new Vector3(-0.5f, 1f, 0f),
            };
            int[] indices = new[]
            {
                0, 20, 21, 0, 21, 1, 1, 21, 22, 1, 22, 2, 2, 22, 23, 2, 23, 3, 3, 23, 24, 3, 24, 4, 4, 24, 25, 4, 25, 5,
                5, 25, 26, 5, 26, 6, 6, 26, 27, 6, 27, 7, 7, 27, 28, 7, 28, 8, 8, 28, 29, 8, 29, 9, 42, 43, 30, 42, 30,
                10, 10, 30, 31, 10, 31, 11, 11, 31, 32, 11, 32, 12, 12, 32, 33, 12, 33, 13, 13, 33, 34, 13, 34, 14, 14,
                34, 35, 14, 35, 15, 15, 35, 36, 15, 36, 16, 16, 36, 37, 16, 37, 17, 17, 37, 38, 17, 38, 18, 18, 38, 39,
                18, 39, 19, 44, 45, 46, 44, 46, 47, 48, 40, 49, 50, 40, 48, 49, 40, 67, 51, 40, 50, 67, 40, 66, 52, 40,
                51, 66, 40, 65, 53, 40, 52, 65, 40, 64, 54, 40, 53, 64, 40, 63, 55, 40, 54, 63, 40, 62, 56, 40, 55, 62,
                40, 61, 57, 40, 56, 61, 40, 60, 58, 40, 57, 60, 40, 59, 59, 40, 58, 68, 41, 69, 87, 41, 68, 69, 41, 70,
                86, 41, 87, 70, 41, 71, 85, 41, 86, 71, 41, 72, 84, 41, 85, 72, 41, 73, 83, 41, 84, 73, 41, 74, 82, 41,
                83, 74, 41, 75, 81, 41, 82, 75, 41, 76, 80, 41, 81, 76, 41, 77, 79, 41, 80, 77, 41, 78, 78, 41, 79,
            };
            Vector3[] normals = new[]
            {
                new Vector3(-0.9510568f, 0f, -0.309016f), new Vector3(-0.809017f, 0f, -0.5877854f),
                new Vector3(-0.5877851f, 0f, -0.8090171f), new Vector3(-0.3090168f, 0f, -0.9510565f),
                new Vector3(0f, 0f, -1f), new Vector3(0.3090171f, 0f, -0.9510565f),
                new Vector3(0.5877854f, 0f, -0.8090168f), new Vector3(0.8090171f, 0f, -0.5877851f),
                new Vector3(0.9510565f, 0f, -0.3090169f), new Vector3(1f, 0f, 0f),
                new Vector3(0.9510565f, 0f, 0.309017f), new Vector3(0.8090169f, 0f, 0.5877854f),
                new Vector3(0.5877852f, 0f, 0.8090171f), new Vector3(0.3090169f, 0f, 0.9510565f),
                new Vector3(0f, 0f, 1f), new Vector3(-0.3090171f, 0f, 0.9510565f),
                new Vector3(-0.5877852f, 0f, 0.809017f), new Vector3(-0.809017f, 0f, 0.5877852f),
                new Vector3(-0.9510565f, 0f, 0.3090169f), new Vector3(-1f, 0f, 0f),
                new Vector3(-0.9510568f, 0f, -0.309016f), new Vector3(-0.809017f, 0f, -0.5877854f),
                new Vector3(-0.5877851f, 0f, -0.8090171f), new Vector3(-0.3090168f, 0f, -0.9510565f),
                new Vector3(0f, 0f, -1f), new Vector3(0.3090171f, 0f, -0.9510565f),
                new Vector3(0.5877854f, 0f, -0.8090168f), new Vector3(0.8090171f, 0f, -0.5877851f),
                new Vector3(0.9510565f, 0f, -0.3090169f), new Vector3(1f, 0f, 0f),
                new Vector3(0.9510565f, 0f, 0.309017f), new Vector3(0.8090169f, 0f, 0.5877854f),
                new Vector3(0.5877852f, 0f, 0.8090171f), new Vector3(0.3090169f, 0f, 0.9510565f),
                new Vector3(0f, 0f, 1f), new Vector3(-0.3090171f, 0f, 0.9510565f),
                new Vector3(-0.5877852f, 0f, 0.809017f), new Vector3(-0.809017f, 0f, 0.5877852f),
                new Vector3(-0.9510565f, 0f, 0.3090169f), new Vector3(-1f, 0f, 0f), new Vector3(0f, -1f, 0f),
                new Vector3(0f, 1f, 0f), new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 0f), new Vector3(-1f, 0f, 0f),
                new Vector3(-1f, 0f, 0f), new Vector3(-0.9510568f, 0f, -0.309016f),
                new Vector3(-0.9510568f, 0f, -0.309016f), new Vector3(0f, -1f, 0f), new Vector3(0f, -1f, 0f),
                new Vector3(0f, -1f, 0f), new Vector3(0f, -1f, 0f), new Vector3(0f, -1f, 0f), new Vector3(0f, -1f, 0f),
                new Vector3(0f, -1f, 0f), new Vector3(0f, -1f, 0f), new Vector3(0f, -1f, 0f), new Vector3(0f, -1f, 0f),
                new Vector3(0f, -1f, 0f), new Vector3(0f, -1f, 0f), new Vector3(0f, -1f, 0f), new Vector3(0f, -1f, 0f),
                new Vector3(0f, -1f, 0f), new Vector3(0f, -1f, 0f), new Vector3(0f, -1f, 0f), new Vector3(0f, -1f, 0f),
                new Vector3(0f, -1f, 0f), new Vector3(0f, -1f, 0f), new Vector3(0f, 1f, 0f), new Vector3(0f, 1f, 0f),
                new Vector3(0f, 1f, 0f), new Vector3(0f, 1f, 0f), new Vector3(0f, 1f, 0f), new Vector3(0f, 1f, 0f),
                new Vector3(0f, 1f, 0f), new Vector3(0f, 1f, 0f), new Vector3(0f, 1f, 0f), new Vector3(0f, 1f, 0f),
                new Vector3(0f, 1f, 0f), new Vector3(0f, 1f, 0f), new Vector3(0f, 1f, 0f), new Vector3(0f, 1f, 0f),
                new Vector3(0f, 1f, 0f), new Vector3(0f, 1f, 0f), new Vector3(0f, 1f, 0f), new Vector3(0f, 1f, 0f),
                new Vector3(0f, 1f, 0f), new Vector3(0f, 1f, 0f),
            };

            return CreateMesh(vertices, indices, normals);
        }

        public static Mesh Cone(float radius = 1f, float height = 1f, int resolution = 10)
        {
            Vector3[] vertices = new Vector3[resolution + 2];
            int[] indices = new int[vertices.Length * 3 + resolution * 3];

            int vertIndex = 0;
            int indexIndex = 0;

            // Bottom Circle
            PopulateCircle(radius, resolution, 0f, false, ref vertIndex, ref indexIndex, ref vertices, ref indices);

            vertices[vertIndex] = new Vector3(0f, height, 0f);

            int theta = 1;
            for (int i = 0; i < resolution - 1; i++)
            {
                indices[indexIndex++] = vertIndex;
                indices[indexIndex++] = theta + 1;
                indices[indexIndex++] = theta;

                theta++;
            }

            indices[indexIndex++] = vertIndex;
            indices[indexIndex++] = 1;
            indices[indexIndex++] = theta;

            return CreateMesh(vertices, indices);
        }

        static void PopulateCircle(float radius, int resolution, float yOffset, bool flip, ref int vertexIndex,
            ref int indexIndex, ref Vector3[] vertices, ref int[] indices)
        {
            float step = (Mathf.PI * 2f) / resolution;
            float theta = 0f;

            vertices[vertexIndex++] = new Vector3(0f, yOffset, 0f);
            int firstVertexIndex = vertexIndex;

            for (int i = 0; i < resolution; i++)
            {
                Vector3 pos = new Vector3(radius * Mathf.Cos(theta), yOffset, radius * Mathf.Sin(theta));

                vertices[vertexIndex++] = pos;
                theta += step;

                if (i < resolution - 1)
                {
                    indices[indexIndex++] = firstVertexIndex - 1;
                    if (flip)
                    {
                        indices[indexIndex++] = vertexIndex;
                        indices[indexIndex++] = vertexIndex - 1;
                    }
                    else
                    {
                        indices[indexIndex++] = vertexIndex - 1;
                        indices[indexIndex++] = vertexIndex;
                    }
                }
                else
                {
                    indices[indexIndex++] = firstVertexIndex - 1;
                    if (flip)
                    {
                        indices[indexIndex++] = firstVertexIndex;
                        indices[indexIndex++] = vertexIndex - 1;
                    }
                    else
                    {
                        indices[indexIndex++] = vertexIndex - 1;
                        indices[indexIndex++] = firstVertexIndex;
                    }
                }
            }
        }

        static readonly int[] vertexPairs = { 0, 1, 0, 2, 0, 3, 0, 4, 1, 2, 2, 3, 3, 4, 4, 1, 5, 1, 5, 2, 5, 3, 5, 4 };

        static readonly int[] edgeTriplets =
            {0, 1, 4, 1, 2, 5, 2, 3, 6, 3, 0, 7, 8, 9, 4, 9, 10, 5, 10, 11, 6, 11, 8, 7};

        public static Mesh Icosahedron(float radius = 1f)
        {
            return Sphere(radius, 1);
        }

        /// <summary>
        /// From: https://github.com/SebLague/Solar-System/blob/Episode_02/Assets/Celestial%20Body/Scripts/SphereMesh.cs
        /// </summary>
        public static Mesh Sphere(float radius = 1f, int resolution = 10)
        {
            Vector3[] baseVertices =
            {
                Vector3.up * radius, Vector3.left * radius, Vector3.back * radius, Vector3.right * radius,
                Vector3.forward * radius, Vector3.down * radius
            };

            int numDivisions = Mathf.Max(0, resolution);
            int numVertsPerFace = ((numDivisions + 3) * (numDivisions + 3) - (numDivisions + 3)) / 2;
            int numVerts = numVertsPerFace * 8 - (numDivisions + 2) * 12 + 6;
            int numTrisPerFace = (numDivisions + 1) * (numDivisions + 1);

            List<Vector3> vertices = new List<Vector3>();
            List<int> indices = new List<int>();

            vertices.AddRange(baseVertices);

            Edge[] edges = new Edge[12];
            for (int i = 0; i < vertexPairs.Length; i += 2)
            {
                Vector3 startVertex = vertices[vertexPairs[i]];
                Vector3 endVertex = vertices[vertexPairs[i + 1]];

                int[] edgeVertexIndices = new int[numDivisions + 2];
                edgeVertexIndices[0] = vertexPairs[i];

                // Add vertices along edge
                for (int divisionIndex = 0; divisionIndex < numDivisions; divisionIndex++)
                {
                    float t = (divisionIndex + 1f) / (numDivisions + 1f);
                    edgeVertexIndices[divisionIndex + 1] = vertices.Count;
                    vertices.Add(Vector3.Slerp(startVertex, endVertex, t));
                }

                edgeVertexIndices[numDivisions + 1] = vertexPairs[i + 1];
                int edgeIndex = i / 2;
                edges[edgeIndex] = new Edge(edgeVertexIndices);
            }

            // Create faces
            for (int i = 0; i < edgeTriplets.Length; i += 3)
            {
                int faceIndex = i / 3;
                bool reverse = faceIndex >= 4;
                CreateFace(edges[edgeTriplets[i]], edges[edgeTriplets[i + 1]], edges[edgeTriplets[i + 2]], reverse,
                    vertices, numDivisions, ref indices);
            }

            return CreateMesh(vertices, indices);
        }

        /// <summary>
        /// From: https://github.com/SebLague/Solar-System/blob/Episode_02/Assets/Celestial%20Body/Scripts/SphereMesh.cs
        /// </summary>
        static void CreateFace(Edge sideA, Edge sideB, Edge bottom, bool reverse, List<Vector3> vertices,
            int numDivisions, ref List<int> indices)
        {
            int numPointsInEdge = sideA.vertexIndices.Length;
            var vertexMap = new List<int>();
            vertexMap.Add(sideA.vertexIndices[0]); // top of triangle

            for (int i = 1; i < numPointsInEdge - 1; i++)
            {
                // Side A vertex
                vertexMap.Add(sideA.vertexIndices[i]);

                // Add vertices between sideA and sideB
                Vector3 sideAVertex = vertices[sideA.vertexIndices[i]];
                Vector3 sideBVertex = vertices[sideB.vertexIndices[i]];
                int numInnerPoints = i - 1;
                for (int j = 0; j < numInnerPoints; j++)
                {
                    float t = (j + 1f) / (numInnerPoints + 1f);
                    vertexMap.Add(vertices.Count);
                    vertices.Add(Vector3.Slerp(sideAVertex, sideBVertex, t));
                }

                // Side B vertex
                vertexMap.Add(sideB.vertexIndices[i]);
            }

            // Add bottom edge vertices
            for (int i = 0; i < numPointsInEdge; i++)
            {
                vertexMap.Add(bottom.vertexIndices[i]);
            }

            // Triangulate
            int numRows = numDivisions + 1;
            for (int row = 0; row < numRows; row++)
            {
                // vertices down left edge follow quadratic sequence: 0, 1, 3, 6, 10, 15...
                // the nth term can be calculated with: (n^2 - n)/2
                int topVertex = ((row + 1) * (row + 1) - row - 1) / 2;
                int bottomVertex = ((row + 2) * (row + 2) - row - 2) / 2;

                int numTrianglesInRow = 1 + 2 * row;
                for (int column = 0; column < numTrianglesInRow; column++)
                {
                    int v0, v1, v2;

                    if (column % 2 == 0)
                    {
                        v0 = topVertex;
                        v1 = bottomVertex + 1;
                        v2 = bottomVertex;
                        topVertex++;
                        bottomVertex++;
                    }
                    else
                    {
                        v0 = topVertex;
                        v1 = bottomVertex;
                        v2 = topVertex - 1;
                    }

                    indices.Add(vertexMap[v0]);
                    indices.Add(vertexMap[(reverse) ? v2 : v1]);
                    indices.Add(vertexMap[(reverse) ? v1 : v2]);
                }
            }
        }

        static Mesh CreateMesh(Vector3[] vertices, int[] indices)
        {
            var mesh = new Mesh();

            mesh.SetVertices(vertices);
            mesh.SetIndices(indices, MeshTopology.Triangles, 0);

            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mesh.Optimize();

            return mesh;
        }

        static Mesh CreateMesh(Vector3[] vertices, int[] indices, Vector3[] normals, Vector2[] uvs)
        {
            var mesh = new Mesh();

            mesh.SetVertices(vertices);
            mesh.SetIndices(indices, MeshTopology.Triangles, 0);
            mesh.SetNormals(normals);
            mesh.SetUVs(0, uvs);

            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mesh.Optimize();

            return mesh;
        }

        static Mesh CreateMesh(Vector3[] vertices, int[] indices, Vector3[] normals)
        {
            var mesh = new Mesh();

            mesh.SetVertices(vertices);
            mesh.SetIndices(indices, MeshTopology.Triangles, 0);
            mesh.SetNormals(normals);

            mesh.RecalculateBounds();
            mesh.Optimize();

            return mesh;
        }

        static Mesh CreateMesh(List<Vector3> vertices, List<int> indices)
        {
            var mesh = new Mesh();

            mesh.SetVertices(vertices);
            mesh.SetIndices(indices, MeshTopology.Triangles, 0);

            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mesh.Optimize();

            return mesh;
        }

        struct Triangle
        {
            public int v0;
            public int v1;
            public int v2;

            public bool Contains(int v)
            {
                return v0 == v || v1 == v || v2 == v;
            }

            public Vector3 AverageNormal(int origin, Vector3[] normals)
            {
                Vector3 avgNormal = Vector3.zero;
                if (origin != v0) avgNormal += normals[v0];
                if (origin != v1) avgNormal += normals[v1];
                if (origin != v2) avgNormal += normals[v2];
                return avgNormal;
            }
        }

        static Mesh SmoothNormals(Mesh mesh)
        {
            var normals = mesh.normals;
            var newNormals = new Vector3[normals.Length];
            var verts = mesh.vertices;
            var tris = mesh.triangles;

            var triangleLookup = new List<Triangle>();

            for (int i = 0; i < tris.Length; i += 3)
            {
                triangleLookup.Add(
                    new Triangle
                    {
                        v0 = tris[i],
                        v1 = tris[i + 1],
                        v2 = tris[i + 2]
                    }
                );
            }

            for (int i = 0; i < verts.Length; i++)
            {
                Vector3 avgNormal = Vector3.zero;
                int totalFacets = 0;
                foreach (var facet in triangleLookup.Where(e => e.Contains(i)))
                {
                    avgNormal += facet.AverageNormal(i, normals);
                    totalFacets += 2;
                }

                avgNormal /= totalFacets;
                newNormals[i] = avgNormal.normalized;
            }

            mesh.SetNormals(newNormals);
            return mesh;
        }

        class Edge
        {
            public int[] vertexIndices;

            public Edge(int[] vertexIndices)
            {
                this.vertexIndices = vertexIndices;
            }
        }
    }
}