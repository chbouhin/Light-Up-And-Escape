using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOpenDoor : MonoBehaviour
{
    [SerializeField] private Key key;
    private Square square;

    private void Start()
    {
        square = FindObjectOfType<Square>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Door" && key.door.transform.GetInstanceID() == col.transform.parent.GetInstanceID()) {
            square.held = null;
            col.transform.parent.GetComponent<Door>().ActivateObj(true);
            Destroy(transform.parent.gameObject);
        }
    }
}
