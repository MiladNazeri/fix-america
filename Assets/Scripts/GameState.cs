using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameState: MonoBehaviour
{
    public Tuple<string, string, string> CurrentBill { get; set; }
    public int averagePopularity = 75;
    public int daysPlayed = 0;
    public int veryPopularBills = 0;
    public int riotBills = 0;
    public int remainingLives = 4;
    public static GameState Instance { get; private set; }

    public enum State
    {
        Title,
        Playing,
        End,
    }

    State state;

    public void SetState(State state) 
    {
        switch(state)
        {
            case State.Title: break;
            case State.Playing: 
            SceneManager.LoadScene("OvalOffice");
            break;
            case State.End: 
            SceneManager.LoadScene("end_scene");
            break;
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