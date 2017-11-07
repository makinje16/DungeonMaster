using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : BaseItem
{

	private bool collected;
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.gameObject.tag.Contains("Hero") || collected) return;
		if(_winCondition.isItems())
		_winCondition.updateItems();
		collected = true;
		Destroy(gameObject);
	}

	// Update is called once per frame
	void Update ()
	{
		collected = false;
	}
}
