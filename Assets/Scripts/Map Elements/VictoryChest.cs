using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryChest : MonoBehaviour
{
    private InputManager inputManager;
    private Animator victory;
    private bool playerIsOn = false;

    private void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
    }

    private void Update()
    {
        if (playerIsOn && inputManager.GetKeyDown("SquareInteract")) {
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
