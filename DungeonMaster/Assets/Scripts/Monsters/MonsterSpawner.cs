using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

	// List of different monster types. Monster1 is in monsters[0], etc.
    [SerializeField]
    private List<GameObject> monsters;
    [SerializeField]
    private GameObject summonefx;
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnMonster (int monster, Vector3 position) {

        Vector3 mposition = new Vector3(position.x, position.y, 0);
		GameObject.Instantiate(monsters[monster], mposition, Quaternion.identity);
     
            GameObject monsterToBeSummoned = GameObject.Instantiate(summonefx, mposition,Quaternion.identity);
		Debug.Log("Spawned monster" + (monster) + " at loc " + mposition.x + "," + mposition.y);
       
    }
}
