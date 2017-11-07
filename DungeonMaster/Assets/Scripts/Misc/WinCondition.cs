using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{

	[SerializeField] private  int clockTime;
	[SerializeField] private  int itemsToCollect;

	[SerializeField] private GameObject UIPrefab;

	private UIManager _uiManager;

	private winCondition condition;

	private float timer;
	
	private enum winCondition{ Time, Items};
	
	// Use this for initialization
	void Start ()
	{
		_uiManager = UIPrefab.GetComponent<UIManager>();
		chooseWinCondition();
		timer = 0;
	}

	private void chooseWinCondition()
	{
		System.Random rand = new System.Random(DateTime.Now.Millisecond);
		int conditionIndex = rand.Next(0, 2);
		
		condition = (winCondition) conditionIndex;
		
		switch(condition)
		{
			case winCondition.Items:
				_uiManager.updateWinConditionText("Items Left: " + itemsToCollect);
				break;
			
			case winCondition.Time:
				_uiManager.updateWinConditionText("Time Left: " + clockTime);
				break;
				
		}
		
	}

	private void UpdateTime()
	{
		clockTime -= 1;
		_uiManager.updateWinConditionText("Time Left: " + clockTime);
	}

	public void updateItems()
	{
		itemsToCollect -= 1;
		_uiManager.updateWinConditionText("Items Left: " + itemsToCollect);
	}

	public bool isItems()
	{
		return condition == winCondition.Items;
	}
	
	// Update is called once per frame
	private void Update () {
		if (condition == winCondition.Time)
		{
			timer += Time.deltaTime;
			if (!(timer >= 1f)) return;
			timer = 0;
			UpdateTime();
		}
	}
}
