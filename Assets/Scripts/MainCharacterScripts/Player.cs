using UnityEngine;
using UnityEngine.InputSystem; // Required for new Input System

namespace MainCharacterScripts
{
    public class Player : MonoBehaviour
    {
        public float moveSpeed;

        private Vector2 moveInput;

        void Update()
        {
            // Movement using new Input System
            moveInput = new Vector2(
                Keyboard.current.aKey.isPressed ? -1 :
                Keyboard.current.dKey.isPressed ? 1 : 0,

                Keyboard.current.sKey.isPressed ? -1 :
                Keyboard.current.wKey.isPressed ? 1 : 0
            );

            Vector3 moveDir = new Vector3(moveInput.x, moveInput.y, 0) * (moveSpeed * Time.deltaTime);
            transform.position += moveDir;
        }
    }
}