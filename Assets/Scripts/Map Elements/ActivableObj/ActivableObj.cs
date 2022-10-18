using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActivableObj : MonoBehaviour
{
    [SerializeField] protected bool reversed = false;
    private int nbActivation = 0;

    public void ActivateObj(bool activate)
    {
        if (activate) {
            if (nbActivation == 0) {
                if (reversed)
                    SetOff();
                else
                    SetOn();
            }
            nbActivation++;
        } else {
            nbActivation--;
            if (nbActivation == 0) {
                if (reversed)
                    SetOn();
                else
                    SetOff();
            }
        }
    }

    protected abstract void SetOn();

    protected abstract void SetOff();
}
