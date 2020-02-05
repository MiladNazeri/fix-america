using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsSwitcher : MonoBehaviour
{

    public Transform front;
    public Transform top;
    public Camera news;

   
   bool showTop = true;

    void SwapNat() 
    {
        if(showTop)
        {
            showTop = false;
            SetPos(top);
               
        }else 
        {
            showTop = true;
            SetPos(front);
        }
    }

    void SetPos(Transform t) 
    {
        news.transform.SetParent(t, false);
        news.transform.localPosition = Vector3.zero;
        news.transform.localRotation = Quaternion.identity;
    }


    int time = 5;
    float timer;
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > time)
        {
            timer = 0;
            time = Random.Range(2, 8);
            SwapNat();
        }
    }
}
