using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtesterSpawn : MonoBehaviour
{
	private void OnTriggerEnter(Collider other) {
		//Debug.Log("trigger enter");
		if(other.GetComponent<Protester>()){
			if(other.GetComponent<Protester>().despawning){
				Destroy(other.gameObject);
			}
		}	
	}

	void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "SpawnPoint.png", false);
    }
}
