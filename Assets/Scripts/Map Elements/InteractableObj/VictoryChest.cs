using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryChest : InteractableObj
{
    protected override void ObjAction()
    {
        FindObjectOfType<GameManager>().isInGame = false;
        FindObjectOfType<Square>().StopMoving();
        GameObject.Find("Victory").GetComponent<Animator>().SetBool("open", true);
    }
}
