#if REGIZMO_DEV
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class PrimitivesHack
{
    [MenuItem("Window/ReGizmo/Dev/Create Primitive Data")]
    public static void Generate()
    {
        var cubeGO = GameObject.CreatePrimitive(PrimitiveType.Capsule).GetComponent<MeshFilter>();

        Mesh mesh = cubeGO.sharedMesh;

        string verts = "";
        foreach (var vert in mesh.vertices)
        {
            verts += $"new Vector3({vert.x}f, {vert.y}f, {vert.z}f)" + ", ";
        }

        string indices = "";
        foreach (var ind in mesh.GetIndices(0))
        {
            indices += ind + ", ";
        }

        string normals = "";
        foreach (var norm in mesh.normals)
        {
            normals += $"new Vector3({norm.x}f, {norm.y}f, {norm.z}f)" + ", ";
        }

        string contents = verts + "\n\n" + indices + "\n\n" + normals;

        System.IO.File.WriteAllText(Application.dataPath + "/primitiveData.txt", contents);
        AssetDatabase.Refresh();

        GameObject.DestroyImmediate(cubeGO.gameObject);
    }


}
#endif