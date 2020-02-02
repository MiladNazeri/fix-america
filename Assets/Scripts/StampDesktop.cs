using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampDesktop : MonoBehaviour
{

    public static StampDesktop Instance { get; private set; }
    public AudioClip audioclip;


    private void Awake() {
        if (null == Instance) {
            Instance = this;
        } else {
            // Another one already exists, suicide ourselves, there can only be one
            Destroy(gameObject);
        }
    }

    public GameObject StampBill;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            onSign();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            onVeto();
        }
    }

    public void onVeto()
    {
        Debug.Log("on veto pressed");
        StampBill.GetComponent<StampBill>().SetCurrentStampType("veto");
        StampBill.GetComponent<StampBill>().DeleteStamp();
        StampMove();
    }

    public void onSign()
    {
        Debug.Log("on sign pressed");
        StampBill.GetComponent<StampBill>().SetCurrentStampType("approved");
        StampBill.GetComponent<StampBill>().DeleteStamp();
        StampMove();
    }

    public void StampMove()
    {
        Animator stampAnimation = gameObject.GetComponent<Animator>();
        stampAnimation.Play("StampAnimations");
        StartCoroutine(MakeStamp());
    }

    public IEnumerator MakeStamp()
    {
        yield return new WaitForSeconds(0.09f);
        StampBill.GetComponent<StampBill>().MakeNewStamp();
        StampAudio.instance?.PlayStampSound();
        transform.GetComponent<AudioSource>().PlayOneShot(audioclip);

    }
}
