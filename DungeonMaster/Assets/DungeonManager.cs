using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DungeonManager : NetworkBehaviour
{

	[SerializeField] private GameObject Hero;
	[SerializeField] private GameObject DungeonMaster;
	
	// Use this for initialization
	void Start ()
	{
		ClientScene.RegisterPrefab(Hero);
		ClientScene.RegisterPrefab(DungeonMaster);
	}

	public void SpawnHero()
	{
			CmdSpawnHero(Hero);
	}
	
	[Command]
	public void CmdSpawnHero(GameObject hero)
	{
		GameObject heroGameObject = (GameObject)Instantiate(hero, transform.position, Quaternion.identity);
		NetworkServer.SpawnWithClientAuthority(heroGameObject, connectionToClient);
	}

	[Command]
	public void CmdSpawnDM()
	{
		GameObject DM = Instantiate(DungeonMaster, new Vector3(0, 0, 0), Quaternion.identity);
		NetworkServer.SpawnWithClientAuthority(DM, connectionToClient);
	}
}
