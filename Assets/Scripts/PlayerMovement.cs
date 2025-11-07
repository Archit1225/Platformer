using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    public LayerMask groundLayer;
    public Transform groundCheck;


    public float moveSpeed = 4f;
    public float fallGravity = 2f;
    public float normalGravity = 1.5f;
    public float jumpGravity = 1.5f;


    private float jumpSpeed = 8f;
    private float moveInputX;
    private bool isFacingRight = true;


    private bool isDashing = false;
    public float dashCooldown = 10f;
    public float dashSpeed = 10f;
    private float dashCooldownTimer;

    private readonly WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dashCooldownTimer = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        HandleGravity();
        if (!isDashing)
        {
            rb.linearVelocity = new Vector2(moveInputX * moveSpeed, rb.linearVelocityY);
        }
        if (isFacingRight && moveInputX < 0f)
        {
            Flip();
        }
        else if (!isFacingRight && moveInputX > 0f)
        {
            Flip();
        }
    }

    public IEnumerator Dash()
    {
        rb.linearVelocityX = dashSpeed*(transform.localScale.x)/Math.Abs(transform.localScale.x);
        rb.gravityScale = 0f;
        isDashing = true;
        Debug.Log("Dashing");
        yield return waitForSeconds;
        rb.gravityScale = normalGravity;
        isDashing = false;
    }

    private void Update()
    {
        HandleAnimations();

        if (Keyboard.current.shiftKey.wasPressedThisFrame && dashCooldownTimer == 0)
        {
            StartCoroutine(Dash());
        }
        else if (dashCooldownTimer > 0) {
            dashCooldownTimer -= Time.deltaTime;
        }
        else if (dashCooldownTimer < 0) {
            dashCooldownTimer =0;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInputX = context.ReadValue<Vector2>().x;
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded() && context.performed && !isDashing)
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

    private void HandleGravity()
    {
        if (rb.linearVelocityY < 0f)
        {
            rb.gravityScale = fallGravity;
        }
        else if (rb.linearVelocityY > 0f)
        {
            rb.gravityScale = jumpGravity;
        }
        else
        {
            rb.gravityScale = normalGravity;
        }
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
}
