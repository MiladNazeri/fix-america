using UnityEngine;
using System;
using System.Collections.Generic;

public class NewBillCollider: MonoBehaviour
{
	private void OnMouseDown()
	{
		Debug.Log("Generating a new bill.");
		GameState.Instance.CurrentBill = Backend.Instance.GetNewBill();
		var popularity = Backend.Instance.GetBillPopularity(
			GameState.Instance.CurrentBill.Item2,
			GameState.Instance.CurrentBill.Item3
		);
		Debug.Log($"New Bill is: {GameState.Instance.CurrentBill.Item1}");
		Debug.Log($"Potential popularity of the bill is: {(int)(popularity * 100)}%.");
	}
}