using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float noiseScale)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        if (noiseScale <= 0) noiseScale = 0.0001f; // Prevent any division by 0
        
        for (int i = 0; i < mapHeight; i++) {
            for (int j = 0; j < mapWidth; j++)
            {
                float sampleI = i / noiseScale;
                float sampleJ = j / noiseScale;
                float perlinValue = Mathf.PerlinNoise(sampleI, sampleJ);
                noiseMap[i, j] = perlinValue;
            }
        }

        return noiseMap;
    }
}
