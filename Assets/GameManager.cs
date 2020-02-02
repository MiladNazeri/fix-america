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
    public void GetNewBill()
    {
        GameState.Instance.CurrentBill = Backend.Instance.GetNewBill();
    }

    public void Approve()
    {
        Backend.Instance.ApproveBill();
    }

    public void Veto()
    {
        Backend.Instance.DeclineBill();
    }
}
