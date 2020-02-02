using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampUiManager : MonoBehaviour
{
    public GameObject approved;
    public GameObject veto;
    public GameObject currentStamp;
    public GameObject newStamp;
    public GameObject stampPosition;
    public List<GameObject> deskStamps;
    public string StampType;
    // Start is called before the first frame update
    void Start()
    {
        deskStamps = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onVeto()
    {
        Debug.Log("on veto pressed");
        currentStamp = veto;
        ShowPaperStamp(false);
        StampMove();
        GameManager.Instance.Veto();
    }

    public void onSign()
    {
        Debug.Log("on sign pressed");
        currentStamp = approved;
        ShowPaperStamp(false);
        StampMove();
        GameManager.Instance.Approve();
    }

    public void StampMove()
    {
        Animator stampAnimation = gameObject.GetComponent<Animator>();

        stampAnimation.Play("StampAnimations");
        StampAudio.instance.PlayStampSound();
        ShowPaperStamp(true);
    }

    public void ShowPaperStamp(bool shouldShow) {
        if (shouldShow)
        {
            Debug.Log("should show");
            newStamp = Instantiate(currentStamp, stampPosition.transform.position, Quaternion.Euler(90, 0, 0));
        } else
        {
            Debug.Log("destroying paper stamp");
            Destroy(newStamp);
        }
    }

    public void ShowDeskStamp(string type, Vector3 collisionPosition)
    {
        GameObject typeToUse;
        if (type == "veto")
        {
            typeToUse = veto;
        } else
        {
            typeToUse = approved;
        }
        GameObject go = Instantiate(typeToUse, collisionPosition, Quaternion.Euler(90, 0, 0));
        deskStamps.Add(go);
    }

    public void DeleteDeskStamps()
    {
        foreach (GameObject stamp in deskStamps)
        {
            Destroy(stamp);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision for stamp");
        if (collision.gameObject.CompareTag("papers"))
        {
            Debug.Log("collision for papers");
            if (StampType == "veto")
            {
                onVeto();
            } else
            {
                onSign();
            }
        } else if (collision.gameObject.CompareTag("desk"))
        {
            Debug.Log("collision for desk");

            Vector3 position = collision.GetContact(0).point;
            StampAudio.instance.PlayStampSound();

            if (StampType == "veto")
            {
                ShowDeskStamp("veto", position);
            }
            else
            {
                ShowDeskStamp("approve", position);
            }
        }
    }
}
