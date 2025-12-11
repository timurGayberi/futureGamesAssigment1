using System.Collections.Generic;
using UnityEngine;

namespace generalScripts.Visuals
{
    public class SpriteParallaxSystem : MonoBehaviour
    {
        [System.Serializable]
        public class ParallaxLayer
        {
            [Tooltip("The Transform of the sprite layer.")]
            public Transform layerTransform;

            [Tooltip("How much this layer moves relative to the input vector. " +
                     "Low values (e.g. 0.01) for base layers, higher (e.g. 0.1) for top layers.")]
            public float speedFactor = 0.05f;

            [HideInInspector] public Vector3 initialLocalPosition;
        }

        [Header("Configuration")]
        [SerializeField] private List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

        [Tooltip("Multiplies the calculated parallax vector.")]
        [SerializeField] private float globalIntensity = 1f;

        [Header("References")]
        [Tooltip("The camera used to calculate the look direction (mouse position).")]
        [SerializeField] private Camera mainCamera;

        [Header("Settings - Look")]
        [Tooltip("If true, layers shift based on mouse position.")]
        [SerializeField] private bool useMouseLook = true;
        [SerializeField] private float lookIntensity = 1f;
        [SerializeField] private float lookClampMagnitude = 5f;

        [Header("Settings - Movement")]
        [Tooltip("If true, layers shift based on Rigidbody velocity.")]
        [SerializeField] private bool useMovement = true;
        [SerializeField] private float movementIntensity = 0.5f;

        private Rigidbody2D _rb;

        private void Awake()
        {
            foreach (var layer in parallaxLayers)
            {
                if (layer.layerTransform != null)
                {
                    layer.initialLocalPosition = layer.layerTransform.localPosition;
                }
            }

            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }

            if (useMovement)
            {
                _rb = GetComponentInParent<Rigidbody2D>();
            }
        }

        public Vector2 ExternalLookInput { get; set; }

        private void LateUpdate()
        {
            Vector2 finalWorldParallax = Vector2.zero;
            if (useMouseLook && mainCamera != null)
            {
                Vector3 mouseScreenPos = UnityEngine.Input.mousePosition;
                Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, -mainCamera.transform.position.z));
                Vector2 lookDir = (mouseWorldPos - transform.position);

                lookDir = Vector2.ClampMagnitude(lookDir, lookClampMagnitude);
                finalWorldParallax += lookDir * lookIntensity;
            }

            finalWorldParallax += ExternalLookInput * lookIntensity;

            if (useMovement && _rb != null)
            {
                finalWorldParallax += _rb.linearVelocity * movementIntensity;
            }

            Vector2 finalLocalParallax = transform.InverseTransformVector(finalWorldParallax);

            UpdateParallax(finalLocalParallax);
        }

        public void UpdateParallax(Vector2 parallaxVector)
        {
            foreach (var layer in parallaxLayers)
            {
                if (layer.layerTransform == null) continue;

                Vector3 offset = (Vector3)parallaxVector * layer.speedFactor * globalIntensity;
                layer.layerTransform.localPosition = layer.initialLocalPosition + offset;
            }
        }
    }
}