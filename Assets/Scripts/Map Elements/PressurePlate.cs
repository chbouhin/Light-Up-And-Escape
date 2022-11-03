using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Powering powering;
    private int nbOfCol = 0;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Square" || col.gameObject.tag == "Box") {
            nbOfCol++;
            if (nbOfCol == 1) {
                animator.SetBool("isPressing", true);
                powering.SendPower(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Square" || col.gameObject.tag == "Box") {
            nbOfCol--;
            if (nbOfCol == 0) {
                animator.SetBool("isPressing", false);
                powering.SendPower(false);
            }
        }
    }
}
