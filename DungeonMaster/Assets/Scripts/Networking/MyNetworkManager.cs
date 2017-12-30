using UnityEngine;
using UnityEngine.Networking;
using Button = UnityEngine.UI.Button;

public class MyNetworkManager : NetworkManager
{

	[SerializeField] private GameObject Hero;
	[SerializeField] private GameObject DM;

	[SerializeField] private Button DungeonMasterSelection;
	[SerializeField] private Button HeroSelection;

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
}
