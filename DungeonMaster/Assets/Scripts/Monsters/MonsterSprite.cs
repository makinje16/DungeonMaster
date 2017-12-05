using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSprite : MonoBehaviour {
    Quaternion rotation;
	// Use this for initialization
	void Start () {
        rotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = rotation;
	}
}
