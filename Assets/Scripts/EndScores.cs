using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScores: MonoBehaviour
{
    public AudioSource hailToTheChief;
    private void Start()
    {
        gameObject.GetComponent<TextMeshPro>().text = 
            $"YOU WERE IMPEACHED\nTIME IN OFFICE: {GameState.Instance?.daysPlayed} days\nAVG POPULARITY: {GameState.Instance?.averagePopularity}%\nAMAZING BILLS: {GameState.Instance?.veryPopularBills}\nRIOT-CAUSING BILLS: {GameState.Instance?.riotBills}";
    }

    public void PlayAgain() 
    {
         SceneManager.LoadScene("OvalOffice");
    }

    public void PlayHailToTheChielf()
    {
        hailToTheChief.Play();
    }
}