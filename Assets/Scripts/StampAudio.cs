﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampAudio : MonoBehaviour
{
    public AudioSource stampSound;
    public static StampAudio instance;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    public void PlayStampSound()
    {
        stampSound.Play();
    }
}
