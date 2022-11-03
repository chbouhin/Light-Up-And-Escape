using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : InteractableObj
{
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private Sprite spriteOn;
    [SerializeField] private Sprite spriteOff;
    [SerializeField] private Powering powering;
    private bool switchIsOn = false;

    protected override void ObjAction()
    {
        switchIsOn = !switchIsOn;
        image.sprite = switchIsOn ? spriteOn : spriteOff;
        powering.SendPower(switchIsOn);
    }
}
