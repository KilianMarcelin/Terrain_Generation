using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class MeshGenerator : MonoBehaviour
{
    public int vertexCountX = 10;
    public int vertexCountZ = 10;
    public float heightScale = 1.0f;
    public bool island = true;
    public bool terrace = false;
    public int terraces = 5;
    
    private Mesh mesh;
    
    public Material material;

    public void GenerateMesh(float[,] heights)
    {
        mesh = new Mesh();
        mesh.indexFormat = IndexFormat.UInt32;   
        GetComponent<MeshFilter>().mesh = mesh;

        Vector3[] vertices = new Vector3[vertexCountX * vertexCountZ];
        int[] triangles = new int[(vertexCountX - 1) * (vertexCountZ - 1) * 6];
        Vector2[] uvs = new Vector2[vertexCountX * vertexCountZ];

        for (int x = 0; x < vertexCountX; x++)
        {
            for (int z = 0; z < vertexCountZ; z++)
            {
                float xPos = x / (float)(vertexCountX - 1);
                float zPos = z / (float)(vertexCountZ - 1);
                int index = z * vertexCountX + x;
                float elevation = heights[z, x];
                if (island)
                {
                    float nx = 2 * (x / (float)vertexCountX) - 1;
                    float nz = 2 * z / (float)vertexCountZ - 1;
                    double eucDist = Math.Min(1, (nx*nx + nz*nz) / Math.Sqrt(2));
                    elevation = (float)(elevation + (1 - eucDist)) / 2;
                }

                if (terrace)
                {
                    elevation = (float)Math.Round(elevation * terraces) / terraces;
                }
                vertices[index] = new Vector3(xPos, elevation*heightScale, zPos);
                uvs[index] = new Vector2(xPos, zPos);
            }
        }

        int triangleIndex = 0;
        for (int x = 0; x < vertexCountX - 1; x++)
        {
            for (int z = 0; z < vertexCountZ - 1; z++)
            {
                int a = z * vertexCountX + x;
                int b = a + 1;
                int c = (z + 1) * vertexCountX + x;
                int d = c + 1;

                triangles[triangleIndex++] = a;
                triangles[triangleIndex++] = c;
                triangles[triangleIndex++] = b;

                triangles[triangleIndex++] = b;
                triangles[triangleIndex++] = c;
                triangles[triangleIndex++] = d;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        transform.localScale = new Vector3(vertexCountX/2, 1, vertexCountZ/2);

        // Vous pouvez maintenant utiliser le tableau heights pour accéder aux valeurs de hauteur associées à chaque vertex.
        
        if (material != null)
        {
            GetComponent<MeshRenderer>().material = material;
        }
    }
}
