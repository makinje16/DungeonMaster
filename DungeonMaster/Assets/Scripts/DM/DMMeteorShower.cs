using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMMeteorShower : MonoBehaviour {

    [SerializeField]
    private GameObject oneexplosion;

    private int num_explosions = 20;
    private int cur_explosions = 0;

    private float radius = 4.5f;

    private GameObject hero;

	// Use this for initialization
	void Start () {
        Invoke("dooneexplosion", .1f);
    }
	
	// Update is called once per frame
	void Update () {
		
        if (hero == null)
        {
            hero = GameObject.FindGameObjectWithTag("Hero");
        }



	}


    void dooneexplosion()
    {
        Vector2 exploc = hero.transform.position;
        exploc += new Vector2(Random.Range(-radius, radius), Random.Range(-radius, radius));

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
