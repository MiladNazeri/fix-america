using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRTK;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public const int SECONDS_PER_BILL = 10;
    public const int DAYS_OF_IMMUNITY = 20;
    public const int POPULARITY_AVG_LOSING_THRESHOLD = 50;
    public const float TIME_BETWEEN_BILLS_SECONDS = 1f;

    public GameObject golfBagsParent;

    public BillTextChange billTextChange;

    public GameObject veto;
    public GameObject approve;
    public GameObject desktopStamp;

    public bool forceLoseFromEditor = false;
    bool canStampNewBill = true;

    public void BillTimerIsOver()
    {
        Lose();
    }

    public void Lose(bool force=false) {
        GameState.Instance.remainingLives--;
        if (GameState.Instance.remainingLives == 0 || force) {
            
            GameState.Instance.SetState(GameState.State.End);
        } else {
            Destroy(golfBagsParent.transform.GetChild(0).gameObject);
            GetNewBill();
        }
    }

    private void Update()
    {
        if (forceLoseFromEditor)
        {
            forceLoseFromEditor = false;
            Lose(true);
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
    }


    public void SetupDesktopStamps()
    {
        Debug.Log("Setting up desktop stamps");
        veto.SetActive(false);
        approve.SetActive(false);
        desktopStamp.SetActive(true);
    }

    public void SetupVRStamps()
    {
        veto.SetActive(true);
        approve.SetActive(true);
        desktopStamp.SetActive(false);
    }

    public void GetNewBill()
    {
        StampBill.Instance.DeleteStamp();
        GameState.Instance.daysPlayed++;
        GameState.Instance.CurrentBill = Backend.Instance.GetNewBill();
        Debug.Log($"New Bill is: {GameState.Instance.CurrentBill.Item1}");

        billTextChange.SetText(GameState.Instance.CurrentBill.Item1);

        var popularity = Backend.Instance.GetBillPopularity(
            GameState.Instance.CurrentBill.Item2,
            GameState.Instance.CurrentBill.Item3
        ) * 100;
        
        Debug.Log($"Potential popularity of the bill is: {popularity}%.");
        TVController.Instance.StartTimerForSeconds(SECONDS_PER_BILL, BillTimerIsOver);
        canStampNewBill = true;
    }

    private IEnumerator WaitThenCreateNewBill()
    {
        yield return new WaitForSeconds(TIME_BETWEEN_BILLS_SECONDS);
        GetNewBill();
    }

    public void Approve()
    {
        if(canStampNewBill)
        {
canStampNewBill = false;
        Backend.Instance.ApproveBill();
        billTextChange.SetText("");
        StartCoroutine(WaitThenCreateNewBill());
        }
        
    }

    public void Veto()
    {
        if(canStampNewBill)
        {
canStampNewBill = false;
        Backend.Instance.DeclineBill();
        billTextChange.SetText("");
        StartCoroutine(WaitThenCreateNewBill());
        }
        
    }
}
