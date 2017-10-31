using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Monster : MonoBehaviour {

    [SerializeField]
    protected float health = 50;
    [SerializeField]
    protected float movementSpeed = 2;
    [SerializeField]
    protected float attackPower = 5;
    protected GameObject hero;

    // Update is called once per frame
    public void Damage(float attack)
    {
        health -= attack;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            other.gameObject.GetComponent<HeroController>().damage(attackPower);
        }
    }
}
