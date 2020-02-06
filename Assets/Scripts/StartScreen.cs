using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public AudioSource intro;

    public void GoGameScreen() 
    {
        intro.Stop();
        SceneManager.LoadScene("OvalOffice");

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
