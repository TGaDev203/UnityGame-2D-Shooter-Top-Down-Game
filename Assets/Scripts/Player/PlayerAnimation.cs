using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //! Components
    private Animator playerAnimation;
    private Rigidbody2D playerRigidBody;
    public float moveSpeed = 5f; // Speed of movement
    private Vector2 targetPosition; // Target position to move towards

    private void Awake()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        playerAnimation = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleAnimation();
        MoveTowardsMouse();
    }

    private void HandleAnimation()
    {
        // Check if there is significant movement
        bool isMoving = playerRigidBody.velocity.magnitude > Mathf.Epsilon;
        playerAnimation.SetBool("isMoving", isMoving);
    }

    private void MoveTowardsMouse()
    {
        // Get mouse position in world space
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate direction vector from player to mouse
        Vector2 direction = (mousePosition - playerRigidBody.position).normalized;

        // Move the player towards the mouse position
        playerRigidBody.velocity = direction * moveSpeed;

        // Optional: Rotate smoothly to face the direction of movement
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Convert to degrees
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

            // Use the transform's rotation for Quaternion.RotateTowards
            playerRigidBody.transform.rotation = Quaternion.RotateTowards(playerRigidBody.transform.rotation, targetRotation, 360 * Time.deltaTime); // Adjust rotation speed as needed
        }
    }
}
