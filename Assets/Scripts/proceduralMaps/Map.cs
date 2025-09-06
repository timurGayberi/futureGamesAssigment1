using UnityEngine;

namespace proceduralMaps
{
    public class Map : MonoBehaviour
    {    
        [Header("Dimensions")]
        public int width;
        public int height;
        public float scale;
        public Vector2 offset;

        [Header("Height map")]
        public float[,] HeightMap;

        void Start()
        {
            GenerateMap();
        }

        void GenerateMap()
        {
            HeightMap = NoiseGenerator.Generate(width, height, scale, offset);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Debug.Log(HeightMap[x, y]);
                }
            }
        }
    }

}

