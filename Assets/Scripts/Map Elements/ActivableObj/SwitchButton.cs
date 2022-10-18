using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour
{
    [SerializeField] private List<ActivableObj> activablesObj;
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private Sprite spriteOn;
    [SerializeField] private Sprite spriteOff;
    private InputManager inputManager;
    private bool squareIsOn = false;
    private bool mouseLightIsOn = false;
    private bool switchIsOn = false;

    private void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
    }

    private void Update()
    {
        if ((squareIsOn && inputManager.GetKeyDown("SquareInteract")) || (mouseLightIsOn && inputManager.GetKeyDown("MouseLightInteract"))) {
            switchIsOn = !switchIsOn;
            image.sprite = switchIsOn ? spriteOn : spriteOff;
            foreach (ActivableObj obj in activablesObj)
                obj.ActivateObj(switchIsOn);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Square")
            squareIsOn = true;
        else if (col.gameObject.tag == "MouseLight")
            mouseLightIsOn = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Square")
            squareIsOn = false;
        else if (col.gameObject.tag == "MouseLight")
            mouseLightIsOn = false;
    }
}
