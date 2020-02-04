using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameState: MonoBehaviour
{
    public Tuple<string, string, string> CurrentBill { get; set; }
    const int STARTING_POPULARITY = 75;
    public int averagePopularity = STARTING_POPULARITY;
    public int daysPlayed = 0;
    public int veryPopularBills = 0;
    public int riotBills = 0;
    const int STARTING_LIVES = 4;
    public int remainingLives = STARTING_LIVES;
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
                MusicManager.Instance.PlayGamePlayMusic(false);
            SceneManager.LoadScene("end_scene");
            break;
        }
    }

    public void Reset() {
        remainingLives = STARTING_LIVES;
        riotBills = 0;
        veryPopularBills = 0;
        averagePopularity = STARTING_POPULARITY;
        daysPlayed = 0;
        CurrentBill = null;
        Backend.Instance.Reset();
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