using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtesterSpawn : MonoBehaviour
{
	 void OnDrawGizmos()
    {

        Gizmos.DrawIcon(transform.position, "SpawnPoint.png", false);
    }
}
