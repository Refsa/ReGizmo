using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReGizmo.Utility
{
    internal static class ReGizmoPrimitives
    {
        static readonly Vector3 DefaultSize = Vector3.one;

        public static Mesh Quad()
        {
            return Quad(Vector2.one);
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
                new[] {
                    0, 1, 2, 0, 2, 3
                };

            Vector3[] normals =
                new[] {
                    Vector3.forward, Vector3.forward, Vector3.forward, Vector3.forward
                };

            return CreateMesh(vertices, indices);
        }

        public static Mesh Cube()
        {
            return Cube(DefaultSize);
        }

        public static Mesh Cube(Vector3 size)
        {
            var mf = GameObject.CreatePrimitive(PrimitiveType.Cube).GetComponent<MeshFilter>();
            var mesh = CreateMesh(mf.sharedMesh.vertices, mf.sharedMesh.GetIndices(0), mf.sharedMesh.normals);
#if UNITY_EDITOR
            GameObject.DestroyImmediate(mf.gameObject);
#else
            GameObject.Destroy(mf.gameObject);
#endif

            return mesh;

            /* if (size == Vector3.zero) size = DefaultSize;

            Vector3 halfSize = size / 2f;

            Vector3 bA = new Vector3(0f, -halfSize.y, 0f);
            Vector3 bB = new Vector3(size.x, -halfSize.y, 0f);
            Vector3 bC = new Vector3(size.x, -halfSize.y, size.z);
            Vector3 bD = new Vector3(0f, -halfSize.y, size.z);

            Vector3 tA = new Vector3(0f, halfSize.y, 0f);
            Vector3 tB = new Vector3(size.x, halfSize.y, 0f);
            Vector3 tC = new Vector3(size.x, halfSize.y, size.z);
            Vector3 tD = new Vector3(0f, halfSize.y, size.z);

            halfSize.y = 0f;

            Vector3[] vertices = new Vector3[8]
            {
                bA - halfSize, bB - halfSize, bC - halfSize, bD - halfSize,
                tA - halfSize, tB - halfSize, tC - halfSize, tD - halfSize
            };

            int[] indices = new int[36]
            {
                0, 1, 2, 0, 2, 3,
                6, 5, 4, 6, 4, 7,
                0, 4, 5, 0, 5, 1,
                1, 5, 6, 1, 6, 2,
                0, 7, 4, 0, 3, 7,
                3, 2, 6, 3, 6, 7
            };

            return CreateMesh(vertices, indices); */
            // return SmoothNormals(CreateMesh(vertices, indices));
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
            var mf = GameObject.CreatePrimitive(PrimitiveType.Capsule).GetComponent<MeshFilter>();
            var mesh = CreateMesh(mf.sharedMesh.vertices, mf.sharedMesh.GetIndices(0), mf.sharedMesh.normals);

            GameObject.DestroyImmediate(mf.gameObject);

            return mesh;
        }

        public static Mesh Cylinder(float radius = 1f, float height = 1f, int resolution = 10)
        {
            float halfHeight = height / 2f;

            Vector3[] vertices = new Vector3[resolution * 2 + 2];
            int[] indices = new int[vertices.Length * 3 + resolution * 6];

            int vertIndex = 0;
            int indexIndex = 0;

            // Bottom Circle
            int firstCircleIndex = 1;
            PopulateCircle(radius, resolution, -halfHeight, false, ref vertIndex, ref indexIndex, ref vertices, ref indices);
            // Top Circle
            int secondCircleIndex = vertIndex + 1;
            PopulateCircle(radius, resolution, halfHeight, true, ref vertIndex, ref indexIndex, ref vertices, ref indices);

            // Sides
            int theta1 = firstCircleIndex;
            int theta2 = secondCircleIndex;
            for (int i = 0; i < resolution - 1; i++)
            {
                indices[indexIndex++] = theta1;
                indices[indexIndex++] = theta2;
                indices[indexIndex++] = theta2 + 1;

                indices[indexIndex++] = theta1;
                indices[indexIndex++] = theta2 + 1;
                indices[indexIndex++] = theta1 + 1;

                theta1++;
                theta2++;
            }

            indices[indexIndex++] = theta1;
            indices[indexIndex++] = theta2;
            indices[indexIndex++] = secondCircleIndex;

            indices[indexIndex++] = theta1;
            indices[indexIndex++] = secondCircleIndex;
            indices[indexIndex++] = firstCircleIndex;

            return CreateMesh(vertices, indices);
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

        static void PopulateCircle(float radius, int resolution, float yOffset, bool flip, ref int vertexIndex, ref int indexIndex, ref Vector3[] vertices, ref int[] indices)
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
        static readonly int[] edgeTriplets = { 0, 1, 4, 1, 2, 5, 2, 3, 6, 3, 0, 7, 8, 9, 4, 9, 10, 5, 10, 11, 6, 11, 8, 7 };

        public static Mesh Icosahedron(float radius = 1f)
        {
            return Sphere(radius, 1);
        }

        /// <summary>
        /// From: https://github.com/SebLague/Solar-System/blob/Episode_02/Assets/Celestial%20Body/Scripts/SphereMesh.cs
        /// </summary>
        public static Mesh Sphere(float radius = 1f, int resolution = 10)
        {
            Vector3[] baseVertices = { Vector3.up * radius, Vector3.left * radius, Vector3.back * radius, Vector3.right * radius, Vector3.forward * radius, Vector3.down * radius };

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
                CreateFace(edges[edgeTriplets[i]], edges[edgeTriplets[i + 1]], edges[edgeTriplets[i + 2]], reverse, vertices, numDivisions, ref indices);
            }

            return CreateMesh(vertices, indices);
        }

        /// <summary>
        /// From: https://github.com/SebLague/Solar-System/blob/Episode_02/Assets/Celestial%20Body/Scripts/SphereMesh.cs
        /// </summary>
        static void CreateFace(Edge sideA, Edge sideB, Edge bottom, bool reverse, List<Vector3> vertices, int numDivisions, ref List<int> indices)
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
                    new Triangle {
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