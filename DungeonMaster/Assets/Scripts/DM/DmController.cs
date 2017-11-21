using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DmController : MonoBehaviour {

	private float manaCount;

	public float GetManaCount () {
		return manaCount;
	}

	[SerializeField]
	private float maxMana;

	public float GetMaxMana () {
		return maxMana;
	}

	[SerializeField]
	private List<GameObject> dmTraps;

	public void SetMaxMana (int newMax) {
		maxMana = newMax;
	}

	[SerializeField]
	private float manaRate;

	public float GetManaRate () {
		return manaRate;
	}

	public void SetManaRate (float newRate) {
		manaRate = newRate;
	}

	private int monsterToSummon;
	private string zoneToSummon;
	private bool toBeSummoned;
	private bool trapToBeActivated;
	private Vector3 trapLoc;
	private int trapType;
	private InputManager inputManager;

	// Use this for initialization
	void Start () {
		manaCount = 0;
		maxMana = 100;
		//every two seconds
		//InvokeRepeating ("IncrementMana", 0.2f, 0.2f);
		monsterToSummon = 0;
		toBeSummoned = false;
		trapToBeActivated = false;
		zoneToSummon = "";
		trapType = -1;
	}
	
	// Update is called once per frame
	void Update () {

		ChangeMana (Time.deltaTime * manaRate);

		//make sure we have the input manager
		if (inputManager == null)
		{
			inputManager = GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputManager>();
		}

//		// Check for monster summon by number OR for trap activation
//		if (Input.GetKeyDown (KeyCode.Alpha1)) {
//			// Queue monster 1
//			toBeSummoned = true;
//			monsterToSummon = 1;
//		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
//			// Queue monster 2
//			toBeSummoned = true;
//			monsterToSummon = 2;
//		} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
//			// Queue monster 3
//			toBeSummoned = true;
//			monsterToSummon = 3;
//		} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
//			// Queue monster 4
//			toBeSummoned = true;
//			monsterToSummon = 4;
//		}
//
//		// Check for summon zone
//		else if (Input.GetKeyDown ("q") && toBeSummoned) {
//			zoneToSummon = "q";
//			SummonMonster ();
//		} else if (Input.GetKeyDown ("w") && toBeSummoned) {
//			zoneToSummon = "w";
//			SummonMonster ();
//		} else if (Input.GetKeyDown ("e") && toBeSummoned) {
//			zoneToSummon = "e";
//			SummonMonster ();
//		} else if (Input.GetKeyDown ("a") && toBeSummoned) {
//			zoneToSummon = "a";
//			SummonMonster ();
//		} else if (Input.GetKeyDown ("s") && toBeSummoned) {
//			zoneToSummon = "s";
//			SummonMonster ();
//		} else if (Input.GetKeyDown ("d") && toBeSummoned) {
//			zoneToSummon = "d";
//			SummonMonster ();
//		}
//
//		// Check for trap
//		else if (Input.GetKeyDown ("t")) {
//			trapToBeActivated = true;
//		}

		if (inputManager.GetDmNum () != -1) {
			if (trapToBeActivated) {
				trapType = inputManager.GetDmNum ();
			} else {
				toBeSummoned = true;
				monsterToSummon = inputManager.GetDmNum ();
			}
		} else if (inputManager.GetDmKey () != "") {
			if (inputManager.GetDmKey () == "t") {
				trapToBeActivated = true;
			} else if (toBeSummoned) {
				zoneToSummon = inputManager.GetDmKey ();
				SummonMonster ();
			}
		} else if (inputManager.GetDmMouseClick () != null && trapToBeActivated && trapType != -1) {
			trapLoc = inputManager.GetDmMouseClick ().Value;
			ActivateTrap ();
		}

		// ESC key to cancel any queued actions
		else if (Input.GetKeyDown (KeyCode.Escape)) {
			CleanInput ();
		}
	}

	void SummonMonster () {
		int manaCost = monsterToSummon * 10;

		// Not enough mana!
		if (manaCost > manaCount) {
			// UI warning
			return;
		}
		
		toBeSummoned = false;

		transform.Find ("SpawnPoint" + zoneToSummon.ToUpper()).GetComponent<MonsterSpawner> ().SpawnMonster(monsterToSummon);

		// Deduct mana
		ChangeMana(0 - manaCost);
		CleanInput ();
	}

	void ActivateTrap () {
		//Debug.Log ("Activating trap " + trapType + " " + trapLoc);
		int manaCost = trapType * 10 + 20;

		if (manaCost > manaCount) {
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

	public void ChangeMana (float amount) {
		manaCount += amount;
		if (manaCount > maxMana)
			manaCount = maxMana;

	//	manaText.text = "Mana: " + manaCount.ToString() + "/" + maxMana.ToString();
	}
}
