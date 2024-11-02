using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //! Component
    [SerializeField] private float moveSpeed;
    [SerializeField] private Camera mainCamera;

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

    private void Update()
    {
        CheckStartGame();
        UpdateMovementInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
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

        // Only update rotation if mouse has moved significantly
        if ((mousePos - lastMousePos).sqrMagnitude > 0.1f)
        {
            Vector3 worldMousePos = mainCamera.ScreenToWorldPoint(mousePos);
            worldMousePos.z = 0f;

            Vector2 direction = (worldMousePos - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            playerRigidbody2D.rotation = angle;

            lastMousePos = mousePos;
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
