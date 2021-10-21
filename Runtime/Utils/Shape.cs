using System.Collections.Generic;
using UnityEngine;

namespace ReGizmo.Utils
{
    internal class Shape
    {
        List<Vector3> vertices;
        List<Vector3> normals;
        List<int> indices;
        List<Vector2> uvs;

        List<Triangle> triangles;

        public Shape()
        {
            vertices = new List<Vector3>();
            normals = new List<Vector3>();
            indices = new List<int>();
            uvs = new List<Vector2>();

            triangles = new List<Triangle>();
        }

        public Shape AddTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            int index1 = AddVertex(p1);
            int index2 = AddVertex(p2);
            int index3 = AddVertex(p3);
            
            indices.Add(index1);
            indices.Add(index2);
            indices.Add(index3);
            
            triangles.Add(new Triangle(index1, index2, index3));

            return this;
        }

        public int AddVertex(Vector3 vertex)
        {
            int index = vertices.IndexOf(vertex);

            if (index == -1)
            {
                vertices.Add(vertex);
                index = vertices.Count - 1;
            }

            return index;
        }

        public Shape AddUv(Vector2 uv)
        {
            uvs.Add(uv);
            return this;
        }

        public Shape Build()
        {
            foreach (var tri in triangles)
            {
                Vector3 normal = tri.GetNormal(vertices);
                
                normals.Add(normal);
                normals.Add(normal);
                normals.Add(normal);
            }

            return this;
        }

        public Mesh ToMesh(string name = "default")
        {
            var mesh = new Mesh();
            mesh.name = name;

            mesh.SetVertices(vertices);
            mesh.SetIndices(indices, MeshTopology.Triangles, 0);
            mesh.SetNormals(normals);
            mesh.SetUVs(0, uvs);
            
            //mesh.RecalculateNormals();
            //mesh.RecalculateBounds();
            //mesh.RecalculateTangents();
            
            mesh.Optimize();

            return mesh;
        }
    }

    internal class Triangle
    {
        public int Index0;
        public int Index1;
        public int Index2;

        public Triangle(int index0, int index1, int index2)
        {
            Index0 = index0;
            Index1 = index1;
            Index2 = index2;
        }

        public Vector3 GetNormal(List<Vector3> vertices)
        {
            Vector3 edge1 = vertices[Index1] - vertices[Index0];
            Vector3 edge2 = vertices[Index2] - vertices[Index1];

            return Vector3.Cross(edge1, edge2).normalized;
        }
        
        public bool Contains(int index)
        {
            return Index0 == index || Index1 == index || Index2 == index;
        }
    }
}