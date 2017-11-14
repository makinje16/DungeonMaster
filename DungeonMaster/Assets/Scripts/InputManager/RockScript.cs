using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (Random.Range(0,10) > 5)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if(Random.Range(0, 10) > 5)
        {
            GetComponent<SpriteRenderer>().flipY = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
