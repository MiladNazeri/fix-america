using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsSwapper : MonoBehaviour
{
    // Start is called before the first frame update

    public Material top;
    public Material front;

    Renderer myRender;
    void Start()
    {
        myRender = GetComponent<Renderer>();


    }

    int time =5;
    float timer;

bool yes = true;

    void SwapNat() 
    {
            if(yes)
            {
                yes = false;
                myRender.material = front;
            }else 
            {
                yes = true;
                myRender.material = top;
            }

    }

    // Update is called once per frame
    void Update()
    {

        // if(Input.GetKeyDown(KeyCode.Alpha3))
        // {
        //     SwapNat();
        // }

        timer+=Time.deltaTime;
        if(timer > time)
        {
            timer = 0;
            time = Random.Range(2, 8);
            SwapNat();
        }
    }
}
