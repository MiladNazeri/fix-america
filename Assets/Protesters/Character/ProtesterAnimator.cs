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

	public float headRotationMax;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		float rotX =  Mathf.Sin(Time.time) * headRotationMax;
		float rotY =  Mathf.Sin(Time.time) * headRotationMax;

		//Head.transform.rotation.eulerAngles = new Vector3(rotX, 0.0f, rotY);
    }
}
