using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MonsterSpawner : NetworkBehaviour {

	// List of different monster types. Monster1 is in monsters[0], etc.
    [SerializeField]
    private List<GameObject> monsters;
    [SerializeField]
    private GameObject summonefx;
	
	// Update is called once per frame
	void Update () {
		
	}

	[ClientRpc]
	public void RpcSpawnMonster (int monster, Vector3 position) {

        Vector3 mposition = new Vector3(position.x, position.y, 0);
		GameObject monsterToBeSummoned1 = GameObject.Instantiate(monsters[monster], mposition, Quaternion.identity);
		NetworkServer.Spawn(monsterToBeSummoned1);
		
        GameObject monsterToBeSummoned2 = GameObject.Instantiate(summonefx, mposition,Quaternion.identity);
		NetworkServer.Spawn(monsterToBeSummoned2);
		Debug.Log("Spawned monster" + (monster) + " at loc " + mposition.x + "," + mposition.y);
       
    }
}
