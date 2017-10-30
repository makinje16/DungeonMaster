﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DmController : MonoBehaviour {

	[SerializeField]
	private Text manaText;

	private int manaCount;
	private int monsterToSummon;
	private string zoneToSummon;
	private bool toBeSummoned;

	// Use this for initialization
	void Start () {
		manaCount = 0;
		//every two seconds
		InvokeRepeating ("IncrementMana", 2.0f, 2.0f);
		monsterToSummon = 0;
		toBeSummoned = false;
		zoneToSummon = "";
	}
	
	// Update is called once per frame
	void Update () {
		
		// Check for monster summon by number
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
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
			SummonMonster (monsterToSummon, zoneToSummon);
		} else if (Input.GetKeyDown ("w") && toBeSummoned) {
			zoneToSummon = "w";
			SummonMonster (monsterToSummon, zoneToSummon);
		} else if (Input.GetKeyDown ("e") && toBeSummoned) {
			zoneToSummon = "e";
			SummonMonster (monsterToSummon, zoneToSummon);
		} else if (Input.GetKeyDown ("a") && toBeSummoned) {
			zoneToSummon = "a";
			SummonMonster (monsterToSummon, zoneToSummon);
		} else if (Input.GetKeyDown ("s") && toBeSummoned) {
			zoneToSummon = "s";
			SummonMonster (monsterToSummon, zoneToSummon);
		} else if (Input.GetKeyDown ("d") && toBeSummoned) {
			zoneToSummon = "d";
			SummonMonster (monsterToSummon, zoneToSummon);
		}

		// ESC key to cancel any queued actions
		else if (Input.GetKeyDown (KeyCode.Escape)) {
			monsterToSummon = 0;
			toBeSummoned = false;
			zoneToSummon = "";
		}
	}

	// Passive mana regen, updates UI element
	void IncrementMana () {
		ChangeMana (1);
	}

	void ChangeMana (int amount) {
		manaCount += amount;
		manaText.text = "Mana: " + manaCount.ToString();
	}

	void SummonMonster (int monster, string zone) {
		int manaCost = monster;
		
		toBeSummoned = false;

		// Deduct mana
		ChangeMana(0 - manaCost);
	}
}
