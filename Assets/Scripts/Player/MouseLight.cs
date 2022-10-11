using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLight : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private Vector2 position;
    private float moveSpeed = 0.05f; // 0 to 1
    private bool canMove = false;

    private void Update()
    {
        if (canMove)
            Move();
    }

    private void Move()
    {
        Vector2 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector3(Mathf.Clamp(mousePosition.x, minScreenBounds.x, maxScreenBounds.x), Mathf.Clamp(mousePosition.y, minScreenBounds.y, maxScreenBounds.y), 0f);
        position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(position);
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }
}
