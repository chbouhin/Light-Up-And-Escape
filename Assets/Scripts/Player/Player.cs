using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [SerializeField] private Animator defeat;
    [SerializeField] protected Rigidbody2D rb;
    protected GameManager gameManager;
    protected bool isAlive = true;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    protected void Die()
    {
        if (isAlive) {
            FindObjectOfType<GameManager>().isInGame = false;
            defeat.SetBool("open", true);
            isAlive = false;
        }
    }
}
