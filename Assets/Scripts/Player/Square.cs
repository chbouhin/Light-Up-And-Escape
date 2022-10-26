using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Square : Player
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    [HideInInspector] public float moveSpeed = 5f;
    [HideInInspector] public float jumpForce = 7f;
    [HideInInspector] public bool isGrounded = true;
    [HideInInspector] public Held held;
    private float horizontal;
    private float lastDirection = 1f; // -1 for left | 1 for right
    private float rotationZ = 0f;
    private float rotationSpeed = 275f;
    private Dictionary<int, Held> heldObjs = new Dictionary<int, Held>();

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
        if (inputManager.GetKeyDown("SquareJump") && isGrounded) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            audioManager.Play("Jump");
        } else if (inputManager.GetKeyUp("SquareJump") && rb.velocity.y > 0f) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    // Only stop the rotation if grounded
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
        if (held != null) {
            held.IsThrow();
            held = null;
        } else if (heldObjs.Count > 0) {
            held = heldObjs.Values.First();
            held.IsGrab();
        }
    }

    public void StopMoving()
    {
        horizontal = 0f;
        animator.SetBool("isMoving", false);
    }

    public void HeldObjOnTriggerEnter(int instanceId, Held held)
    {
        heldObjs.Add(instanceId, held);
    }

    public void HeldObjOnTriggerExit(int instanceId)
    {
        heldObjs.Remove(instanceId);
    }
}
