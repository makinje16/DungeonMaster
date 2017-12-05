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
        InvokeRepeating("Sprint", 2, 4);
    }



    protected void Sprint()
    {
        gameObject.GetComponent<AIPath>().speed += sprintBonus;
        Invoke("NormalSpeed", 1);
    }

    protected void NormalSpeed()
    {
        gameObject.GetComponent<AIPath>().speed -= sprintBonus;
    }
}
