using scriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Collectibles
{
    [RequireComponent(typeof(Collider2D))]
    public class CollectibleBase : MonoBehaviour
    {
        [Header("Collectible Data")]
        [SerializeField]
        private CollectiblesData data;
        
        [Header("Collectible Properties")]
        [SerializeField]
        private Collider2D _collider;
        
        private float _despawnTimer;
        private void Awake()
        {
            if (_collider != null) return;
            _collider = GetComponent<Collider2D>();
            if (_collider != null)
            {
                _collider.isTrigger = true;
            }
        }

        private void Start()
        {
            _despawnTimer = data.despawnTime;
        }

        private void Update()
        {
            if (_despawnTimer > 0)
            {
                _despawnTimer -= Time.deltaTime;

                if (_despawnTimer <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Collect(other.gameObject);
            }
        }
        public void Collect(GameObject collector)
        {
            if (data == null)
            {
                Debug.LogError($"Collectible on {gameObject.name} is missing CollectiblesData!");
                return;
            }
            
            data.ApplyEffect(collector);
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }
}
