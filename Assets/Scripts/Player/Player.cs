using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [SerializeField] protected InputManager inputManager;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] private List<string> deathSounds;
    [SerializeField] private ParticleSystem normalDeathParticules;
    [SerializeField] private GameObject skin;
    protected GameManager gameManager;
    protected AudioManager audioManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void Die(ParticleSystem deathParticleSystem = null)
    {
        if (gameManager.isInGame) {
            audioManager.Play(deathSounds[Random.Range(0, deathSounds.Count)]);
            skin.SetActive(false);
            if (deathParticleSystem)
                deathParticleSystem.Play(true);
            else
                normalDeathParticules.Play(true);
            gameManager.Lose();
        }
    }
}
