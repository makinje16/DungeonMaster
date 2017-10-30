using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSword : MonoBehaviour {

    private SpriteRenderer sr;
    private CircleCollider2D ccollider;

    private float atktime;
    private float atkcount;

    private bool isAttacking;

    private float atkpower;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // call damage function here, passing in atkpower
        }
    }


    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        ccollider = GetComponent<CircleCollider2D>();
        sr.enabled = false;
        ccollider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

        //if is active
		if (isAttacking)
        {
            atkcount -= Time.deltaTime;
            if (atkcount <= 0)
            {
                isAttacking = false;
                sr.enabled = false;
                ccollider.enabled = false;
            }
        }

	}

    public void doBasicAttack(Vector2 direct)
    {
        doAttack(10, .2f, direct);
    }


    public void doAttack(float power, float time, Vector2 direct)
    {
        isAttacking = true;
        atkpower = power;
        atkcount = time;
        transform.localPosition = direct;
        sr.enabled = true;
        ccollider.enabled = true;
    }


    public bool checkAttacking()
    {
        return isAttacking;
    }
}
