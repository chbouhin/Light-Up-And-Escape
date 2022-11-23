using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICoin : MonoBehaviour
{
    private CoinsCount coinsCount;
    private Vector2 destination;
    private float moveSpeed = 0.65f;
    private float sizeCol = 8f;

    private void Start()
    {
        coinsCount = transform.parent.GetComponent<CoinsCount>();
        destination = coinsCount.GetPosNewCoin();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, destination) < sizeCol) {
            coinsCount.AddNewCoin();
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime * Screen.width);
    }
}
