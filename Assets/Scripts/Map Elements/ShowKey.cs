using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowKey : MonoBehaviour
{
    [SerializeField] private TextMeshPro textKey;
    [SerializeField] private Animator animator;
    [SerializeField] private bool squareCanInteract;
    private InputManager inputManager;

    private void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        ChangeKey();
    }

    public void ChangeKey()
    {
        KeyCodes keys = squareCanInteract ? inputManager.GetKeyCodes("SquareInteract") : inputManager.GetKeyCodes("MouseLightInteract");
        textKey.text = "";
        if (keys.key1 != KeyCode.None)
            textKey.text += keys.key1.ToString();
        if (keys.key2 != KeyCode.None) {
            if (keys.key1 != KeyCode.None)
                textKey.text += " | ";
            textKey.text += keys.key2.ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (squareCanInteract) {
            if (col.gameObject.tag == "Square")
                animator.SetBool("Open", true);
        } else {
            if (col.gameObject.tag == "MouseLight")
                animator.SetBool("Open", true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (squareCanInteract) {
            if (col.gameObject.tag == "Square")
                animator.SetBool("Open", false);
        } else {
            if (col.gameObject.tag == "MouseLight")
                animator.SetBool("Open", false);
        }
    }
}
