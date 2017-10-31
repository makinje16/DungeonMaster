using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {
	// Use this for initialization

	[SerializeField]
	private GameObject monster1;

	[SerializeField]
	private GameObject monster2;

	[SerializeField]
	private GameObject monster3;

	[SerializeField]
	private GameObject monster4;

	private List<GameObject> monsters;

	void Start () {
		Debug.Log (name);
		try {
			monsters.Add(monster1);
			monsters.Add(monster2);
			monsters.Add(monster3);
			monsters.Add(monster4);
		} catch (System.Exception e) {

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnMonster (int monster) {
		// We'll want to spawn different types of monsters here
		GameObject.Instantiate(monsters[monster], transform.position, Quaternion.identity);
		Debug.Log("Spawned monster" + monster + " at zone " + name);
	}
}