using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixAmericaUIButton : MonoBehaviour
{

	public void ApproveClick()
	{
		StampDesktop.Instance.onSign();
	}

	public void VetoClick()
	{
		StampDesktop.Instance.onVeto();
	}

	public void TwitterClick()
	{
		TwitterPhoneController.Instance.Tweet();
	}

}