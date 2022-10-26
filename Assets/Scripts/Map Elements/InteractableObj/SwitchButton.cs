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
    [SerializeField] private GameObject linePowerObj;
    private List<LinePower> linePowers = new List<LinePower>();
    private bool switchIsOn = false;

    private void Start()
    {
        foreach (ActivableObj activableObj in activablesObj) {
            LinePower linePower = Instantiate(linePowerObj).GetComponent<LinePower>();
            linePower.startPos = transform.position;
            linePower.endPos = activableObj.transform.position;
            linePowers.Add(linePower);
        }
    }

    protected override void ObjAction()
    {
        switchIsOn = !switchIsOn;
        image.sprite = switchIsOn ? spriteOn : spriteOff;
        foreach (ActivableObj obj in activablesObj)
            obj.ActivateObj(switchIsOn);
        foreach (LinePower linePower in linePowers)
            linePower.SendPower(switchIsOn);
    }
}
