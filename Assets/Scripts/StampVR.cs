using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampVR : MonoBehaviour
{

    public List<GameObject> deskStamps;
    public string stampType;
    public GameObject approved;
    public GameObject veto;
    bool _isGrabbed = true;

    public GameObject StampBill;
    public AudioClip clip;

    void Start()
    {
        deskStamps = new List<GameObject>();
    }

    public void ShowDeskStamp(Vector3 collisionPosition)
    {
        GameObject typeToUse;

        if (stampType == "veto")
        {
            GameManager.Instance.Veto();
            typeToUse = veto;
        }
        else
        {
            GameManager.Instance.Approve();
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

    public void SetGrabbed(bool isGrabbed)
    {
        this._isGrabbed = isGrabbed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isGrabbed)
        {
            return;
        }
        
        Debug.Log("collision for stamp");
        if (collision.gameObject.CompareTag("papers"))
        {
            Debug.Log("collision for papers");

            StampBill.GetComponent<StampBill>().SetCurrentStampType(stampType);
            StampBill.GetComponent<StampBill>().MakeNewStamp();
            //StampAudio.instance.PlayStampSound();
            transform.GetComponent<AudioSource>().PlayOneShot(clip);
        } else if (collision.gameObject.CompareTag("desk"))
        {
            Debug.Log("collision for desk");

            Vector3 deskStampPosition = collision.GetContact(0).point;
            //StampAudio.instance.PlayStampSound();
            transform.GetComponent<AudioSource>().PlayOneShot(clip);
            ShowDeskStamp(deskStampPosition);
        }
    }
}
