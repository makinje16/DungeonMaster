using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SelectionScreen : NetworkBehaviour
{

	public MatchSetup PlayerPrefab;

	private void Start()
	{
		
	}

	public void CallSpawnHero()
	{
		PlayerPrefab.SpawnHero();
		gameObject.SetActive(false);
	}
}
