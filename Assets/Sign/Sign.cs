using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sign : MonoBehaviour
{
    public GameObject paper;

    public enum Size 
    {
        Small, Medium, Large
    }

    public List<Vector3> textSizes;
    public List<Vector3> paperSizes;

    public TextMeshPro text;
	public Transform board;

	public float startTextChance = 0.5f;
	public float endTextChance = 0.5f;
	public float minSize = 0.9f;
	public float maxSize = 2.0f;



    public void SetSize () 
    {

    }
    
    public void SetText()
    {
        
    }

    string[] startText = new string[]
    {
        "We are all",
        "We love",
        "Free",
        "Our sons are",
        "Protect the",
        "Feed the",
        "What about the",
        "More",
        "More Rights for",
        "Our daughters are",
		"More Money for ",
		"Our Future: ",
		"We want more ",
		"I want ",
		"Justice for "
    };


    string[] endText = new string[]
    {
        "Now!",
        "ASAP",
        "forever",
        "for everyone",
        "together",
		"for us"
    };


    void Start() 
    {
        int i = Random.Range(0, startText.Length);
		int b = Random.Range(0, endText.Length);
		string o = "";
		if(Random.value > startTextChance){
			o += startText[i];
			o += " ";
		}
		o += GameState.Instance.CurrentBill.Item2;
		if(Random.value > endTextChance){
			o += " ";
			o += endText[b];
		}

        text.SetText(o);
		//randomization
		float randomSize = Mathf.Lerp(minSize, maxSize, Random.value);
		board.transform.localScale = new Vector3(randomSize, randomSize, randomSize);

    }
}
