using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State 
    {
        Title,
        Playing,
        End,
    }

    State state; 

    public void SetState() 
    {
        switch(state)
        {
            case State.Title: break;
            case State.Playing: break;
            case State.End: break;
        }
    }

    //stamp one bill, with timer, if timer hits 0 you lose
void Start () {
    GetNewBill();
}

void Update() {
    if(Input.GetKeyDown(KeyCode.Alpha1))
    {
        Approve();
    }

    if(Input.GetKeyDown(KeyCode.Alpha2))
    {
        Veto();
    }
}

    public void GetNewBill()
    {
        GameState.Instance.CurrentBill = Backend.Instance.GetNewBill();
    }

    public void Approve()
    {
        Backend.Instance.ApproveBill();
        GetNewBill();
    }

    public void Veto()
    {
        Backend.Instance.DeclineBill();
        GetNewBill();
    }
}
