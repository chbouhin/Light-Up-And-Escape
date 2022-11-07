using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : Held
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private BoxCollider2D normalBoxCollider;
    [SerializeField] private BoxCollider2D triggerBoxCollider;
    [SerializeField] private LayerMask groundLayer;
    private Vector2 savePosition;
    private float statMultiplier = 1.75f; // bigger is it, lower the player will move and jump

    public override void IsGrab()
    {
        rigidBody.isKinematic = true;
        rigidBody.velocity = Vector2.zero;
        normalBoxCollider.enabled = false;
        triggerBoxCollider.enabled = false;
        base.IsGrab();
        square.SetStat(false, statMultiplier);
        transform.localRotation = Quaternion.identity;
        savePosition = transform.localPosition;
    }

    public override void IsThrow()
    {
        // If between new pos and player == layer => between player and new pos, else new pos
        if (Physics2D.OverlapCircle(transform.TransformPoint(Vector2.Lerp(Vector2.zero, savePosition / transform.localScale, 0.8f)), 0.45f, groundLayer))
            transform.localPosition = Vector2.Lerp(transform.localPosition, savePosition, 0.45f);
        else
            transform.localPosition = savePosition;
        rigidBody.isKinematic = false;
        normalBoxCollider.enabled = true;
        triggerBoxCollider.enabled = true;
        base.IsThrow();
        square.SetStat(true, statMultiplier);
    }
}
