using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class GameState: MonoBehaviour
{
    public Tuple<string, string, string> CurrentBill { get; set; }
    public int averagePopularity = 75;
    public int daysPlayed = 0;
    public int veryPopularBills = 0;
    public int riotBills = 0;
    public static GameState Instance { get; private set; }

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

    private void Awake() {
        if (null == Instance) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            // Another one already exists, suicide ourselves, there can only be one
            Destroy(gameObject);
        }
    }
}