using System;
using UnityEngine;
using GeneralScripts; 

namespace MainCharacterScripts
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;
        
        [Header("Component References")]
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerShooting playerShooting;

        [Header("Stats")]
        [SerializeField] private PlayerStats playerStats;
        
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
        }

        private void Update()
        {
            var moveInput = KeyboardInputListener.Instance.MoveInput;
            float strafeInput = KeyboardInputListener.Instance.StrafeInput;
            float rotationInput = KeyboardInputListener.Instance.RotationInput;
            
            Vector2 relativeMovement = (Vector2)transform.up * moveInput.y + (Vector2)transform.right * strafeInput;
            
            if (relativeMovement.magnitude > 1f)
            {
                relativeMovement.Normalize();
            }
            
            playerMovement.SetInput(relativeMovement);
            
            playerMovement.Tick();
            
            transform.Rotate(Vector3.forward, rotationInput * playerStats.rotationSpeed * Time.deltaTime);
        }
    }
}
