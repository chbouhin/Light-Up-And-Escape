using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarAnimation : MonoBehaviour
{
    private Transform destination;
    private float sizeMult = 2.5f;
    private float sizeSpeed = 1.5f;
    private float moveSpeedTime = 3f;
    private float timerMoveSpeed = 0f;
    private bool isMoving = false;

    private void Update()
    {
        if (isMoving) {
            timerMoveSpeed += Time.deltaTime;
            transform.localScale = Vector2.Lerp(transform.localScale, Vector2.one, timerMoveSpeed / moveSpeedTime);
            transform.position = Vector2.Lerp(transform.position, destination.position, timerMoveSpeed / moveSpeedTime);
            if (Vector2.Distance(transform.position, destination.position) < 0.1f) {
                destination.GetComponent<Image>().color = Color.white;
                gameObject.SetActive(false);
            }
        } else {
            transform.localScale = Vector2.Lerp(transform.localScale, Vector2.one * sizeMult, sizeSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.localScale, Vector2.one * sizeMult) < 0.1f)
                isMoving = true;
        }
    }

    public void SetDestination(Transform destination)
    {
        this.destination = destination;
    }
}
