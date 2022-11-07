using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [SerializeField] protected InputManager inputManager;
    [SerializeField] protected Rigidbody2D rb;
    protected GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Die()
    {
        if (gameManager.isInGame)
            gameManager.Lose();
    }
}
