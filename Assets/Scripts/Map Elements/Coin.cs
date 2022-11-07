using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject UICoins;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Square") {
            Instantiate(UICoins, Camera.main.WorldToScreenPoint(transform.localPosition), Quaternion.identity, GameObject.Find("CoinsCount").transform);
            Destroy(gameObject);
        }
    }
}
