using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Held : InteractableObj
{
    private Transform parent;
    protected Square square;
    private bool grab = false;
    private float animSpeed = 0.1f; // 0 to 1
    [SerializeField] private float sizeWhenGrab = 0.4f; // 0 to 1

    protected override void Start()
    {
        base.Start();
        parent = transform.parent;
        square = FindObjectOfType<Square>();
    }

    protected override void Update()
    {
        base.Update();
        if (grab) {
            transform.localPosition = Vector2.Lerp(transform.localPosition, Vector2.zero, animSpeed);
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(sizeWhenGrab, sizeWhenGrab), animSpeed);
        } else {
            transform.localScale = Vector2.Lerp(transform.localScale, Vector2.one, animSpeed);
        }
    }

    protected virtual void IsGrab()
    {
        grab = true;
        transform.SetParent(square.transform);
    }

    public virtual void IsThrow()
    {
        grab = false;
        transform.SetParent(parent);
    }

    protected override void ObjAction()
    {
        if (square.HoldObj(this))
            IsGrab();
    }
}
