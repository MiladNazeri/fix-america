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

    void Start() 
    {
        text.SetText(GameState.Instance.CurrentBill.Item2);
    }
}
