using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GoGameScreen() 
    {
        SceneManager.LoadScene("OvalOffice");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
