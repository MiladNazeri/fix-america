using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sign : MonoBehaviour
{
    public GameObject paper;

    public enum Size 
    {
        Small, Medium, Large
    }

    public List<Vector3> textSizes;
    public List<Vector3> paperSizes;

    public TextMeshPro text;

    public void SetSize () 
    {

    }
    
    public void SetText()
    {
        
    }

    string[] rioters = new string[]
    {
        "We are all",
        "We love",
        "Free",
        "My son is a",
        "Protect the",
        "Feed the",
        "What about the",
        "More",
        "More Rights for",
        "My daugther is a"
    };

    void Start() 
    {
        int i = Random.Range(0, rioters.Length);

        text.SetText(rioters[i] + " " + GameState.Instance.CurrentBill.Item2);
    }
}
