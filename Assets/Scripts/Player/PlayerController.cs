using UnityEngine;

namespace BulletHell {
    public class PlayerController : MonoBehaviour {
        [SerializeField] int currentWeaponIndex = 0;
        [SerializeField] GameObject[] inventory;
        [SerializeField] float moveSpeed = 6f;

        Vector2 mousePosition;
        Animator animator;
        Rigidbody2D rb;

        Camera playerCamera;
        Vector2 moveInput;

        void Start() {
            rb = GetComponent<Rigidbody2D>();
            playerCamera = Camera.main;
            animator = GetComponent<Animator>();
            inventory[currentWeaponIndex].SetActive(true);
        }

        void Update() {
            HandleMovement();
            SwitchWeapon();
            UpdateOrientation();

        }

        void HandleMovement() {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");

            rb.linearVelocity = moveInput.normalized * moveSpeed;

            if (moveInput != Vector2.zero)
                animator.SetBool("isMoving", true);
            else
                animator.SetBool("isMoving", false);
        }

        void UpdateOrientation() {
            // Flip sprite based on mouse position
            mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition);
            if (mousePosition.x > transform.position.x) {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            } else {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
        }

        void SwitchWeapon() {
            if (Input.GetKeyDown(KeyCode.Q)) {
                inventory[currentWeaponIndex].SetActive(false);
                if (--currentWeaponIndex < 0) {
                    currentWeaponIndex = inventory.Length - 1;
                }
                inventory[currentWeaponIndex].SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.E)) {
                inventory[currentWeaponIndex].SetActive(false);
                if (++currentWeaponIndex > inventory.Length - 1) {
                    currentWeaponIndex = 0;
                }
                inventory[currentWeaponIndex].SetActive(true);
            }
        }
    }
}