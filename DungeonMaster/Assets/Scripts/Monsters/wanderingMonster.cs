using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wanderingMonster : MonoBehaviour {

    [SerializeField]
    private float movementSpeed;
    private Vector2 direction;
    private GameObject hero;

    void Start()
    {
        hero = GameObject.FindWithTag("Hero");
        direction = hero.transform.position - transform.position;
        movementSpeed = 2;
        //direction = Vector3.left;
        Invoke("ChangeDirection", Random.Range(1,3));
    }



    void ChangeDirection()
    {
        direction = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        Invoke("ChangeDirection", Random.Range(1, 3));
    }

    void Update()
    {
        //direction = hero.transform.position - transform.position;
        transform.Translate(direction.normalized * Time.deltaTime * movementSpeed);
    }
}
