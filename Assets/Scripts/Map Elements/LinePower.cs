using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePower : MonoBehaviour
{
    [SerializeField] private ParticleSystem particules;
    [HideInInspector] public Vector3 startPos;
    [HideInInspector] public Vector3 endPos;
    private float speed = 5f;
    private float minTime = 5f;
    private float actualTime = 0f;

    private void Start()
    {
        transform.position = startPos;
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
}
