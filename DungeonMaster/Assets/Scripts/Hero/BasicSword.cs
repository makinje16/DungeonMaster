﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSword : MonoBehaviour {

    private SpriteRenderer sr;
    private CircleCollider2D ccollider;

    private float atktime;
    private float atkcount;

    private bool isAttacking;

    private bool isSpin;

    private float spinatktime = .3f;

    private float atkpower;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Monster collidedMonster = collision.gameObject.GetComponent<Monster>();
            Vector2 pushdirection = collidedMonster.transform.position - transform.position;
            pushdirection.Normalize();
            collidedMonster.Damage(atkpower, pushdirection);
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

            if (isSpin)
            {
                transform.localPosition = new Vector2(Mathf.Cos((8 * Mathf.PI) * (spinatktime - atkcount))*.5f, Mathf.Sin((8* Mathf.PI) * (spinatktime - atkcount))*.5f);
            }

            if (atkcount <= 0)
            {
                isAttacking = false;
                sr.enabled = false;
                ccollider.enabled = false;
                isSpin = false;
            }
        }

	}

    public void doBasicAttack(Vector2 direct)
    {
        doAttack(10, .2f, direct);
    }

    public void doStrongAttack(Vector2 direct)
    {
        isSpin = true;
        doAttack(20, spinatktime, direct);
    }


    public void doAttack(float power, float time, Vector2 direct)
    {
        isAttacking = true;
        atkpower = power;
        atkcount = time;
        transform.localPosition = direct;
        sr.enabled = true;
        ccollider.enabled = true;
        GetComponent<AudioSource>().Play();
    }

    


    public bool checkAttacking()
    {
        return isAttacking;
    }
}
