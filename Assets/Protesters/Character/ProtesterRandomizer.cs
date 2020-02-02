using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtesterRandomizer : MonoBehaviour
{
	public Renderer headRenderer;
	public Renderer leftHandRenderer;
	public Renderer rightHandRenderer;
	public Renderer BodyRenderer;

	static int globalSeed = 0;

    void Start()
    {
		globalSeed += 1;
		Random.InitState(globalSeed);
		//Debug.Log("global seed is " + globalSeed.ToString());
		//global
		Color skinColor = new Color(1.0f,0.0f,1.0f,1.0f);
		float skinIntensity = Random.value * 0.5f; //max 0.5 because otherwise people get too yellow
		float skinBrightness = Random.value;
		skinColor = Color.HSVToRGB(0.16f, skinIntensity, skinBrightness);

		//HEAD
		headRenderer.material.SetColor("_SkinColor", skinColor);
		headRenderer.material.SetColor("_HairColor" , RandomColor());
		headRenderer.material.SetFloat("_FaceVariant", Random.Range(0,7));
		headRenderer.material.SetFloat("_FaceExpression", Random.Range(0,7));

		//HANDS
		leftHandRenderer.material.SetColor("_SkinColor", skinColor);
		rightHandRenderer.material.SetColor("_SkinColor", skinColor);

		//BODY
		BodyRenderer.material.SetColor("_SkinColor", skinColor);
		BodyRenderer.material.SetFloat("_Variant", Random.Range(0,7));
		BodyRenderer.material.SetColor("_ColorA" , RandomColor());
		BodyRenderer.material.SetColor("_ColorA" , RandomColor());
		BodyRenderer.material.SetColor("_ColorA" , RandomColor());
    }

	Color RandomColor(){
		float r = Random.value;
		float g = Random.value;
		float b = Random.value;
		return new Color(r, g, b);
	}

}

