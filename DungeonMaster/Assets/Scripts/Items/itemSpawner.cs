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
	
	// Use this for initialization
	void Start () {
		initItems();
	}


	private void initItems()
	{
		System.Random rand = new System.Random((int)Time.time);
		int numItems = rand.Next(0, 5);
		int xlocation = rand.Next(xMin, xMax);
		int ylocation = rand.Next(yMin, yMax);

		for (int i = 0; i < numItems; ++i)
		{
			System.Random randomItem = new System.Random((int) Time.time);
			var item = randomItem.Next(0, items.Length - 1);
			GameObject.Instantiate(items[item], new Vector3(xlocation, ylocation), Quaternion.identity);
		}
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}
}
