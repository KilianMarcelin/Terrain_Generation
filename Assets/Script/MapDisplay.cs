using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRenderer;
    public Color[] terrainColors;
    public float[] terrainLevels;
    public void DrawNoiseMap(float[,] noiseMap)
    {
        int height = noiseMap.GetLength(0);
        int width = noiseMap.GetLength(1);
        Texture2D texture = new Texture2D(width,height);
        
        // More efficient than set color by color
        Color[] colorMap = new Color[width * height];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                float val = noiseMap[i, j];
                int terrainColorIndex = 0;
                for (int k = 0; k < terrainLevels.Length; k++)
                {
                    if (val <= terrainLevels[k])
                    {
                        terrainColorIndex = k;
                        break;
                    }
                    
                }
                colorMap[i * width + j] = terrainColors[terrainColorIndex];
            }
        }
        texture.SetPixels(colorMap);
        texture.Apply();

        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(width/10, 1, height/10);
    }
}
