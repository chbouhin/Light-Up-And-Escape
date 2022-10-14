using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActivableObj : MonoBehaviour
{
    [SerializeField] protected bool reversed = false;

    public void ActivateObj(bool activate)
    {
        if (reversed)
            activate = !activate;
        if (activate)
            SetOn();
        else
            SetOff();
    }

    protected abstract void SetOn();

    protected abstract void SetOff();
}
