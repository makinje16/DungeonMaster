using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

	[SerializeField]
	private int xMin;

	[SerializeField]
	private int xMax;

	[SerializeField]
	private int yMin;

	[SerializeField]
	private int yMax;

	Dictionary<string, Zone> zones;

	// Use this for initialization
	void Start () {
		zones = new Dictionary<string, Zone>();

		float xDiff = ((float)xMax - (float)xMin) / 3;
		float yDiff = ((float)yMax - (float)yMin) / 2;

		zones.Add("q", new Zone ((float)xMin, (float)xMin + xDiff, (float)yMax, (float)yMax - yDiff));
		zones.Add("w", new Zone ((float)xMin + xDiff, (float)xMin + (2 * xDiff), (float)yMax, (float)yMax - yDiff));
		zones.Add("e", new Zone ((float)xMin + (2 * xDiff), (float)xMax, (float)yMax, (float)yMax - yDiff));
		zones.Add("a", new Zone ((float)xMin, (float)xMin + xDiff, (float)yMax - yDiff, (float)yMax - 2 * yDiff));
		zones.Add("s", new Zone ((float)xMin, (float)xMin + xDiff, (float)yMax - yDiff, (float)yMax - 2 * yDiff));
		zones.Add("d", new Zone ((float)xMin, (float)xMin + xDiff, (float)yMax - yDiff, (float)yMax - 2 * yDiff));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnMonster (int monster, string zone) {

	}
}

public struct Zone {
	public float xMin, xMax, yMin, yMax;

	public Zone(float x1, float x2, float y1, float y2) {
		xMin = x1;
		xMax = x2;
		yMin = y1;
		yMax = y2;
	}
}