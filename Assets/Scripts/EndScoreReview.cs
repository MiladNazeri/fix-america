using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using System.Collections;

public class EndScoreReview: MonoBehaviour {
    public TextMeshPro Text;
    private void Start()
    {
        StartCoroutine(ReviewCoroutine());
    }

    public IEnumerator ReviewCoroutine()
    {
        foreach(var bill in Backend.Instance.allDrawnBills) {
            Text.text = bill;
            yield return new WaitForSeconds(3);
        }
    }
}