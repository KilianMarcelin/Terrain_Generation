using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;
    public int octaves;
    public float persistance;
    public float lacunarity;
    public bool autoUpdate;
    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale, octaves,persistance,lacunarity);

        MapDisplay mapDisplay = FindObjectOfType<MapDisplay>();
        MeshGenerator meshGenerator = FindObjectOfType<MeshGenerator>();
        WaterGenerator waterGenerator = FindObjectOfType<WaterGenerator>();
        mapDisplay.DrawNoiseMap(noiseMap); 
        meshGenerator.vertexCountX = mapWidth;
        meshGenerator.vertexCountZ = mapHeight;
        waterGenerator.vertexCountX = mapWidth;
        waterGenerator.vertexCountZ = mapHeight;
        meshGenerator.GenerateMesh(noiseMap);
        waterGenerator.GenerateMesh();
        waterGenerator.transform.localPosition = new Vector3(waterGenerator.transform.localPosition.x,
            (meshGenerator.heightScale * mapDisplay.terrainLevels[1]),
            waterGenerator.transform.localPosition.z);
    }
}
