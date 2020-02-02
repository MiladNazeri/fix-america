using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class EndScores: MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<TextMeshPro>().text = 
            $"YOU WERE IMPEACHED\nTIME IN OFFICE: {GameState.Instance?.daysPlayed} days\nAVG POPULARITY: {GameState.Instance?.averagePopularity}%\nAMAZING BILLS: {GameState.Instance?.veryPopularBills}\nRIOT-CAUSING BILLS: {GameState.Instance?.riotBills}";
    }
}