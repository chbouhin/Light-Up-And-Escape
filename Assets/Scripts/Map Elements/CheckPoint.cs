using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void OnTriggerEnter2D(Collider2D col)
    {
        PlayerPrefs.SetInt("CheckPoint", 1);
        animator.SetTrigger("Open");
    }
}
