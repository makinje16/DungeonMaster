using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seekingMonster : MonoBehaviour {

    [SerializeField]
    private float movementSpeed;
    private Vector2 direction;
    private GameObject hero;

    void Start()
    {
        hero = GameObject.FindWithTag("Hero");
        direction = hero.transform.position - transform.position;
        movementSpeed = 2;
    }

    void Update()
    {
        direction = hero.transform.position - transform.position;
        transform.Translate(direction.normalized * Time.deltaTime * movementSpeed);
    }
}
