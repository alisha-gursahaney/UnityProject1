using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public float playerMoveSpeed = 5.0f;
    public float jumpingPower = 8.0f;

    private float horizontal = 0.0f;
    private bool jump = false;
    private bool isFacingRight = true;
    private bool isGrounded = false;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal") * playerMoveSpeed;
        animator.SetFloat("PlayerSpeed", Mathf.Abs(horizontal));

        if ((isFacingRight && horizontal < 0.0f) || (!isFacingRight && horizontal > 0.0f))
        {
            Flip();
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(horizontal, 0f, 0f);
        transform.position += movement * Time.fixedDeltaTime;

        if (jump)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpingPower), ForceMode2D.Impulse);
            jump = false;
        }
    }

    private bool IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        return isGrounded;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
