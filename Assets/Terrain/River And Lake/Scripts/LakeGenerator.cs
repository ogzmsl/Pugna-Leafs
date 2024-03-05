using PathCreation;
using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeGenerator : PathSceneTool
{
    [Min(1)]
    public int resolution = 1;
    public Material material;

    protected VertexPath _Path
    {
        get
        {
            return pathCreator.path;
        }
    }

    protected MeshRenderer meshRenderer;
    protected MeshFilter meshFilter;
    protected Mesh mesh;

    protected override void PathUpdated()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = material;
        meshFilter = GetComponent<MeshFilter>();
        mesh = new Mesh();
        meshFilter.mesh = mesh;

        if (pathCreator == null || _Path == null)
        {
            return;
        }

        int pathPointCount = _Path.NumPoints / resolution;
        int vertexCount = pathPointCount + 2;
        Vector3[] verts = new Vector3[vertexCount];
        Vector2[] uvs = new Vector2[vertexCount];
        int[] triangles = new int[pathPointCount * 3];

        verts[0] = Vector3.zero;
        uvs[0] = Vector2.one * 0.5f;

        int pathPointIndex = 0;
        for (int i = 1; i < vertexCount - 1; i++)
        {
            verts[i] = _Path.localPoints[pathPointIndex];
            float uv_X = (verts[i].x - _Path.bounds.min.x) / _Path.bounds.size.x;
            float uv_y = (verts[i].z - _Path.bounds.min.z) / _Path.bounds.size.z;
            uvs[i] = new Vector2(uv_X, uv_y);
            pathPointIndex += resolution;
            if (pathPointIndex > _Path.NumPoints)
            {
                break;
            }
        }

        verts[vertexCount - 1] = _Path.localPoints[0];
        uvs[vertexCount - 1] = new Vector2(
            (verts[vertexCount - 1].x - _Path.bounds.min.x) / _Path.bounds.size.x, 
            (verts[vertexCount - 1].z - _Path.bounds.min.z) / _Path.bounds.size.z);

        int triIndex = 0;
        for (int i = 0; i < pathPointCount; i++)
        {
            triangles[triIndex] = 0;
            triangles[triIndex + 1] = i + 2;
            triangles[triIndex + 2] = i + 1;
            
            triIndex += 3;
        }

        mesh.Clear();
        mesh.vertices = verts;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}