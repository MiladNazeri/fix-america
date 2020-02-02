using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamp_Base : MonoBehaviour
{
    protected GameObject stampPosition;

    // Start is called before the first frame update
    void Start()
    {
        stampPosition = GameObject.FindGameObjectWithTag("stampPosition");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
