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
    private float movingDirection = 1; // -1 for left | 1 for right
    private float jumpForce = 7f;
    private bool canMove = false;
    private bool isGrounded = true;
    private float rotationZ = 0f;
    private float rotationSpeed = 175f;

    private void Update()
    {
        if (canMove) {
            CheckIfGrounded();
            CheckIfMoving();
            CheckIfJump();
            CheckIfFall();
        }
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

    private void CheckIfMoving()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        transition.SetBool("isMoving", horizontal != 0f);
        if (horizontal > 0f)
            movingDirection = 1;
        else if (horizontal < 0f)
            movingDirection = -1;
    }

    private void CheckIfJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        else if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
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

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }
}
