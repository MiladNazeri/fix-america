using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TVController : MonoBehaviour
{
    public static TVController Instance { get; private set; }

    public TextMeshPro TVPopularityText;
    public TextMeshPro TVTimerText;

    private int timerEndTime = 0;
    private bool timerInProgress = false;
    public delegate void TimerCallbackDelegate();
    private TimerCallbackDelegate timerCallback;

    private void Awake() {
        if (null == Instance) {
            Instance = this;
        } else {
            // Another one already exists, suicide ourselves, there can only be one
            Destroy(gameObject);
        }
    }

    public void DisplayPopularity(int pop_pct)
    {
    	CancelTimer();
    	var text = $"POPULARITY: {pop_pct}%\n";
    	var rethoric = pop_pct > 50 ? "I'M THE BEST!" : "FAKE NUZE!";
    	var avg = $"\n{GameState.Instance.daysPlayed}-day avg: {GameState.Instance.averagePopularity}%";
    	TVTimerText.text = "";
    	TVPopularityText.text = text + rethoric + avg;
    }

    public void ShowUserGoodStuff(string s)
    {
        TVPopularityText.text = s;
    }

    public void StartTimerForSeconds(int seconds, TimerCallbackDelegate callback)
    {
    	timerCallback = callback;
    	timerEndTime = (int) (Time.realtimeSinceStartup + seconds);
    	timerInProgress = true;
    }

    public void CancelTimer()
    {
    	timerInProgress = false;
    }

    private void Update()
    {
    	if (timerInProgress)
    	{
    		var time_remaining = (int) (timerEndTime - (int) Time.realtimeSinceStartup);
    		TVTimerText.text = time_remaining.ToString();
    		TVPopularityText.text = "";
    		if (0 == time_remaining)
    		{
    			timerInProgress = false;
    			timerCallback?.Invoke();
    		}
    	}
    }
}
