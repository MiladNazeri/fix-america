using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtesterAnimator : MonoBehaviour
{
	public Transform LeftArm;
	public Transform RightArm;
	public Transform Head;
	public Transform Body;
	//public Transform Head;

	public float headRotationMax = 25.0f;
	public float armOffsetMax = 0.2f;
	public float maxAnimSpeed = 2.0f;

	Vector3 originalLeftArmPosition;
	Vector3 originalRightArmPosition;
	float randomOffsetLeftHand;
	float randomOffsetRightHand;
	float randomValueLeftHand;
	float randomValueRightHand;
	float headRotationAmount;
	float headNodAmount;
	float headRotationSpeed;
	float headNodSpeed;

    // Start is called before the first frame update
    void Start()
    {
		originalLeftArmPosition = LeftArm.localPosition;
		originalRightArmPosition = RightArm.localPosition;
		randomOffsetLeftHand = Random.value;
		randomOffsetRightHand = Random.value;
		randomValueLeftHand = Random.value;
		randomValueRightHand = Random.value;

		headRotationAmount = Random.value * headRotationMax;
		headNodAmount = Random.value * headRotationMax;
		headRotationSpeed = Random.value;
		headNodSpeed = Random.value;
    }

    // Update is called once per frame
    void Update()
    {
		float rotX =  Mathf.Sin(Time.time * headRotationSpeed) * headRotationAmount;
		float rotY =  Mathf.Sin(Time.time * headNodSpeed) * headNodAmount;
		Vector3 setRot = new Vector3(rotX, 0.0f, rotY);
		Head.localEulerAngles = setRot;

		//move arms up and down
		float armOffsetLeft =  Mathf.Sin(Time.time + randomOffsetLeftHand) * armOffsetMax * randomValueLeftHand;
		Vector3 offPosL = new Vector3(0.0f, armOffsetLeft, 0.0f);
		LeftArm.localPosition = originalLeftArmPosition + offPosL;

		float armOffsetRight =  Mathf.Sin(Time.time + randomOffsetRightHand) * armOffsetMax * randomValueRightHand;
		Vector3 offPosR = new Vector3(0.0f, armOffsetRight, 0.0f);
		RightArm.localPosition = originalRightArmPosition + offPosR;
    }
}
