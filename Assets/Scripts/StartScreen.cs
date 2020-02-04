using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public AudioSource intro;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void GoGameScreen() 
    {
        intro.Stop();
        SceneManager.LoadScene("OvalOffice");

    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collision with start button: {other.gameObject.name}");
        if (other.gameObject.name == "[VRTK][AUTOGEN][Controller][NearTouch][CollidersContainer]")
        {
            intro.Stop();
            SceneManager.LoadScene("OvalOffice");
        }
    }
}
