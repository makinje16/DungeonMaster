using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using Button = UnityEngine.UI.Button;

public class MyNetworkManager : NetworkManager
{

	[SerializeField] private GameObject Hero;
	[SerializeField] private GameObject DM;

	[SerializeField] private Button DungeonMasterSelection;
	[SerializeField] private Button HeroSelection;

	[SerializeField] private Canvas characterSelectionCanvas;

	private int playerSelectionIndex = 0;
	
	// Use this for initialization
	void Start () {
		DungeonMasterSelection.onClick.AddListener(delegate { AssignPlayerPrefab(DungeonMasterSelection.name); });
		HeroSelection.onClick.AddListener(delegate { AssignPlayerPrefab(HeroSelection.name); });
	}

	private void AssignPlayerPrefab(string _buttonName)
	{
		switch (_buttonName)
		{
			case "Hero Button":
				playerSelectionIndex = 0;
				break;
			case "DM Button":
				playerSelectionIndex = 1;
				break;
		}

		playerPrefab = spawnPrefabs[playerSelectionIndex];
	}

	public override void OnClientConnect(NetworkConnection conn)
	{
		characterSelectionCanvas.enabled = false;

		IntegerMessage msg = new IntegerMessage(playerSelectionIndex);

		if (!clientLoadedScene)
		{
			ClientScene.Ready(conn);
			if (autoCreatePlayer)
			{
				ClientScene.AddPlayer(conn, 0, msg);
			}
		}
	}

	public override void OnClientDisconnect(NetworkConnection conn)
	{
		characterSelectionCanvas.enabled = true;
	}

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
	{
		int id = 0;

		if (extraMessageReader != null)
		{
			IntegerMessage i = extraMessageReader.ReadMessage<IntegerMessage>();
			id = i.value;
		}

		GameObject playerPrefab = spawnPrefabs[id];
		
		GameObject player;
		Transform startPos = GetStartPosition();
		if (startPos != null)
		{
			player = (GameObject)Instantiate(playerPrefab, startPos.position, startPos.rotation);
		}
		else
		{
			player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
		}

		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
	}

	public override void OnServerDisconnect(NetworkConnection conn)
	{
		base.OnServerDisconnect(conn);
		Debug.Log("Client disconnected");
	}
}
