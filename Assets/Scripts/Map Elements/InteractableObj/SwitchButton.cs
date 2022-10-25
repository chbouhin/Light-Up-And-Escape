using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : InteractableObj
{
    public List<ActivableObj> activablesObj;
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private Sprite spriteOn;
    [SerializeField] private Sprite spriteOff;
    private bool switchIsOn = false;

    protected override void ObjAction()
    {
        switchIsOn = !switchIsOn;
        image.sprite = switchIsOn ? spriteOn : spriteOff;
        foreach (ActivableObj obj in activablesObj)
            obj.ActivateObj(switchIsOn);
    }
}
