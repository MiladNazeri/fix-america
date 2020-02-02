using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScores: MonoBehaviour
{
    public AudioSource hailToTheChief;
    const float MAX_TIME_ON_END_SCENE_SEC = 15f;
    private float timeSceneStarted = 0f;

    private void Start()
    {
        timeSceneStarted = Time.realtimeSinceStartup;
        gameObject.GetComponent<TextMeshPro>().text = 
            $"YOU WERE IMPEACHED\nTIME IN OFFICE: {GameState.Instance?.daysPlayed} days\nAVG POPULARITY: {GameState.Instance?.averagePopularity}%\nAMAZING BILLS: {GameState.Instance?.veryPopularBills}\nRIOT-CAUSING BILLS: {GameState.Instance?.riotBills}";
    }

    public void PlayAgain() 
    {
        GameState.Instance.Reset();
         SceneManager.LoadScene("OvalOffice");
    }

    public void PlayHailToTheChielf()
    {
        hailToTheChief.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)
            || (Time.realtimeSinceStartup - timeSceneStarted) > MAX_TIME_ON_END_SCENE_SEC
        )
        {
            PlayAgain();
        }
    }
}