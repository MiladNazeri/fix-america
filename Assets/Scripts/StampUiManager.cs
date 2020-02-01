using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampUiManager : MonoBehaviour
{
    public GameObject approved;
    public GameObject veto;
    public GameObject currentStamp;
    public GameObject newStamp;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onVeto()
    {
        Debug.Log("on veto pressed");
        currentStamp = veto;
        ShowStamp(false);
        StampMove();
    }

    public void onSign()
    {
        Debug.Log("on sign pressed");
        currentStamp = approved;
        ShowStamp(false);
        StampMove();
    }

    public void StampMove()
    {
        Animator stampAnimation = gameObject.GetComponent<Animator>();

        stampAnimation.Play("StampAnimations");
        StampAudio.instance.PlayStampSound();
        ShowStamp(true);
    }

    public void ShowStamp(bool shouldShow) {
        if (shouldShow)
        {
            Debug.Log("should show");
            newStamp = Instantiate(currentStamp, currentStamp.transform.position, Quaternion.Euler(90, 0, 0));
        } else
        {
            Destroy(newStamp);
        }
    }
}
