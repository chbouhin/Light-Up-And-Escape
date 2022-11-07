using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMob : Mob
{
    private Vector2 lastMove;
    private float weight = 0.2f;

    protected override Player GetPlayer()
    {
        return FindObjectOfType<MouseLight>();
    }

    protected override void Update()
    {
        base.Update();
        rb.gravityScale = isIdle ? weight : 0f;
        if (stopDetectTimer > 0f) {
            velocity = lastMove;
        } else {
            velocity = (Vector2)Vector3.Normalize(player.transform.position - transform.position) * moveSpeed;
            lastMove = velocity;
        }
    }
}
