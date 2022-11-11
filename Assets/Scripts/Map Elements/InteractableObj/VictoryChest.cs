using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryChest : InteractableObj
{
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem particules;

    protected override void ObjAction()
    {
        animator.SetBool("open", true);
        particules.Play(true);
        FindObjectOfType<GameManager>().Victory();
        audioManager.Play("VictoryChest");
    }
}