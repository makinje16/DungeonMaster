using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spelldmgMissile : MonoBehaviour
{

    [SerializeField]
    Vector2 direction = Vector2.up;

    [SerializeField]
    float attackPower = 10;

    [SerializeField]
    float speed = 5;

    void Start()
    {
        
    }

    // Use this for initialization
    public void setDirection(Vector2 dir)
    {
        direction = dir;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction.normalized * Time.deltaTime * speed);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Hero"))
        {
            Vector2 pushdirection = other.gameObject.transform.position - transform.position;
            pushdirection.Normalize();
            other.gameObject.GetComponent<HeroController>().damage(attackPower, pushdirection * 0.4f);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
