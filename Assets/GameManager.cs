﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public const int SECONDS_PER_BILL = 10;
    public const float TIME_BETWEEN_BILLS_SECONDS = 3f;

    public BillTextChange billTextChange;

    public int remainingLives = 10;


    public void BillTimerIsOver()
    {
        Lose();
    }

    public void Lose() {
        remainingLives--;
        if (remainingLives == 0) {
            SceneManager.LoadScene("end_scene");
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

    private void Start()
    {
        GetNewBill();
        SetDesktopOrVRStamps();
    }

    void Update() {

    }

    public void SetDesktopOrVRStamps()
    {
        VRTK_SDKSetup vrtkSetup = VRTK_SDKManager.instance.loadedSetup;

        if (vrtkSetup == null)
        {
            GameObject[] vrStamps = GameObject.FindGameObjectsWithTag("VR_Stamps");
            foreach(GameObject stamp in vrStamps)
            {
                stamp.gameObject.SetActive(false);
            }
            Debug.Log("No setup is loaded");
        }
        else
        {
            GameObject desktopStamp = GameObject.FindGameObjectWithTag("Desktop_Stamp");
            desktopStamp.gameObject.SetActive(false);
        }
    }

    public void GetNewBill()
    {
        StampBill.Instance.DeleteStamp();
        GameState.Instance.CurrentBill = Backend.Instance.GetNewBill();
        Debug.Log($"New Bill is: {GameState.Instance.CurrentBill.Item1}");

        billTextChange.SetText(GameState.Instance.CurrentBill.Item1);

        var popularity = Backend.Instance.GetBillPopularity(
            GameState.Instance.CurrentBill.Item2,
            GameState.Instance.CurrentBill.Item3
        ) * 100;
        
        Debug.Log($"Potential popularity of the bill is: {popularity}%.");
        TVController.Instance.StartTimerForSeconds(SECONDS_PER_BILL, BillTimerIsOver);
    }

    private IEnumerator WaitThenCreateNewBill()
    {
        yield return new WaitForSeconds(TIME_BETWEEN_BILLS_SECONDS);
        GetNewBill();
    }

    public void Approve()
    {
        Backend.Instance.ApproveBill();
        StartCoroutine(WaitThenCreateNewBill());
    }

    public void Veto()
    {
        Backend.Instance.DeclineBill();
        StartCoroutine(WaitThenCreateNewBill());
    }
}
