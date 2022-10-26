using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Held
{
    [SerializeField] private Animator animator;
    public Door door;

    // Cant have more than 8 keys in a level (for colors generation)
    public override void IsGrab()
    {
        base.IsGrab();
        animator.enabled = false;
        transform.GetChild(0).localPosition = Vector2.zero;
        transform.GetChild(1).gameObject.SetActive(true);
    }

    public override void IsThrow()
    {
        base.IsThrow();
        animator.enabled = true;
        transform.GetChild(1).gameObject.SetActive(false);
    }
}
