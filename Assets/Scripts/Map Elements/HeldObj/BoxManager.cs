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
    private float statMultiplier = 1.65f; // bigger is it, lower the player will move and jump

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
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, savePosition, 1.25f, groundLayer);
        if (raycastHit)
            transform.position = Vector2.Lerp(transform.position, raycastHit.point, 0.75f);
        else
            transform.localPosition = savePosition;
        rigidBody.isKinematic = false;
        normalBoxCollider.enabled = true;
        triggerBoxCollider.enabled = true;
        base.IsThrow();
        square.SetStat(true, statMultiplier);
    }
}
