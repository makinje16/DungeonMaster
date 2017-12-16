using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seekingMonster : Monster {

	private SpriteRenderer sr;
    
    void Start()
    {
        hero = GameObject.FindWithTag("Hero");
        direction = hero.transform.position - transform.position;
		sr = GetComponentInChildren<SpriteRenderer> ();
    }


    protected override void Move()
    {
        direction = hero.transform.position - transform.position;
        transform.Translate(direction.normalized * Time.deltaTime * movementSpeed);
		if (direction.x >= 0) {
			sr.flipX = false;
		} else {
			sr.flipX = true;
		}
    }

    protected override void Update()
    {
        base.Update();
        direction = hero.transform.position - transform.position;
        //transform.Translate(direction.normalized * Time.deltaTime * movementSpeed);
        if (direction.x >= 0)
        {
           // sr.flipX = false;
        }
        else
        {
           // sr.flipX = true;
        }
    }
}
