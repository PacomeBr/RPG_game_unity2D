    using System.Collections;
    using System.Collections.Generic;
    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class PlayerMovements : MonoBehaviour
    {
        [Header("Movement")]
        float horizontal;
        float vertical;
        Vector2 moveInput;
        public float moveSpeed;
        public float walkSpeed;
        public float runSpeedMultiplier;
        private bool sprintInput;
        public bool isWalkling;
        public bool isRunning;
        private bool facingRight;
        public bool canFacing;
        public bool InventoryVisibility = false;
        public BreakingObjectSystem breakingObjectSystem;
        public GameObject inventory;

        Rigidbody2D rb;


        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            moveSpeed = walkSpeed;
            inventory.SetActive(false);
        }
        void Update()
        {
            Move();
        }
        public void MoveInput(InputAction.CallbackContext context)
        {

            isWalkling = context.performed && !isRunning;
            moveInput = context.ReadValue<Vector2>().normalized;
            horizontal = moveInput.x;
            vertical = moveInput.y;
        }
        public void Sprint(InputAction.CallbackContext context)
        {
        
            if (context.performed && moveInput != Vector2.zero)
            {
                isRunning = true;
                isWalkling = false;
                moveSpeed *= runSpeedMultiplier;
            }
            else if(context.canceled && isRunning)
            {
                isRunning = false;
                isWalkling = true;
                moveSpeed = walkSpeed;
            }


        }

        void Move()
        {
            rb.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
            if (horizontal > 0 && facingRight)
            {
                Flip();
            }
            else if (horizontal < 0 && !facingRight)
            {
                Flip();
            }
        }

        void Flip()
        {
            if (canFacing == true)
            {
                Vector3 currentScale = gameObject.transform.localScale;
                currentScale.x *= -1;
                gameObject.transform.localScale = currentScale;

                facingRight = !facingRight;
            }
        }


        public void Immobilizes(bool isImmobilizing)
        {
            if (isImmobilizing)
            {
                moveSpeed = 0f;
                canFacing = false;
            }
            else
            {
                moveSpeed = walkSpeed;
                canFacing = true;
            }
            
        }
        public void OpenInventory(InputAction.CallbackContext context)
        {
            
            if (context.performed)
            {
                InventoryVisibility = !InventoryVisibility;
                if (InventoryVisibility)
                {
                    inventory.SetActive(true);
                }
                else { inventory.SetActive(false); }
            }
        }
    }
