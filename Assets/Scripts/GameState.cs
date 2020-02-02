using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class GameState: MonoBehaviour
{
    public Tuple<string, string, string> CurrentBill { get; set; }
    public static GameState Instance { get; private set; }

    private void Awake() {
        if (null == Instance) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            // Another one already exists, suicide ourselves, there can only be one
            Destroy(gameObject);
        }
    }
}