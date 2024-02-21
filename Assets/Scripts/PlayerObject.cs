using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public float playerMoveSpeed = 1.0f;
    public float jumpingPower = 8.0f;

    private float horizontal = 0.0f;
    private bool jump = false;
    private bool space = false;
    private bool isFacingRight = true;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("PlayerSpeed", Mathf.Abs(horizontal));

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Space"))
        {
            space = true;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * playerMoveSpeed, rb.velocity.y);

        if (jump)
        {
            rb.AddForce(new Vector2(0f, jumpingPower), ForceMode2D.Impulse);
            jump = false;
        }

        if ((isFacingRight && horizontal < 0.0f) || (!isFacingRight && horizontal > 0.0f))
        {
            Flip();
        }

        if (IsGrounded())
        {
            animator.SetBool("IsJumping", false);
        }

        space = false;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        sr.flipX = !isFacingRight;
    }
}
