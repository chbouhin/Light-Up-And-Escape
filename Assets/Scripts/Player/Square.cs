using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : Player
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    private float horizontal;
    private float moveSpeed = 5f;
    private float movingDirection = 1; // -1 for left | 1 for right
    private float jumpForce = 7f;
    private bool isGrounded = true;
    private float rotationZ = 0f;
    private float rotationSpeed = 275f;

    private void Update()
    {
        CheckIfGrounded();
        if (gameManager.isInGame) {
            CheckIfMoving();
            CheckIfJump();
            if (transform.position.y < -6f)
                Die();
        }
        CheckIfFall();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }

    private void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);
    }

    private void CheckIfMoving()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetBool("isMoving", horizontal != 0f);
        if (horizontal > 0f)
            movingDirection = 1;
        else if (horizontal < 0f)
            movingDirection = -1;
    }

    private void CheckIfJump()
    {
        if (Input.GetButtonDown("SquareJump") && isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        else if (Input.GetButtonUp("SquareJump") && rb.velocity.y > 0f)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
    }

    private void CheckIfFall()
    {
        if (!isGrounded) {
            rotationZ -= Time.deltaTime * rotationSpeed * movingDirection;
            transform.GetChild(0).rotation = Quaternion.Euler(0f, 0f, rotationZ);
        } else {
            rotationZ = 0f;
            transform.GetChild(0).rotation = Quaternion.identity;
        }
    }

    public void StopMoving()
    {
        horizontal = 0f;
        animator.SetBool("isMoving", false);
    }
}
