using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemSpawner : MonoBehaviour
{

	[SerializeField]
	private int xMin;

	[SerializeField]
	private int xMax;

	[SerializeField]
	private int yMin;

	[SerializeField]
	private int yMax;


	[SerializeField] 
	private GameObject[] items;

	public GameObject collectible;

	private WinCondition _winCondition;
	// Use this for initialization
	void Start () {
		_winCondition = GameObject.Find("GameManager").GetComponent<WinCondition>();
		Invoke("initItems", 1);
	}


	private void initItems()
	{
		System.Random rand = new System.Random(DateTime.Now.Millisecond);
		int numItems = rand.Next(0, 6);
		
		for (int i = 0; i < numItems; ++i)
		{
			int xlocation = rand.Next(xMin, xMax);
			int ylocation = rand.Next(yMin, yMax);
			System.Random randomItem = new System.Random(DateTime.Now.Millisecond);
			var item = randomItem.Next(0, items.Length);
			GameObject.Instantiate(items[item], new Vector3(xlocation, ylocation), Quaternion.identity);
		}

		if (!_winCondition.isItems()) return;

		for (int i = 0; i < _winCondition.itemsToCollect; ++i)
		{
			int xlocation = rand.Next(xMin, xMax);
			int ylocation = rand.Next(yMin, yMax);
			Instantiate(collectible, new Vector3(xlocation, ylocation), Quaternion.identity);
		}

	}
	
	
	// Update is called once per frame
	void Update () {
		
	}
}
