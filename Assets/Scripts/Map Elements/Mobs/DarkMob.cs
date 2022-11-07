using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMob : Mob
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform leftWallCheck;
    [SerializeField] private Transform rightWallCheck;
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
            velocity.x = player.transform.position.x - transform.position.x < 0f ? -1f : 1f;
            lastMove = velocity.x;
        }
        if (Physics2D.OverlapCircle(groundCheck.position, 0.02f, wallLayerMask)) {
            if (velocity.x == -1f)
                velocity.y = Physics2D.OverlapCircle(leftWallCheck.position, 0.2f, wallLayerMask) ? bigJumpForce : smallJumpForce;
            else
                velocity.y = Physics2D.OverlapCircle(rightWallCheck.position, 0.2f, wallLayerMask) ? bigJumpForce : smallJumpForce;
        }
    }
}
