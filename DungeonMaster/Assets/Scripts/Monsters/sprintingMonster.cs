using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprintingMonster : Monster {

    [SerializeField]
    private float sprintBonus;
    private Vector2 direction;

    void Start()
    {
        hero = GameObject.FindWithTag("Hero");
        direction = hero.transform.position - transform.position;
        sprintBonus = 2;
        InvokeRepeating("Sprint", 2, 6);
    }



    void Sprint()
    {
        movementSpeed += sprintBonus;
        Invoke("NormalSpeed", 1);
    }

    void NormalSpeed()
    {
        movementSpeed -= sprintBonus;
    }

    void Update()
    {
        direction = hero.transform.position - transform.position;
        transform.Translate(direction.normalized * Time.deltaTime * movementSpeed);
    }
}
