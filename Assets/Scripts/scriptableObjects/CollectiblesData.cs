using UnityEngine;

namespace scriptableObjects
{
    public abstract class CollectiblesData : ScriptableObject
    {
        [Header("Visual & Prefab")]
        public GameObject collectiblePrefab;
        
        [Header("World Behavior")]
        public float despawnTime;
        
        [Tooltip("Sound effect to play on pickup.")]
        public AudioClip collectSound;
        
        public abstract void ApplyEffect(GameObject collector);
    }
}