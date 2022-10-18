using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObj : MonoBehaviour
{
    protected InputManager inputManager;
    private bool playerIsTouching = false;

    protected virtual void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
    }

    protected virtual void Update()
    {
        if (playerIsTouching && inputManager.GetKeyDown("SquareInteract"))
            ObjAction();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Square")
            playerIsTouching = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Square")
            playerIsTouching = false;
    }

    protected abstract void ObjAction();
}
