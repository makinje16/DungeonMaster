using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seekingMonster : Monster {

    
    void Start()
    {
        hero = GameObject.FindWithTag("Hero");
        direction = hero.transform.position - transform.position;
    }

    protected override void Move()
    {
        direction = hero.transform.position - transform.position;
        transform.Translate(direction.normalized * Time.deltaTime * movementSpeed);
    }
}
