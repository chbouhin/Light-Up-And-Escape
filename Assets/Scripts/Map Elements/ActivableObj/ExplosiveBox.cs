using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBox : ActivableObj
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private GameObject obj;
    [SerializeField] private string sound;
    private AudioManager audioManager;
    private bool isActive = true;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    protected override void SetOn()
    {
        if (isActive) {
            obj.SetActive(false);
            audioManager.Play(sound);
            particle.Play(true);
            isActive = false;
        }
    }

    protected override void SetOff()
    {
    }
}
