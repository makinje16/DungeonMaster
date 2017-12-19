using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakableCrate : MonoBehaviour {

	// Use this for initialization
	void Start () {
        float myc = Random.Range(.3f, .7f);
        if (gameObject.layer == 14)
        {
            GetComponent<SpriteRenderer>().color = new Color(myc-.3f, myc+.3f, myc-.3f,.45f);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(myc, myc, myc);
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
