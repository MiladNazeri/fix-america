using UnityEngine;
using System;
using System.Collections.Generic;

public class ClickCollider: MonoBehaviour
{
	private void OnMouseDown()
	{
		Debug.Log($"{gameObject.name} was clicked");
	}
}