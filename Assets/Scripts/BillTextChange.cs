using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BillTextChange : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshPro text;

    public void SetText(string t)
    {
        text.SetText(t);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
