using UnityEngine;
using System;
using System.Collections.Generic;

public class VetoCollider: MonoBehaviour
{
	private void OnMouseDown()
	{
		GameManager.Instance.Vote(false);
	}
}