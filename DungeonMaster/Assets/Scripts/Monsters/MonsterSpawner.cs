using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnMonster (int monster) {
		// We'll want to spawn different types of monsters here
		GameObject.Instantiate(new Monster(), Transform.position);
	}
}