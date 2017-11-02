using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{

	[SerializeField] private  int clockTime;
	[SerializeField] private  int itemsToCollect;

	[SerializeField] private Canvas winConditionCanvas;

	[SerializeField] private Text winConditionText;

	private winCondition condition;

	private float timer;
	
	private enum winCondition{ Time, Items};
	
	// Use this for initialization
	void Start ()
	{
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
				winConditionText.text = "Items Left: ";
				break;
			
			case winCondition.Time:
				winConditionText.text = "Time Left: " + clockTime;
				break;
				
		}
		
	}

	private void UpdateTime()
	{
		clockTime -= 1;
		winConditionText.text = "Time Left: " + clockTime.ToString();
	}

	private void updateItems()
	{
		itemsToCollect -= 1;
		winConditionText.text = itemsToCollect.ToString();
	}
	
	// Update is called once per frame
	private void Update () {
		if (condition == winCondition.Time)
		{
			timer += Time.deltaTime;
			if (!(timer >= 1)) return;
			timer = 0;
			UpdateTime();
		}
	}
}
