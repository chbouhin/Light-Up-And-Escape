using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : ActivableObj
{
    [SerializeField] private GameObject lampOn;
    [SerializeField] private GameObject lampOff;

    protected override void SetOn()
    {
        lampOn.SetActive(true);
        lampOff.SetActive(false);
    }

    protected override void SetOff()
    {
        lampOn.SetActive(false);
        lampOff.SetActive(true);
    }
}
