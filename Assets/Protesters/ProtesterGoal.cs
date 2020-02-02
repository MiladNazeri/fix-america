using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is the place where protesters go to protest

public class ProtesterGoal : MonoBehaviour
{
	
	public Transform lookAtTarget;

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "ProtesterGoal.png", false);
    }
}
