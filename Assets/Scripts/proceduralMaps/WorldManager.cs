using System.Collections.Generic;
using UnityEngine;

namespace proceduralMaps
{
    public class WorldManager : MonoBehaviour
    {
        public static WorldManager Instance;

        [Header("References")]
        [SerializeField] private Transform playerTransform;
        [SerializeField] private GameObject chunkPrefab;
        
        [Header("Settings")]
        public int chunkWidth = 32;
        public int chunkHeight = 32;
        public int viewDistance = 3; // Number of chunks to load in each direction from the player.
        
        private Vector2Int _currentPlayerChunk;
        private Dictionary<Vector2Int, GameObject> _loadedChunks = new Dictionary<Vector2Int, GameObject>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            if (playerTransform == null)
            {
                Debug.LogError("Player Transform is not assigned in WorldManager.");
                return;
            }

            _currentPlayerChunk = GetChunkCoordinates(playerTransform.position);
            GenerateWorld();
        }

        private void Update()
        {
            // IMPORTANT: Check if the player still exists before trying to access their position.
            if (playerTransform == null)
            {
                return;
            }

            Vector2Int newChunk = GetChunkCoordinates(playerTransform.position);
            if (newChunk != _currentPlayerChunk)
            {
                _currentPlayerChunk = newChunk;
                GenerateWorld();
            }
        }

        private void GenerateWorld()
        {
            HashSet<Vector2Int> chunksToKeep = new HashSet<Vector2Int>();
            
            for (int x = -viewDistance; x <= viewDistance; x++)
            {
                for (int y = -viewDistance; y <= viewDistance; y++)
                {
                    Vector2Int chunkCoords = new Vector2Int(_currentPlayerChunk.x + x, _currentPlayerChunk.y + y);
                    chunksToKeep.Add(chunkCoords);

                    if (!_loadedChunks.ContainsKey(chunkCoords))
                    {
                        GenerateChunk(chunkCoords);
                    }
                }
            }
            
            List<Vector2Int> chunksToUnload = new List<Vector2Int>();
            foreach (var chunk in _loadedChunks)
            {
                if (!chunksToKeep.Contains(chunk.Key))
                {
                    chunksToUnload.Add(chunk.Key);
                }
            }
            
            foreach (var chunkCoords in chunksToUnload)
            {
                UnloadChunk(chunkCoords);
            }
        }

        private void GenerateChunk(Vector2Int coords)
        {
            GameObject chunkInstance = Instantiate(chunkPrefab, new Vector3(coords.x * chunkWidth, coords.y * chunkHeight, 0), Quaternion.identity);
            chunkInstance.name = $"Chunk_{coords.x}_{coords.y}";
            
            Map mapComponent = chunkInstance.GetComponent<Map>();
            if (mapComponent == null)
            {
                Debug.LogError("Chunk Prefab is missing a Map component!");
                return;
            }

            // Set the map dimensions and offset for the chunk
            mapComponent.width = chunkWidth;
            mapComponent.height = chunkHeight;
            mapComponent.offset = new Vector2(coords.x * chunkWidth, coords.y * chunkHeight);

            _loadedChunks.Add(coords, chunkInstance);
            
            // Generate the map data for this specific chunk.
            mapComponent.GenerateMap();
        }

        private void UnloadChunk(Vector2Int coords)
        {
            if (_loadedChunks.TryGetValue(coords, out GameObject chunk))
            {
                Destroy(chunk);
                _loadedChunks.Remove(coords);
            }
        }
        
        private Vector2Int GetChunkCoordinates(Vector3 position)
        {
            int chunkX = Mathf.FloorToInt(position.x / chunkWidth);
            int chunkY = Mathf.FloorToInt(position.y / chunkHeight);
            return new Vector2Int(chunkX, chunkY);
        }
    }
}
