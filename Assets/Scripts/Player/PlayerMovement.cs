using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //! Component
    [SerializeField] private float moveSpeed;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float rotationSpeed;

    private Rigidbody2D playerRigidbody2D;
    private Vector2 movement;
    private Vector3 lastMousePos;

    private void Awake()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void Update()
    {
        CheckStartGame();
        UpdateMovementInput();
    }

    //! Handle Movement
    private void UpdateMovementInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        Vector2 newPosition = playerRigidbody2D.position + movement * moveSpeed * Time.fixedDeltaTime;
        playerRigidbody2D.MovePosition(newPosition);
    }

private void RotatePlayer()
{
    Vector3 mousePos = Input.mousePosition;

    // Only update rotation if the mouse has moved significantly
    if ((mousePos - lastMousePos).sqrMagnitude > 0.1f)
    {
        Vector3 worldMousePos = mainCamera.ScreenToWorldPoint(mousePos);
        worldMousePos.z = 0f; // Ensure the z-axis is 0 for 2D

        Vector2 direction = (worldMousePos - transform.position).normalized;
        if (direction != Vector2.zero) // Prevent NaN issues when direction is zero
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Create a target rotation
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            
            // Increase rotation speed by a multiplier
            playerRigidbody2D.rotation = Mathf.LerpAngle(playerRigidbody2D.rotation, angle, rotationSpeed * Time.deltaTime);
        }

        lastMousePos = mousePos; // Update last mouse position
    }
}


    //! Check State
    private void CheckStartGame()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Cursor.visible = false;
            // Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}
