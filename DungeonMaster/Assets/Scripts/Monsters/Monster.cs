using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

abstract public class Monster : MonoBehaviour {

    [SerializeField]
    protected float health = 50;
    [SerializeField]
    protected float movementSpeed = 2;
    [SerializeField]
    protected float attackPower = 5;
    protected GameObject hero;
    [SerializeField]
    protected float stunDuration = .25f;
    [SerializeField]
    protected bool isStunned = false;

    [SerializeField]
    protected Vector2 direction;
    protected Vector2 stundirection;

    [SerializeField] 
    private GameObject[] dropableItems;

    // Update is called once per frame
    public void Damage(float attack, Vector2 hitdirection)
    {
        stundirection = hitdirection;
        isStunned = true;
        health -= attack;
        GetComponent<AudioSource>().Play();
        if (health <= 0)
        {
            DropItem();
            Destroy(gameObject);
        }
    }

    public void Debuff(string field, float modifier ,float duration)
    {
        if (field.Equals("Slow"))
        {
            StartCoroutine(Restore(field, modifier, duration));
            movementSpeed *= modifier;
        }
    }

    protected IEnumerator Restore(string field, float modifier, float duration)
    {
        yield return new WaitForSeconds(duration);
        if (field.Equals("Slow"))
        {
            movementSpeed /= modifier;
        }
    }

    protected void DropItem()
    {
        System.Random random = new System.Random();
        float index = random.Next(0, 11);
        if (index <= 3)
        {
            Instantiate(dropableItems[0], transform.position, Quaternion.identity);
            return;
        }
        if (index <= 6)
        {
            Instantiate(dropableItems[1], transform.position, Quaternion.identity);
            return;
        }
    }
    
    protected virtual void Move()
    {
        transform.Translate(direction.normalized * Time.deltaTime * movementSpeed);
    }

    protected virtual void Update()
    {
        if (isStunned)// stunned knockback
        {
            transform.Translate(stundirection * Time.deltaTime * (movementSpeed + 3));
            stunDuration -= Time.deltaTime;
            if (stunDuration <= 0)
            {
                stunDuration = .25f;
                isStunned = false;
            }
        }
        else // regular movement
        {
            Move();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            Vector2 pushdirection = other.gameObject.transform.position - transform.position;
            pushdirection.Normalize();
            other.gameObject.GetComponent<HeroController>().damage(attackPower, pushdirection);
        }
    }
}
