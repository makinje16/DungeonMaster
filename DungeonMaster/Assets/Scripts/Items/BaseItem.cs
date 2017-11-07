using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

public class BaseItem : MonoBehaviour
{
	internal CircleCollider2D rangeCollider2D;
	internal HeroController _heroController;
	internal WinCondition _winCondition;
	[SerializeField] internal float pickupRadius;

	
	public void ItemEffect()
	{
	}
	
	// Use this for initialization
	void Start () {
		_heroController = GameObject.Find("Hero").GetComponent<HeroController>();
		_winCondition = GameObject.Find("GameManager").GetComponent<WinCondition>();
		rangeCollider2D = GetComponent<CircleCollider2D>();
		rangeCollider2D.radius = pickupRadius;
	}

	internal void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.gameObject.tag.Contains("Hero")) return;
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
