using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : BaseItem {

	// Use this for initialization
	void Start () {
		Invoke("destroyGameobject", 5);
	}

	private void destroyGameobject()
	{
		Destroy(gameObject);
	}
}
