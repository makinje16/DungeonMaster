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
	}
	
	// Update is called once per frame
	void Update () {

		ChangeMana (Time.deltaTime * manaRate);
		
		// Check for monster summon by number OR for trap activation
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			if (trapToBeActivated)
				dmTraps [0].GetComponent<DmTrap> ().Activate ();
			// Queue monster 1
			toBeSummoned = true;
			monsterToSummon = 1;
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			// Queue monster 2
			toBeSummoned = true;
			monsterToSummon = 2;
		} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			// Queue monster 3
			toBeSummoned = true;
			monsterToSummon = 3;
		} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
			// Queue monster 4
			toBeSummoned = true;
			monsterToSummon = 4;
		}

		// Check for summon zone
		else if (Input.GetKeyDown ("q") && toBeSummoned) {
			zoneToSummon = "q";
			SummonMonster ();
		} else if (Input.GetKeyDown ("w") && toBeSummoned) {
			zoneToSummon = "w";
			SummonMonster ();
		} else if (Input.GetKeyDown ("e") && toBeSummoned) {
			zoneToSummon = "e";
			SummonMonster ();
		} else if (Input.GetKeyDown ("a") && toBeSummoned) {
			zoneToSummon = "a";
			SummonMonster ();
		} else if (Input.GetKeyDown ("s") && toBeSummoned) {
			zoneToSummon = "s";
			SummonMonster ();
		} else if (Input.GetKeyDown ("d") && toBeSummoned) {
			zoneToSummon = "d";
			SummonMonster ();
		}

		// Check for trap
		else if (Input.GetKeyDown ("t")) {
			trapToBeActivated = true;
		}

		// ESC key to cancel any queued actions
		else if (Input.GetKeyDown (KeyCode.Escape)) {
			monsterToSummon = 0;
			toBeSummoned = false;
			trapToBeActivated = false;
			zoneToSummon = "";
		}
	}

	// Passive mana regen, updates UI element
	void IncrementMana () {
		ChangeMana (1);
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
	}

	public void ChangeMana (float amount) {
		manaCount += amount;
		if (manaCount > maxMana)
			manaCount = maxMana;

	//	manaText.text = "Mana: " + manaCount.ToString() + "/" + maxMana.ToString();
	}
}
