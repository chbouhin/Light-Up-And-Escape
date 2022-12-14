using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMob : Mob
{
    [SerializeField] private Transform groundCheck;
    private float bigJumpForce = 5f;
    private float smallJumpForce = 2f;
    private float lastMove;

    protected override Player GetPlayer()
    {
        return FindObjectOfType<Square>();
    }

    protected override void Update()
    {
        base.Update();
        velocity.y = rb.velocity.y;
        if (stopDetectTimer > 0f) {
            velocity.x = lastMove;
        } else {
            velocity.x = player.transform.position.x - transform.position.x < 0f ? -moveSpeed : moveSpeed;
            lastMove = velocity.x;
        }
        if (Physics2D.OverlapCircle(groundCheck.position, 0.02f, wallLayerMask)) {
            Vector2 dir = velocity.x < 0f ? new Vector2(-1f, 0f) : Vector2.one;
            RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, dir, 1f, wallLayerMask);
            velocity.y = raycastHit ? bigJumpForce : smallJumpForce;
        }
    }
}
