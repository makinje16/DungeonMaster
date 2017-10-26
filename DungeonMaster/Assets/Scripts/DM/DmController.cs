using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DmController : MonoBehaviour {

	public Text manaText;

	private int manaCount;

	// Use this for initialization
	void Start () {
		manaCount = 0;
		//every two seconds
		InvokeRepeating ("IncremenetMana", 2.0f, 2.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//passive mana regen, updates UI element
	void IncrementMana () {
		manaCount += 1;
		manaText.text = "Mana: " + manaCount.ToString();
	}
}
