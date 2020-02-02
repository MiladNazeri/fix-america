using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Protester : MonoBehaviour
{

	//state stuff

	public bool despawning;
	public bool arrived;

	public Vector3 lastPosition;
	float lastCheckTime;
	public float positionCheckInterval = 3.0f;
	public float minWalkCheckDistance = 10.0f;
	public Transform lookAtTarget;

	NavMeshAgent agent;
	void Awake () {
		agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Time.time - positionCheckInterval > lastCheckTime){
			lastCheckTime = Time.time;
			if(Vector3.Distance(transform.position, lastPosition) < minWalkCheckDistance){
				arrived = true;
				agent.isStopped = true;
			}
			lastPosition = transform.position;
		}
		if(arrived){
			if(lookAtTarget != null){
				transform.LookAt(lookAtTarget);
			}
		}
    }

	public void SetDestination(ProtesterGoal goal){
		//Debug.Log("setting destination to " + destination.ToString());
		agent.destination = goal.transform.position;
		if(goal.lookAtTarget){
			lookAtTarget = goal.lookAtTarget;
		}
	}
	public void SetDestination(Vector3 t){
		//Debug.Log("setting destination to " + destination.ToString());
		agent.destination = t;
	}
}
