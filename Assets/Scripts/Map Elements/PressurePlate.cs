using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public List<ActivableObj> activablesObj;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject linePowerObj;
    private int nbOfCol = 0;

    private void Start()
    {
        foreach (ActivableObj activableObj in activablesObj) {
            LinePower linePower = Instantiate(linePowerObj).GetComponent<LinePower>();
            linePower.startPos = transform.position;
            linePower.endPos = activableObj.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Square" || col.gameObject.tag == "Box") {
            nbOfCol++;
            if (nbOfCol == 1) {
                animator.SetBool("isPressing", true);
                foreach (ActivableObj obj in activablesObj)
                    obj.ActivateObj(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Square" || col.gameObject.tag == "Box") {
            nbOfCol--;
            if (nbOfCol == 0) {
                animator.SetBool("isPressing", false);
                foreach (ActivableObj obj in activablesObj)
                    obj.ActivateObj(false);
            }
        }
    }
}
