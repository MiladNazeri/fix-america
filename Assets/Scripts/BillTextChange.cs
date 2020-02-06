using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BillTextChange : MonoBehaviour
{
    public TextMeshPro text;

    public void SetText(string t)
    {
        text.SetText(t);
    }

}
