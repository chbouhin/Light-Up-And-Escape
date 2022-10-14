using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryChest : MonoBehaviour
{
    private Animator victory;
    private bool playerIsOn = false;

    private void Update()
    {
        if (playerIsOn && Input.GetButtonDown("Interact")) {
            FindObjectOfType<GameManager>().isInGame = false;
            FindObjectOfType<Square>().StopMoving();
            victory = GameObject.Find("Victory").GetComponent<Animator>();
            victory.SetBool("open", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Square")
            playerIsOn = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Square")
            playerIsOn = false;
    }
}
