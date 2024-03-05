using System.Collections;
using System.Collections.Generic;
using PathCreation;
using PathCreation.Examples;
using UnityEngine;

public class RiverGenerator : PathSceneTool
{
    [Min(1)]
    public int resolution = 1;
    public float _Width = .4f;
    public bool _FlattenSurface = true;
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
        Vector3[] verts = new Vector3[pathPointCount * 2];
        Vector2[] uvs = new Vector2[verts.Length];
        Vector3[] normals = new Vector3[verts.Length];
        int numTris = 2 * (pathPointCount - 1) + ((_Path.isClosedLoop) ? 2 : 0);
        int[] triangles = new int[numTris * 3];

        bool usePathNormals = !(_Path.space == PathSpace.xyz && _FlattenSurface);
        int pathPointIndex = 0;
        for (int i = 0; i < pathPointCount; i++) 
        {
            Vector3 localUp = (usePathNormals) ? Vector3.Cross(_Path.GetTangent(pathPointIndex), _Path.GetNormal(pathPointIndex)) : _Path.up;
            Vector3 localRight = (usePathNormals) ? _Path.GetNormal(pathPointIndex) : Vector3.Cross(localUp, _Path.GetTangent(pathPointIndex));
            verts[i] = _Path.localPoints[pathPointIndex] - localRight * Mathf.Abs(_Width);
            verts[i + pathPointCount] = _Path.localPoints[pathPointIndex] + localRight * Mathf.Abs(_Width);
            uvs[i] = new Vector2(0, _Path.times[pathPointIndex]);
            uvs[i + pathPointCount] = new Vector2(1, _Path.times[pathPointIndex]);
            normals[i] = localUp;
            normals[i + pathPointCount] = localUp;

            pathPointIndex += resolution;
            if (pathPointIndex > _Path.NumPoints) 
            {
                break;
            }
        }

        int index = 0;
        for (int i = 0; i < pathPointCount - 1; i++)
        {
            triangles[index] = i;
            triangles[index + 1] = i + 1;
            triangles[index + 2] = i + pathPointCount;

            triangles[index + 3] = i + pathPointCount + 1;
            triangles[index + 4] = i + pathPointCount;
            triangles[index + 5] = i + 1;

            index += 6;
        }

        if (_Path.isClosedLoop)
        {
            triangles[index] = 2 * pathPointCount - 1;
            triangles[index + 1] = pathPointCount - 1;
            triangles[index + 2] = 0;

            triangles[index + 3] = 0;
            triangles[index + 4] = pathPointCount;
            triangles[index + 5] = 2 * pathPointCount - 1;

            index += 6;
        }       

        mesh.Clear();
        mesh.vertices = verts;
        mesh.uv = uvs;
        mesh.normals = normals;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
    }
}
