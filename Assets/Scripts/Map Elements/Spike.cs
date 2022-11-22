using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        Mob mob = col.transform.GetComponent<Mob>();
        if (mob)
            mob.Die();
        else
            col.transform.GetComponent<Player>().Die();
    }
}
