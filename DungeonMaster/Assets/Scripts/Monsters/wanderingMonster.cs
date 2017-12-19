using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wanderingMonster : Monster {
	SpriteRenderer sr;
    

    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        hero = GameObject.FindWithTag("Hero");
        direction = hero.transform.position - transform.position;
        if (direction.x < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
        Invoke("ChangeDirection", Random.Range(1,3));
		
    }



    protected void ChangeDirection()
    {
        if (sr == null)
        {
            sr = GetComponentInChildren<SpriteRenderer>();
        }
        direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        Invoke("ChangeDirection", Random.Range(1, 3));

		if (direction.x < 0) {
			sr.flipX = true;
		} else {
			sr.flipX = false;
		}
    }
}
