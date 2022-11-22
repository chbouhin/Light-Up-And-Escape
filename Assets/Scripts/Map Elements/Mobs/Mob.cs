using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mob : MonoBehaviour
{
    [SerializeField] private string playerTag;
    [SerializeField] private Animator animator;
    [SerializeField] protected LayerMask wallLayerMask;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] private Transform topPlayerDetect;
    [SerializeField] private List<string> deathSounds;
    [SerializeField] private ParticleSystem normalDeathParticules;
    [SerializeField] private ParticleSystem fallDeathParticules;
    [SerializeField] private GameObject skin;
    protected Player player;
    protected Vector2 velocity = new Vector2();
    protected float moveSpeed = 1.75f;
    protected bool isIdle = true;
    protected float stopDetectTimer = 0f;
    private float rangeDetection = 5f;
    private float timeStopDetect = 1.5f;
    protected AudioManager audioManager;

    private void Start()
    {
        player = GetPlayer();
        audioManager = FindObjectOfType<AudioManager>();
    }

    protected abstract Player GetPlayer();

    protected virtual void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > rangeDetection || (Physics2D.Linecast(transform.position, player.transform.position, wallLayerMask) && Physics2D.Linecast(topPlayerDetect.position, player.transform.position, wallLayerMask))) {
            if (stopDetectTimer >= timeStopDetect)
                isIdle = true;
            stopDetectTimer += Time.deltaTime;
        } else {
            stopDetectTimer = 0f;
            isIdle = false;
        }
        animator.SetBool("isIdle", isIdle);
        if (transform.position.y < -6f) {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            Die(fallDeathParticules);
        }
    }

    private void OnCollisionEnter2D(Collision2D  col)
    {
        if (col.gameObject.tag == playerTag)
            player.Die();
    }

    private void FixedUpdate()
    {
        if (!isIdle)
            rb.velocity = velocity;
        else
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(0f, rb.velocity.y), Time.deltaTime * 2);
    }

    public void Die(ParticleSystem deathParticleSystem = null)
    {
        audioManager.Play(deathSounds[Random.Range(0, deathSounds.Count)]);
        skin.SetActive(false);
        if (deathParticleSystem)
            deathParticleSystem.Play(true);
        else
            normalDeathParticules.Play(true);
    }
}
