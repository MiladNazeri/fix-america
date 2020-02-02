using UnityEngine;
using System;
using System.Collections.Generic;

public class ApproveCollider: MonoBehaviour
{
	private void OnMouseDown()
	{
		GameManager.Instance.Approve();
	}
}