using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtesterManager : MonoBehaviour
{
	public int desiredProtesters;

	public List<Protester> protesters;

	public ProtesterSpawn[] spawnPoints;
	public GameObject protesterPrefab;
	public ProtesterGoal[] goals;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = Resources.FindObjectsOfTypeAll(typeof(ProtesterSpawn)) as ProtesterSpawn[];
		goals = Resources.FindObjectsOfTypeAll(typeof(ProtesterGoal)) as ProtesterGoal[];
		protesters = new List<Protester>();
    }

	public void SetProtesterAmount(int amount){
		Debug.Log("requesting protesters: " + amount.ToString());
		desiredProtesters = amount;
		UpdateProtesters();
	}
	void UpdateProtesters(){
		if(desiredProtesters < protesters.Count){
			int c = protesters.Count - desiredProtesters;
			while (c > 0){
				RemoveProtester();
				c--;
			}
		} else if(desiredProtesters > protesters.Count){
			int c = desiredProtesters - protesters.Count;
			while (c > 0){
				AddProtester();
				c--;
			}
		}

	}
	void AddProtester(){
		Debug.Log("adding protester");
		//create a protester at a random spawn point and send them to a random destination
		GameObject newProtesterPrefab = Instantiate(protesterPrefab, randomSpawnPoint(), Quaternion.identity);
		Protester newProtester = newProtesterPrefab.GetComponent<Protester>();
		newProtester.SetDestination(randomGoal());
		protesters.Add(newProtester);
	}
	void RemoveProtester(){
		Debug.Log("removing protester");
		//find a random protester, mark them as leaving and send them back to a spawn point to despawn
	}

	Vector3 randomSpawnPoint(){
		Random rand = new Random();
		var pick = Random.Range(0,(spawnPoints.Length - 1));
		return spawnPoints[pick].transform.position;
	}
	Vector3 randomGoal(){
		Random rand = new Random();
		var pick = Random.Range(0,(goals.Length - 1));
		return goals[pick].transform.position;
	}
}
