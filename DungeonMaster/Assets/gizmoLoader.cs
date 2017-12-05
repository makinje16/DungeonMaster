using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gizmoLoader : MonoBehaviour
{

	
	// Use this for initialization
	void Start () {
		
	}
	
	void OnDrawGizmos() {
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere(transform.position, .5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
