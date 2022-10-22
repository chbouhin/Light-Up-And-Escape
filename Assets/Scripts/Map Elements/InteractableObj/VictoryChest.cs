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
        FindObjectOfType<Square>().StopMoving();
        GameObject.Find("Victory").GetComponent<Animator>().SetBool("open", true);
    }
}
