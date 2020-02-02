using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampBill : MonoBehaviour
{
    public static StampBill Instance { get; private set; }
    public string currentStampType;
    public GameObject newStamp;
    public GameObject approved;
    public GameObject veto;
    public List<GameObject> currentStamp = new List<GameObject>();
    public GameObject stampPosition;
    // Start is called before the first frame update
    void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            stampPosition = GameObject.FindGameObjectWithTag("stampPosition");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCurrentStampType(string newStampType)
    {
        currentStampType = newStampType;
    }

    public void MakeNewStamp()
    {
        GameObject typeToUse = null;
 
        if (currentStampType == "veto")
        {
            GameManager.Instance.Veto();
            typeToUse = veto;
        } else
        {
            GameManager.Instance.Approve();
            typeToUse = approved;
        }

        GameObject f = (GameObject)(Instantiate(typeToUse, stampPosition.transform.position, Quaternion.Euler(90, 0, 0)));
        currentStamp.Add(f);
    }

    public void DeleteStamp()
    {
        Debug.Log("destroying paper stamp");
        foreach(GameObject g in currentStamp)
        {
            DestroyImmediate(g);
        }
    }

}
