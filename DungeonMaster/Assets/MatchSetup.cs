using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MatchSetup : NetworkBehaviour
{

	[SerializeField] private GameObject Hero;
	
	
	// Use this for initialization
	void Start () {
		GameObject.Find("SelectionScreen").GetComponent<SelectionScreen>().PlayerPrefab = this;
	}

	public void SpawnHero()
	{
		CmdSpawnHero(Hero);
	}
	
	[Command]
	public void CmdSpawnHero(GameObject hero)
	{
		GameObject heroGameObject = (GameObject)Instantiate(hero, transform.position, Quaternion.identity);
		NetworkServer.SpawnWithClientAuthority(heroGameObject, this.gameObject);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
