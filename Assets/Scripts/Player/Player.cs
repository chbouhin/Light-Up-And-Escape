using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [SerializeField] protected InputManager inputManager;
    [SerializeField] private Popup defeat;
    [SerializeField] protected Rigidbody2D rb;
    protected GameManager gameManager;
    protected bool isAlive = true;
    protected AudioManager audioManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    protected void Die()
    {
        if (isAlive) {
            FindObjectOfType<GameManager>().isInGame = false;
            audioManager.Play("Loose");
            defeat.OpenClose(true);
            isAlive = false;
        }
    }
}
