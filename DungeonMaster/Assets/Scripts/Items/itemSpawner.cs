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
	private GameObject[] dropableObjects;

	[SerializeField] 
	private GameObject[] items;

	[SerializeField] 
	private Transform[] spawnPoints;
	

	public GameObject collectible;

	private WinCondition _winCondition;

	private static bool droppedItem;
	
	// Use this for initialization
	void Start () {
		_winCondition = GameObject.Find("GameManager").GetComponent<WinCondition>();
		Invoke("initItems", 1);
		InvokeRepeating("dropItem", 8, 1);
		droppedItem = false;
		spawnPoints = GameObject.Find("SpawnPoints").GetComponentsInChildren<Transform>();
	}

	private void initItems()
	{
		System.Random rand = new System.Random(DateTime.Now.Millisecond);
		int numItems = rand.Next(0, 6);
		List<int> locations = new List<int>();
		
		Debug.Log("Adding " + numItems + " items");
		
		//adds all the index locations for the items
		locations = addLocationSpots(ref locations, numItems);
		
		Debug.Log("There are " + locations.Count + " to place items at!");
		//puts one item at each location based on the indexes stored in locations<>
		for (int i = 0; i < numItems; ++i)
		{
			var xlocation = spawnPoints[locations[i]].position.x;
			var ylocation = spawnPoints[locations[i]].position.y;
			
			var item = rand.Next(0, items.Length);
			GameObject.Instantiate(items[item], new Vector3(xlocation, ylocation), Quaternion.identity);
		}

		if (!_winCondition.isItems()) return;

		locations = addLocationSpots(ref locations, _winCondition.itemsToCollect);
		
		//if itemsToCollect then spawn them in... can be on other items.
		for (int i = locations.Count - _winCondition.itemsToCollect; i < locations.Count; ++i)
		{
			var index = rand.Next(0, spawnPoints.Length);
			Debug.Log("Index is " + index);
			
			var xlocation = spawnPoints[index].transform.position.x;
			var ylocation = spawnPoints[index].transform.position.y;
			
			Instantiate(collectible, new Vector3(xlocation, ylocation), Quaternion.identity);
		}
		Debug.Log("FinishedItems");

	}

	private List<int> addLocationSpots(ref List<int> locations, int quantityToAdd)
	{
		System.Random index = new System.Random(DateTime.Now.Millisecond);
		
		for (int i = 0; i < quantityToAdd;)
		{
			var newIndex = index.Next(0, spawnPoints.Length);
			if (locations.Contains(newIndex))
			{
				Debug.Log("Number is already in the list");
				continue;
			}
			locations.Add(newIndex);
			++i;
		}

		Debug.Log("Locations are now " + locations);
		return locations;
	}

	private void dropItem()
	{
		if(droppedItem) {return;}
		Debug.Log("Trying to drop item");
		System.Random rand = new System.Random(DateTime.Now.Millisecond);
		int chance = rand.Next(1, 20);

		if (chance != 1) return;
		droppedItem = true;
		int xlocation = rand.Next(xMin, xMax);
		int ylocation = rand.Next(yMin, yMax);
		int item = rand.Next(0, dropableObjects.Length);
		Instantiate(dropableObjects[item], new Vector3(xlocation, ylocation), Quaternion.identity);
		Invoke("canDropItems", DmController.ITEM_DESPAWN_TIME);
		Debug.Log("Dropped Item: " + item);
	}


	private void canDropItems()
	{
		droppedItem = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
