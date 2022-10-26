using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Held : MonoBehaviour
{
    [SerializeField] private float sizeWhenGrab = 0.4f; // 0 to 1
    protected Square square;
    private Transform parent;
    private bool grab = false;
    private float animSpeed = 0.1f; // 0 to 1
    int instanceId;

    private void Start()
    {
        parent = transform.parent;
        square = FindObjectOfType<Square>();
        instanceId = transform.GetInstanceID();
    }

    private void Update()
    {
        if (grab) {
            transform.localPosition = Vector2.Lerp(transform.localPosition, Vector2.zero, animSpeed);
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(sizeWhenGrab, sizeWhenGrab), animSpeed);
        } else {
            transform.localScale = Vector2.Lerp(transform.localScale, Vector2.one, animSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Square")
            square.HeldObjOnTriggerEnter(instanceId, this);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Square")
            square.HeldObjOnTriggerExit(instanceId);
    }

    public virtual void IsGrab()
    {
        grab = true;
        transform.SetParent(square.transform);
    }

    public virtual void IsThrow()
    {
        grab = false;
        transform.SetParent(parent);
    }
}
