using UnityEngine;

namespace proceduralMaps
{
    public class NoiseGenerator : MonoBehaviour
    {
        public static float[,] Generate(int width, int height, float scale, Vector2 offset)
        {
            float[,] noiseMap = new float[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    float sampleX = (float)x * scale + offset.x,
                          sampleY = (float)y * scale + offset.y;
                    
                    noiseMap[x, y] = Mathf.PerlinNoise(sampleX, sampleY);
                }
            }
            
            return noiseMap;
        }
    }
}
