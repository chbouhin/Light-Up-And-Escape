using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powering : MonoBehaviour
{
    [SerializeField] private GameObject linePowerObj;
    public List<ActivableObj> activablesObj;
    private List<LinePower> linePowers = new List<LinePower>();

    private void Start()
    {
        foreach (ActivableObj activableObj in activablesObj) {
            LinePower linePower = Instantiate(linePowerObj).GetComponent<LinePower>();
            linePower.startPos = transform.position;
            linePower.endPos = activableObj.transform.position;
            linePowers.Add(linePower);
        }
    }

    public void SendPower(bool isSending)
    {
        foreach (LinePower linePower in linePowers)
            linePower.SendPower(isSending);
        foreach (ActivableObj obj in activablesObj)
            obj.ActivateObj(isSending);
    }
}
