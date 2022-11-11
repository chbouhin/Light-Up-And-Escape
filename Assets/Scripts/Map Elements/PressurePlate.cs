using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Powering powering;
    private AudioManager audioManager;
    private int nbOfCol = 0;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        nbOfCol++;
        if (nbOfCol == 1) {
            animator.SetBool("isPressing", true);
            powering.SendPower(true);
            audioManager.Play("PressurePlateOn");
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        nbOfCol--;
        if (nbOfCol == 0) {
            animator.SetBool("isPressing", false);
            powering.SendPower(false);
            audioManager.Play("PressurePlateOff");
        }
    }
}
