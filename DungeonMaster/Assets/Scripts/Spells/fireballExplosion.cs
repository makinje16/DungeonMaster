using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballExplosion : MonoBehaviour {

	[SerializeField]
	float timer = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0) {
			DestroyObject (gameObject);
		}
	}
}
