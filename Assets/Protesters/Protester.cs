using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Protester : MonoBehaviour
{

	//state stuff

	public bool despawning;

	NavMeshAgent agent;
	void Awake () {
		agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SetDestination(Vector3 destination){
		//Debug.Log("setting destination to " + destination.ToString());
		agent.destination = destination;
	}
}
