using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : Player
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    [HideInInspector] public float moveSpeed = 5f;
    [HideInInspector] public float jumpForce = 7f;
    [HideInInspector] public bool isGrounded = true;
    [HideInInspector] public Held held;
    private bool canThrow = true;
    private float horizontal;
    private float lastDirection = 1f; // -1 for left | 1 for right
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
            if (inputManager.GetKeyDown("SquareInteract"))
                CheckIfHeld();
        }
        CheckIfFall();//only stop the rotation if grounded
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
        horizontal = 0f;
        if (inputManager.GetKey("SquareMoveLeft"))
            horizontal -= 1f;
        if (inputManager.GetKey("SquareMoveRight"))
            horizontal += 1f;
        animator.SetBool("isMoving", horizontal != 0f);
        if (horizontal > 0f)
            lastDirection = 1;
        else if (horizontal < 0f)
            lastDirection = -1;
    }

    private void CheckIfJump()
    {
        if (inputManager.GetKeyDown("SquareJump") && isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        else if (inputManager.GetKeyUp("SquareJump") && rb.velocity.y > 0f)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
    }

    private void CheckIfFall()
    {
        if (!isGrounded) {
            rotationZ -= Time.deltaTime * rotationSpeed * lastDirection;
            transform.GetChild(0).rotation = Quaternion.Euler(0f, 0f, rotationZ);
        } else {
            rotationZ = 0f;
            transform.GetChild(0).rotation = Quaternion.identity;
        }
    }

    private void CheckIfHeld()
    {
        if (held != null && canThrow) {
            held.IsThrow();
            held = null;
        }
        canThrow = true;
    }

    public void StopMoving()
    {
        horizontal = 0f;
        animator.SetBool("isMoving", false);
    }

    public bool HoldObj(Held held)
    {
        if (this.held != null )
            return false;
        this.held = held;
        canThrow = false;
        return true;
    }
}
