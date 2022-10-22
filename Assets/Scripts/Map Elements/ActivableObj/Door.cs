using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ActivableObj
{
    [SerializeField] private Animator animator;

    protected override void SetOn()
    {
        animator.SetBool("open", true);
    }

    protected override void SetOff()
    {
        animator.SetBool("open", false);
    }
}
