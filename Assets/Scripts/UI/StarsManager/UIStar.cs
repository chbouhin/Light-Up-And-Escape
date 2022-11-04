using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStar : MonoBehaviour
{
    private StarsCount starsCount;
    private Vector2 destination;
    private float moveSpeed = 650f;
    private float sizeCol = 8f;

    private void Start()
    {
        starsCount = transform.parent.GetComponent<StarsCount>();
        destination = starsCount.GetPosNewStar();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, destination) < sizeCol) {
            starsCount.AddNewStar();
            Destroy(gameObject);
        }
    }
}
