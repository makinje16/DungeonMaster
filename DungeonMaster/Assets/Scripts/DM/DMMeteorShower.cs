using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMMeteorShower : MonoBehaviour {

    [SerializeField]
    private GameObject oneexplosion;

    private int num_explosions = 20;
    private int cur_explosions = 0;

    private float radius = 4.5f;
    private Vector2 orig;

    private GameObject hero;

	// Use this for initialization
	void Start () {
        Invoke("dooneexplosion", .1f);
        if (hero == null)
        {
            hero = GameObject.FindGameObjectWithTag("Hero");
            orig = hero.transform.position;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
        



	}


    void dooneexplosion()
    {
        Vector2 exploc = orig;
        exploc += new Vector2(Random.Range(-radius, radius), Random.Range(-radius, radius));
        radius -= .25f;
        if (radius <= .5f)
        {
            radius = .5f;
        }
        Instantiate(oneexplosion, exploc, Quaternion.identity);
        cur_explosions++;
        if (cur_explosions < num_explosions)
        {
            Invoke("dooneexplosion", .15f);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
