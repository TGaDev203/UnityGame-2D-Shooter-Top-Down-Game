using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //! Components
    [SerializeField] private float moveSpeed;
    [SerializeField] private float steerSpeed;
    private Rigidbody2D playerRigidbody2D;

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
        HandlePlayerMovement();
    }

    private void HandlePlayerMovement()
    {
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        transform.Translate(0, moveAmount, 0);
        transform.Rotate(0, 0, -steerAmount);
    }
}
