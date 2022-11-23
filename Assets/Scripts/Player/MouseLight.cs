using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLight : Player
{
    private Vector2 position;
    private Vector2 destination;
    private float moveSpeed = 0.04f;
    private float maxDistanceMove = 4f;

    private void Start()
    {
        position = transform.position;
        destination = position;
    }

    private void Update()
    {
        if (gameManager.isInGame)
            Move();
    }

    private void Move()
    {
        if (inputManager.GetKey("MouseLightMove")) {
            Vector2 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
            Vector2 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D raycastHit = Physics2D.Linecast(transform.position, destination, groundLayer);
            if (raycastHit)
                destination = raycastHit.point - (Vector2)Vector3.Normalize(raycastHit.point - (Vector2)transform.position) * 0.1f;
            destination = new Vector3(Mathf.Clamp(destination.x, minScreenBounds.x, maxScreenBounds.x), Mathf.Clamp(destination.y, minScreenBounds.y, maxScreenBounds.y), 0f);
        }
        float distance = Vector2.Distance(transform.position, destination);
        if (distance > maxDistanceMove)
            distance = maxDistanceMove;
        position = Vector2.Lerp(transform.position, transform.position + Vector3.Normalize(destination - (Vector2)transform.position) * distance, moveSpeed);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(position);
    }

    public void SetPos(Vector2 pos)
    {
        destination = pos;
        position = pos;
        transform.position = pos;
    }
}
