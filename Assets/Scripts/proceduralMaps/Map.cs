using System.Collections.Generic;
using scriptableObjects;
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
        public BiomePreset[]  biomes;
        public GameObject tilePrefab;
        
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
            if(debugImage == null)
            {
                debugImage = GetComponent<RawImage>();
                Debug.Log("raw image not found!");
            }

            if (Application.isPlaying)
            {
                GenerateMap();
            }
        }

        void Update()
        {
            if (Application.isPlaying)
                return;

            if (Time.time - _lastGenerateTime > 0.1f)
            {
                _lastGenerateTime = Time.time;
                GenerateMap();
            }
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

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
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

                        case MapDisplay.Biome:
                        {
                            BiomePreset biome = GetBiome(HeightMap[x, y], MoistureMap[x, y], HeatMap[x, y]);
                            pixels[i] = biome.debugColor;

                            if (Application.isPlaying)
                            {
                                GameObject tile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                                tile.GetComponent<SpriteRenderer>().sprite = biome.GetTileSprite();
                            }
                            
                            break;
                        }
                        
                        default:
                            Debug.Log("Unknown display type");
                            break;
                    }

                    i++;
                    
                }
            }
            
            Texture2D tex = new Texture2D(width, height);
            tex.SetPixels(pixels);
            tex.filterMode = FilterMode.Point;
            tex.Apply();
            
            debugImage.texture = tex;
            
        }

        BiomePreset GetBiome(float height, float moisture, float heat)
        {
            BiomePreset biomeToReturn = null;
            List<BiomePreset> tempBiomes = new List<BiomePreset>();

            foreach (BiomePreset biome in biomes)
            {
                if (biome.MatchCondition(height, moisture, heat))
                {
                    tempBiomes.Add(biome);
                }
            }

            float curValue = 0.0f;

            foreach (BiomePreset biome in tempBiomes)
            {
                float diffValue = (height - biome.minHeight) + (moisture - biome.minMoisture) +  (heat - biome.minHeat);

                if (biomeToReturn == null)
                {
                    biomeToReturn = biome;
                    curValue = diffValue;
                }
                else if (diffValue < curValue)
                {
                    biomeToReturn = biome;
                    curValue = diffValue;
                }
            }

            if (biomeToReturn == null)
                return biomes[0];
            
            return biomeToReturn;
        }
    }
}