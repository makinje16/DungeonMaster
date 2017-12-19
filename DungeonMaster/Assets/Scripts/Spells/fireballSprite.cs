using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballSprite : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject hero = GameObject.FindWithTag("Hero");
		Vector2 dir = hero.transform.position - transform.position;
		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
