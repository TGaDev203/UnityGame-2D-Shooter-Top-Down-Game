using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //! Components
    [SerializeField] private Animator playerAnimation;
    private Rigidbody2D playerRigidBody;

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
    }

    private void HandleAnimation()
    {
        MoveAnimation();
    }

    //! Move Animation
    private void MoveAnimation()
    {
        bool PlayerHasHorizontalSpeed = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        playerAnimation.SetBool("isMoving", PlayerHasHorizontalSpeed);
    }
}
