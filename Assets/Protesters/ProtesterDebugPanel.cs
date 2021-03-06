﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProtesterDebugPanel : MonoBehaviour
{
	public InputField inputField;
	public ProtesterManager manager;
	public Text numDebugText;

	public void SetAmount(){
		int num = int.Parse(inputField.text);
		numDebugText.text = manager.protesters.Count.ToString();
		manager.SetProtesterAmount(num);
	} 
}
