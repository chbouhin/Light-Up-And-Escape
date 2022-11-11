using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ActivableObj
{
    [SerializeField] private Animator animator;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    protected override void SetOn()
    {
        animator.SetBool("open", true);
        audioManager.Play("DoorOpen");
    }

    protected override void SetOff()
    {
        animator.SetBool("open", false);
        audioManager.Play("DoorClose");
    }
}
