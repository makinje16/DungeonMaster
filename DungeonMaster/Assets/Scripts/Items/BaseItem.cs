using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

public class BaseItem : MonoBehaviour
{
	private CircleCollider2D rangeCollider2D;
	private HeroController _heroController;

	[SerializeField] private float pickupRadius;

	
	public void ItemEffect()
	{
	}
	
	// Use this for initialization
	void Start () {
		_heroController = GameObject.Find("Hero").GetComponent<HeroController>();
		rangeCollider2D = GetComponent<CircleCollider2D>();
		rangeCollider2D.radius = pickupRadius;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.gameObject.tag.Contains("Hero")) return;
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
