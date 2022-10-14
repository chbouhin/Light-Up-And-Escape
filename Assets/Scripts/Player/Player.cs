using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [SerializeField] private Animator defeat;
    protected bool canMove = false;

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    protected void Die()
    {
        defeat.SetBool("open", true);
    }
}
