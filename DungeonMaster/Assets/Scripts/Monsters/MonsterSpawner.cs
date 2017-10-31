using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {
	// Use this for initialization


    [SerializeField]
    private List<GameObject> monsters;

	
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnMonster (int monster) {
        // We'll want to spawn different types of monsters here
        
		GameObject.Instantiate(monsters[monster], transform.position, Quaternion.identity);
		Debug.Log("Spawned monster" + monster + " at zone " + name);
	}
}