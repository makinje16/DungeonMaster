using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seekingMonster : Monster {

    private Vector2 direction;
    

    void Start()
    {
        hero = GameObject.FindWithTag("Hero");
        direction = hero.transform.position - transform.position;
    }

    void Update()
    {
        direction = hero.transform.position - transform.position;
        transform.Translate(direction.normalized * Time.deltaTime * movementSpeed);
    }
}
