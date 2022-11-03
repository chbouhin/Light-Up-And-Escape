using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private GameObject UIStars;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Square") {
            Instantiate(UIStars, Camera.main.WorldToScreenPoint(transform.localPosition), Quaternion.identity, GameObject.Find("StarsCount").transform);
            Destroy(gameObject);
        }
    }
}
