using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprintingMonster : seekingMonster {

    [SerializeField]
    private float sprintBonus;

    void Start()
    {
        hero = GameObject.FindWithTag("Hero");
        direction = hero.transform.position - transform.position;
        sprintBonus = 2;
        InvokeRepeating("Sprint", 2, 6);
    }



    protected void Sprint()
    {
        movementSpeed += sprintBonus;
        Invoke("NormalSpeed", 1);
    }

    protected void NormalSpeed()
    {
        movementSpeed -= sprintBonus;
    }
}
