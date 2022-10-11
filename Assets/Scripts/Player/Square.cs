using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator transition;
    private float horizontal;
    private float moveSpeed = 5f;
    private float jumpForce = 7f;
    private bool canMove = false;
    private bool isGrounded = true;

    private void Update()
    {
        if (canMove)
            Move();
    }

    private void Move()
    {
        CheckIfGrounded();
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        else if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        transition.SetFloat("velocity", rb.velocity.y);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }

    private void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        transition.SetBool("isGrounded", isGrounded);
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }
}
