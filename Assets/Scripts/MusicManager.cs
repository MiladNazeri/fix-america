using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource gameplayMusic;
    // Start is called before the first frame update
    public static MusicManager Instance { get; private set; }

    private void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Another one already exists, suicide ourselves, there can only be one
            Destroy(gameObject);
        }
    }

    public void PlayGamePlayMusic()
    {
        gameplayMusic.Play();
    }
}
