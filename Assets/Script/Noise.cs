using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float noiseScale, int octaves, float persistance, float lacunarity)
    {
        float[,] noiseMap = new float[mapHeight, mapWidth];
        
        // Used later for normalization
        float minNoiseHeight = float.MaxValue;
        float maxNoiseHeight = float.MinValue;
        
        if (noiseScale <= 0) noiseScale = 0.0001f; // Prevent any division by 0
        
        for (int i = 0; i < mapHeight; i++) {
            for (int j = 0; j < mapWidth; j++)
            {
                float frequency = 1;
                float amplitude = 1;
                float noiseHeight = 0;
                for (int k = 0; k < octaves; k++)
                {
                    float sampleI = i / noiseScale * frequency;
                    float sampleJ = j / noiseScale * frequency;
                    float perlinValue = Mathf.PerlinNoise(sampleI, sampleJ) * 2 - 1; // to have -1 to 1 values
                    noiseHeight += perlinValue * amplitude;
                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if (noiseHeight > maxNoiseHeight) maxNoiseHeight = noiseHeight;
                else if (noiseHeight < minNoiseHeight) minNoiseHeight = noiseHeight;
                noiseMap[i, j] = noiseHeight;
            }
        }

        // Normalize
        for (int i = 0; i < mapHeight; i++)
        {
            for (int j = 0; j < mapWidth; j++)
            {
                noiseMap[i, j] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[i, j]);
            }
        }

        return noiseMap;
    }
}
