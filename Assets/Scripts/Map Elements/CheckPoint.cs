using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private AudioManager audioManager;
    private bool isOn = false;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!isOn) {
            PlayerPrefs.SetInt("CheckPoint", 1);
            animator.SetTrigger("Open");
            audioManager.Play("CheckPointSound");
            isOn = true;
        }
    }
}
