using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TwitterPhoneController: MonoBehaviour
{
	public static TwitterPhoneController Instance { get; private set; }

	public TextMeshPro tweetsRemaining;
	public GameObject ThumbUp;
	public GameObject ThumbDown;
	public GameObject Shrug;

	private int timerEndTime = 0;
	private bool timerInProgress = false;

	public bool forceTweetFromEditor = false;

	private int remainingTweets = 5;

	const string HANDS_NAME = "[VRTK][AUTOGEN][Controller][NearTouch][CollidersContainer]";

	public AudioSource tweetSound;

	enum TwitterDisplays {
		NONE,
		THUMB_UP,
		THUMB_DOWN,
		SORRY,
	}

	private TwitterDisplays currentDisplay;

	private void Awake() {
		if (null == Instance) {
			Instance = this;
		} else {
			// Another one already exists, suicide ourselves, there can only be one
			Destroy(gameObject);
		}
	}

	public void Tweet()
	{
		Debug.Log($"remainingTweets: {remainingTweets}");
		if (remainingTweets <= 0)
		{
			currentDisplay = TwitterDisplays.SORRY;
			return;
		}
		tweetSound.Play();
		remainingTweets--;
		updateTweetsCount();
		var popularity = Backend.Instance.GetCurrentBillPopularity();
		if (popularity >= 0.5f) {
			currentDisplay = TwitterDisplays.THUMB_UP;
		} else {
			currentDisplay = TwitterDisplays.THUMB_DOWN;
		}
		StartTimerForSeconds(5);
	}

	private void updateTweetsCount()
	{
		tweetsRemaining.text = $"REMAINING TWEETS: {remainingTweets}";
	}

	private void StartTimerForSeconds(int seconds)
	{
		timerEndTime = (int) (Time.realtimeSinceStartup + seconds);
		timerInProgress = true;
	}

	public void CancelTimer()
	{
		timerInProgress = false;
	}

	public void OnTriggerEnter(Collider other) {
		Debug.Log($"Collision with phone: {other.gameObject.name}");
		if (other.gameObject.name == HANDS_NAME)
		{
			Debug.Log("Touched by a hand!");
			Tweet();
		}
	}

	private void Update()
	{
		if (forceTweetFromEditor)
		{
			forceTweetFromEditor = false;
			Tweet();
		}
		if (timerInProgress)
		{
			var time_remaining = (int) (timerEndTime - (int) Time.realtimeSinceStartup);
			if (0 == time_remaining)
			{
				timerInProgress = false;
				currentDisplay = TwitterDisplays.NONE;
			}
		}
		switch(currentDisplay) {
			case TwitterDisplays.NONE:
				ThumbUp.SetActive(false);
				ThumbDown.SetActive(false);
				Shrug.SetActive(false);
			break;
			case TwitterDisplays.THUMB_UP:
				ThumbUp.SetActive(true);
				ThumbDown.SetActive(false);
				Shrug.SetActive(false);
			break;
			case TwitterDisplays.THUMB_DOWN:
				ThumbUp.SetActive(false);
				ThumbDown.SetActive(true);
				Shrug.SetActive(false);
			break;
			case TwitterDisplays.SORRY:
				ThumbUp.SetActive(false);
				ThumbDown.SetActive(false);
				Shrug.SetActive(true);
			break;
		}
	}
}
