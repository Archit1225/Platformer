using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    public LayerMask groundLayer;
    public Transform groundCheck;


    private float moveSpeed=4f;
    private float jumpSpeed=8f;
    private float moveInputX;
    private bool isFacingRight=true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInputX * moveSpeed, rb.linearVelocityY);
        if (isFacingRight && moveInputX < 0f)
        {
            Flip();
        }
        else if (!isFacingRight && moveInputX > 0f)
        {
            Flip();
        }

    }
    private void Update()
    {
        HandleAnimations();
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInputX = context.ReadValue<Vector2>().x;
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded() && context.performed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
        }
    }

    private void HandleAnimations()
    {
        animator.SetBool("isIdle", Mathf.Abs(moveInputX) < 0.1f && isGrounded());
        animator.SetBool("isWalking", Mathf.Abs(moveInputX) > 0.1f && isGrounded());

        animator.SetBool("isJumping", rb.linearVelocityY > 0.1f);
        animator.SetBool("isGrounded", isGrounded());

        animator.SetFloat("yVelocity", rb.linearVelocityY); 
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    //private void ChangeState(PlayerState newState)
    //{
    //    if (playerState == PlayerState.Idle)
    //        animator.SetBool("IsIdle", false);
    //    else if (playerState == PlayerState.Walking)
    //        animator.SetBool("IsWalking", false);

    //    playerState = newState;

    //    if (playerState == PlayerState.Idle)
    //        animator.SetBool("IsIdle", true);
    //    else if (playerState == PlayerState.Walking)
    //        animator.SetBool("IsWalking", true);
    //}
}
//public enum PlayerState
//{
//    Idle, Walking, Jumping, Landing
//}
