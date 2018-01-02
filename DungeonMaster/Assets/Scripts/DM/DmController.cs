using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DmController : NetworkBehaviour {

#region member Variables
    private gamecontroller.DMAbilities currentabilities;
	
    [SerializeField] private GameObject monsterspawns;
    [SerializeField] private GameObject itemspawns;
	[SerializeField] private List<Transform> monsterSpawnTransforms;
	[SerializeField] private List<GameObject> dmTraps;
	
	[SerializeField] private float manaRate;
	[SerializeField] private float maxMana;
	[SerializeField] private float cameraSpeed;
	[SerializeField] private float xMaxBound;
	[SerializeField] private float xMinBound;
	[SerializeField] private float yMaxBound;
	[SerializeField] private float yMinBound;
	
    private gamecontroller gc;
	
	public float GetManaCount () {
		return manaCount;
	}
	public float GetMaxMana () {
		return maxMana;
	}
	public float GetManaRate () {
		return manaRate;
	}
	public float getmanapercentage()
	{
		return manapercentage;
	}

	public void SetMaxMana (int newMax) {
		maxMana = newMax;
	}
	public void SetManaRate (float newRate) {
		manaRate = newRate;
	}

	private string zoneToSummon;
	
	private int trapType;
	private int monsterToSummon;
	
	private bool toBeSummoned;
	private bool trapToBeActivated;
	private bool manaLocked;
	private bool isInfiniteMana;

	private InputManager inputManager;
	private Transform heroTransform;

	private float manapercentage;
	private float infiniteManaCounter;
	private float manaCount;
	private readonly float edgePercentage = .1f;

	public static float ITEM_DESPAWN_TIME = 5f;
	public static float MANA_LOCK_TIME = 3f;
	public const float INFINITE_MANA_TIME = 5f;
	public const float INFINITE_MANA_COOLDOWN = 30f;

	private Vector3 trapLoc;
#endregion

#region Unity Functions
    // Use this for initialization
    void Start () {
		manaCount = 50;
		maxMana = 100;

		monsterToSummon = 0;
		toBeSummoned = false;
		trapToBeActivated = false;
		zoneToSummon = "";
		trapType = -1;
		manaLocked = false;
		infiniteManaCounter = 120;
		heroTransform = GameObject.FindGameObjectWithTag("Hero").GetComponent<Transform>();
		monsterSpawnTransforms = monsterspawns.GetComponentsInChildren<Transform>().ToList();
        Debug.Log(monsterSpawnTransforms.Count);

	    Cursor.lockState = CursorLockMode.Confined;
    }
	
	// Update is called once per frame
	void Update () {

		if (isLocalPlayer)
		{
			mouseMovement();
			if (heroTransform == null)
			{
				heroTransform = GameObject.FindGameObjectWithTag("Hero").GetComponent<Transform>();
				Debug.Log("Cant find heroTransform.");
			}
			if (gc == null)
			{
				gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<gamecontroller>();
				currentabilities = gc.getAbilities();
			}
			if (manapercentage > 0)
			{
				manapercentage -= Time.deltaTime;
			}

			float recharge_factor = 1;
			if (GameObject.FindGameObjectsWithTag("Monster").Length == 0)
			{
				recharge_factor = 3;
			}

			ChangeMana(Time.deltaTime * manaRate * recharge_factor);

			//make sure we have the input manager
			if (inputManager == null)
			{
				inputManager = GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputManager>();
			}

			monsterSpawnTransforms.Sort((p1, p2) =>
				Vector3.Distance(p1.position, heroTransform.position)
					.CompareTo(Vector3.Distance(p2.position, heroTransform.position)));

			if (inputManager.GetDmNum() != -1)
			{
				toBeSummoned = true;
				monsterToSummon = inputManager.GetDmNum();

				if (((monsterToSummon < 3) &&
				     (gamecontroller.HasFlag(currentabilities, gamecontroller.DMAbilities.meleemonsters))) ||
				    ((monsterToSummon >= 3) &&
				     (gamecontroller.HasFlag(currentabilities, gamecontroller.DMAbilities.specialmonsters))))
				{
					SummonMonster();
				}


			}
			else if (inputManager.GetDmSpell() != -1)
			{
				trapToBeActivated = true;
				trapType = inputManager.GetDmSpell();
				if (gamecontroller.HasFlag(currentabilities, gamecontroller.DMAbilities.spells))
				{
					ActivateTrap();
				}

			}

			// ESC key to cancel any queued actions
			else if (Input.GetKeyDown(KeyCode.Escape))
			{
				CleanInput();
			}
			else if (Input.GetKeyDown(KeyCode.L))
			{
				if (gamecontroller.HasFlag(currentabilities, gamecontroller.DMAbilities.infintemana))
				{
					infiniteMana();
				}

			}

			infiniteManaCounter += Time.deltaTime;
		}
	}
	
#endregion
	
#region member Functions
	void SummonMonster () {
		if (isLocalPlayer)
		{
			mouseMovement();
			int manaCost = monsterToSummon * 10;

			// Not enough mana!
			if (manaCost > manaCount && !isInfiniteMana)
			{
				// UI warning
				CleanInput();
				return;
			}

			toBeSummoned = false;

			transform.Find("SpawnPointQ").GetComponent<MonsterSpawner>()
				.SpawnMonster(monsterToSummon, monsterSpawnTransforms[0].position);
			Debug.Log(monsterSpawnTransforms[0].position);
			if (monsterToSummon == 2)
				monsterToSummon = 0;

			transform.Find("SpawnPointQ").GetComponent<MonsterSpawner>()
				.SpawnMonster(monsterToSummon, monsterSpawnTransforms[1].position);
			Debug.Log(monsterSpawnTransforms[1].position);

			// Deduct mana
			if (isInfiniteMana)
			{
				return;
			}

			ChangeMana(0 - manaCost);
			CleanInput();

			gc.summontwo();
		}
	}

	private void mouseMovement()
	{
		if (Input.mousePosition.x >= Screen.width)
		{
			gameObject.transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
		}
		if (Input.mousePosition.x <= 0)
		{
			gameObject.transform.Translate(Vector3.left * cameraSpeed * Time.deltaTime);
		}
		if (Input.mousePosition.y >= Screen.height)
		{
			gameObject.transform.Translate(Vector3.up * cameraSpeed * Time.deltaTime);
		}
		if (Input.mousePosition.y <= 0)
		{
			gameObject.transform.Translate(Vector3.down * cameraSpeed * Time.deltaTime);
		}
		checkBounds();
	}

	private void checkBounds()
	{
		if (transform.position.x >= xMaxBound)
		{
			transform.position = new Vector3(xMaxBound, transform.position.y);
		}
		if (transform.position.x <= xMinBound)
		{
			transform.position = new Vector3(xMinBound, transform.position.y);
		}
		if (transform.position.y >= yMaxBound)
		{
			transform.position = new Vector3(transform.position.x, yMaxBound);
		}
		if (transform.position.y <= yMinBound)
		{
			transform.position = new Vector3(transform.position.x, yMinBound);
		}
	}

	void ActivateTrap () {
        //Debug.Log ("Activating trap " + trapType + " " + trapLoc);
        trapLoc = heroTransform.transform.position;
		int manaCost = 55;
        

		if (manaCost > manaCount) {
			CleanInput ();
			return;
		}

		GameObject.Instantiate (dmTraps [trapType - 1], trapLoc, Quaternion.identity);
		ChangeMana (0 - manaCost);
		CleanInput ();
	}

	void CleanInput() {
		monsterToSummon = 0;
		trapType = -1;
		toBeSummoned = false;
		trapToBeActivated = false;
		zoneToSummon = "";
	}

	public void activateManaLock()
	{
		manaLocked = true;
		Invoke("deactivateManaLock", MANA_LOCK_TIME);
	}

    public bool checkInfinteMana()
    {
        if (INFINITE_MANA_COOLDOWN > infiniteManaCounter)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

	private void infiniteMana()
	{
		if (checkInfinteMana())
        {
            ChangeMana(100);
            manaLocked = true;
            isInfiniteMana = true;
            infiniteManaCounter = 0;
            Invoke("deactivateManaLock", INFINITE_MANA_TIME);
            manapercentage = INFINITE_MANA_TIME;
        }
		
	}
	
	private void deactivateManaLock()
	{
		manaLocked = false;
		isInfiniteMana = false;
	}
	
	public void ChangeMana (float amount)
	{
		if (manaLocked && amount > 0){return;}
        
		manaCount += amount;
		if (manaCount > maxMana)
			manaCount = maxMana;

	//	manaText.text = "Mana: " + manaCount.ToString() + "/" + maxMana.ToString();
	}
	
	public bool getisinfinitemana()
	{
		return isInfiniteMana;
	}
#endregion
}
