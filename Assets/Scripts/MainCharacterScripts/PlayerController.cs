using System;
using generalScripts;
using generalScripts.Interfaces;
using UnityEngine;

namespace MainCharacterScripts
{
    /// <summary>
    /// Controls player helicopter movement and rotation based on processed inputs
    /// All parameters are adjustable in Unity Editor
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;

        [Header("Component References")]
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerShooting playerShooting;
        [SerializeField] private PlayerInputProcessor inputProcessor;

        [Header("Stats")]
        [SerializeField] private PlayerStats playerStats;

        [Header("Rotation Settings")]
        [Tooltip("Rotation speed multiplier")]
        [SerializeField] private float rotationSpeed = 5f;

        [Tooltip("Additional rotation from A/D steering")]
        [SerializeField] private float steeringRotationSpeed = 100f;

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

        public void Start()
        {
            playerMovement.Initialize(playerStats);

            // Validate input processor
            if (inputProcessor == null)
            {
                inputProcessor = GetComponent<PlayerInputProcessor>();
                if (inputProcessor == null)
                {
                    Debug.LogError("[PlayerController] PlayerInputProcessor component not found!");
                }
            }
        }

        private void Update()
        {
            if (inputProcessor == null) return;

            HandleMovement();
            HandleRotation();
            HandleSteering();
        }

        private void HandleMovement()
        {
            Vector2 movementDirection = Vector2.zero;

            // Only apply movement if allowed by dead zone
            if (inputProcessor.CanMove && inputProcessor.ThrottleInput != 0)
            {
                movementDirection = (Vector2)transform.up * inputProcessor.ThrottleInput;

                if (movementDirection.magnitude > 1f)
                {
                    movementDirection.Normalize();
                }
            }

            playerMovement.SetInput(movementDirection);
            playerMovement.Tick();
        }

        private void HandleRotation()
        {
            // Only rotate if conditions are met
            if (inputProcessor.ShouldRotate)
            {
                float currentAngle = transform.eulerAngles.z;
                float targetAngle = inputProcessor.TargetRotationAngle;
                float smoothFactor = inputProcessor.GetRotationSmoothFactor();

                float newAngle = Mathf.LerpAngle(
                    currentAngle,
                    targetAngle,
                    rotationSpeed * smoothFactor * Time.deltaTime
                );

                transform.rotation = Quaternion.Euler(0, 0, newAngle);
            }
        }

        private void HandleSteering()
        {
            // A/D keys for additional steering
            if (inputProcessor.SteerInput != 0)
            {
                transform.Rotate(
                    Vector3.forward,
                    inputProcessor.SteerInput * steeringRotationSpeed * Time.deltaTime
                );
            }
        }
    }
}