using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;


namespace proceduralMaps
{
    public enum MapDisplay
    {
        Height,
        Moisture,
        Heat,
        Biome
    }

    [ExecuteInEditMode]
    public class Map : MonoBehaviour
    {
        public MapDisplay displayType;
        
        [SerializeField]
        private RawImage debugImage;
        
        [Header("Dimensions")]
        public int width;
        public int height;
        public float scale;
        public Vector2 offset;

        [Header("Height map")]
        public Wave[] heightWaves;
        public Gradient heightDebugColors;
        public float[,] HeightMap;
        
        [Header("Moisture map")]
        public Wave[] moistureWaves;
        public Gradient moistureDebugColors;
        public float[,] MoistureMap;
        
        [Header("Heat map")]
        public Wave[] heatWaves;
        public Gradient heatDebugColors;
        public float[,] HeatMap;
        

        private float _lastGenerateTime;

        void Start()
        {
            GenerateMap();
            
            if(debugImage == null)
            {
                debugImage = GetComponent<RawImage>();
                Debug.Log("raw image not found!");
            }
        }

        void Update()
        {
            if (!(Time.time - _lastGenerateTime > 0.1f)) return;
            _lastGenerateTime = Time.time;
            GenerateMap();
        }

        void GenerateMap()
        {
            //Generating the height map
            HeightMap = NoiseGenerator.Generate(width, height, scale, offset, heightWaves);
            
            //Generating the moisture map
            MoistureMap = NoiseGenerator.Generate(width, height, scale, offset, moistureWaves);
            
            //Generating the heat map
            HeatMap = NoiseGenerator.Generate(width, height, scale, offset, heatWaves);
            
            Color[] pixels = new Color[width * height];
            int i = 0;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    switch (displayType)
                    {
                        case MapDisplay.Height:
                            pixels[i] = heightDebugColors.Evaluate(HeightMap[x, y]);
                            break;
                        case MapDisplay.Moisture:
                            pixels[i] = moistureDebugColors.Evaluate(MoistureMap[x, y]);
                            break;
                        case MapDisplay.Heat:
                            pixels[i] = heatDebugColors.Evaluate(HeatMap[x, y]);
                            break;
                        
                        default:
                            break;
                    }

                    i++;

                    /*
                     * pixels[i] = Color.Lerp(Color.black, Color.white, HeightMap[x, y]);
                     *i++;
                     */
                }
            }
            
            Texture2D tex = new Texture2D(width, height);
            tex.SetPixels(pixels);
            tex.filterMode = FilterMode.Point;
            tex.Apply();
            
            debugImage.texture = tex;

            /*
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Debug.Log(HeightMap[x, y]);
                }
            }
            */
        }
    }

}

