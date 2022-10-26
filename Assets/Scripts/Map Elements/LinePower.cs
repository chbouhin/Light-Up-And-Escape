using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePower : MonoBehaviour
{
    [SerializeField] private ParticleSystem particules;
    [SerializeField] private Color isOnColor1;
    [SerializeField] private Color isOnColor2;
    [SerializeField] private Color isOffColor1;
    [SerializeField] private Color isOffColor2;
    [HideInInspector] public Vector3 startPos;
    [HideInInspector] public Vector3 endPos;
    private float speed = 5f;
    private float minTime = 5f;
    private float actualTime = 0f;

    private void Start()
    {
        transform.position = startPos;
        ParticleSystem.MainModule psMain = particules.main;
        psMain.startColor = new ParticleSystem.MinMaxGradient(isOffColor1, isOffColor2);
    }

    private void Update()
    {
        actualTime += Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
        if (transform.position == endPos)
            if (actualTime < minTime) {
                particules.Stop();
            } else {
                particules.Play(true);
                transform.position = startPos;
                actualTime = 0f;
            }
    }

    public void SendPower(bool isSending)
    {
        if (isSending) {
            ParticleSystem.MainModule psMain = particules.main;
            psMain.startColor = new ParticleSystem.MinMaxGradient(isOnColor1, isOnColor2);
        } else {
            ParticleSystem.MainModule psMain = particules.main;
            psMain.startColor = new ParticleSystem.MinMaxGradient(isOffColor1, isOffColor2);
        }
    }
}
