using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObj : MonoBehaviour
{
    [SerializeField] private bool squareCanInteract = true;
    [SerializeField] private bool mouseLightCanInteract = true;
    private InputManager inputManager;
    private bool squareIsTouching = false;
    private bool mouseLightIsTouching = false;

    protected virtual void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
    }

    protected virtual void Update()
    {
        if (squareCanInteract && squareIsTouching && inputManager.GetKeyDown("SquareInteract"))
            ObjAction();
        if (mouseLightCanInteract && mouseLightIsTouching && inputManager.GetKeyDown("MouseLightInteract"))
            ObjAction();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (squareCanInteract && col.gameObject.tag == "Square")
            squareIsTouching = true;
        else if (mouseLightCanInteract && col.gameObject.tag == "MouseLight")
            mouseLightIsTouching = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (squareCanInteract && col.gameObject.tag == "Square")
            squareIsTouching = false;
        else if (mouseLightCanInteract && col.gameObject.tag == "MouseLight")
            mouseLightIsTouching = false;
    }

    protected abstract void ObjAction();
}
