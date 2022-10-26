using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryChest : InteractableObj
{
    [SerializeField] private Animator animator;

    protected override void ObjAction()
    {
        animator.SetBool("open", true);
        FindObjectOfType<GameManager>().isInGame = false;
        audioManager.Play("Victory");
        GameObject.Find("Victory").GetComponent<Popup>().OpenClose(true);
        FindObjectOfType<Square>().StopMoving();
    }
}