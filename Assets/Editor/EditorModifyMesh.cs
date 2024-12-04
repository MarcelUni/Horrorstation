using UnityEngine;
using UnityEditor;
using System.Linq;

public class EditorModifyMesh : EditorWindow
{
    [MenuItem("Tools/Mesh/FlipNormals")]
    static void FlipNormals()
    {
        Mesh oldMesh = Selection.activeObject as Mesh;
        if (oldMesh == null)
        {
            Debug.LogError("No mesh selected!");
            return;
        }

        // Create a new mesh to store the modified normals
        Mesh newMesh = new Mesh();
        newMesh.vertices = oldMesh.vertices;
        newMesh.triangles = oldMesh.triangles.Reverse().ToArray();
        newMesh.uv = oldMesh.uv;
        newMesh.normals = oldMesh.normals;
        newMesh.tangents = oldMesh.tangents;
        newMesh.colors = oldMesh.colors;
        newMesh.boneWeights = oldMesh.boneWeights;
        newMesh.bindposes = oldMesh.bindposes;
        newMesh.subMeshCount = oldMesh.subMeshCount;
        for (int i = 0; i < oldMesh.subMeshCount; i++)
        {
            newMesh.SetTriangles(oldMesh.GetTriangles(i).Reverse().ToArray(), i);
        }
        newMesh.RecalculateNormals();

        // Save the new mesh as an asset
        string path = AssetDatabase.GetAssetPath(oldMesh);
        string newPath = System.IO.Path.GetDirectoryName(path) + "/" + System.IO.Path.GetFileNameWithoutExtension(path) + "_Flipped.asset";
        AssetDatabase.CreateAsset(newMesh, newPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Flipped normals and saved new mesh at: " + newPath);
    }

    [MenuItem("Tools/Mesh/FlipNormals", true)]
    static bool DoSomethingValidation() => Selection.activeObject is Mesh;
}
