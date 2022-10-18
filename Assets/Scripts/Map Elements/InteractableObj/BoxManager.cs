using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : InteractableObj
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer;
    private Transform parent;
    private Square square;
    private Vector2 savePosition;
    private bool grab = false;
    private float animSpeed = 0.1f; // 0 to 1
    private float sizeWhenGrab = 0.4f; // 0 to 1

    protected override void Start()
    {
        base.Start();
        parent = transform.parent;
        square = FindObjectOfType<Square>();
    }

    protected override void Update()
    {
        base.Update();
        if (grab) {
            transform.localPosition = Vector2.Lerp(transform.localPosition, Vector2.zero, animSpeed);
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(sizeWhenGrab, sizeWhenGrab), animSpeed);
        } else {
            transform.localScale = Vector2.Lerp(transform.localScale, Vector2.one, animSpeed);
        }
    }


    protected override void ObjAction()
    {
        grab = !grab;
        if (grab) {
            rigidBody.isKinematic = true;
            rigidBody.velocity = Vector2.zero;
            boxCollider.enabled = false;
            transform.SetParent(square.transform);
            square.moveSpeed /= 2;
            square.jumpForce /= 2;
            transform.localRotation = Quaternion.identity;
            savePosition = transform.localPosition;
        } else {
            if (Physics2D.OverlapCircle(transform.TransformPoint(Vector2.Lerp(Vector2.zero, savePosition / transform.localScale, 0.75f)), 0.45f, groundLayer))
            // If between new pos and player == layer => between player and new pos, else new pos
                transform.localPosition = Vector2.Lerp(transform.localPosition, savePosition, 0.45f);
            else
                transform.localPosition = savePosition;
            rigidBody.isKinematic = false;
            boxCollider.enabled = true;
            transform.SetParent(parent);
            square.moveSpeed *= 2;
            square.jumpForce *= 2;
        }
    }
}
