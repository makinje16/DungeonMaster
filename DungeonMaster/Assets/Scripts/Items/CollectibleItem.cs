using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : BaseItem {
	
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.gameObject.tag.Contains("Hero")) return;
		if(_winCondition.isItems())
		_winCondition.updateItems();
		
		gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
