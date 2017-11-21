﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour {

    #region member Variables
    private InputManager inputmanager;

    private SpriteRenderer sr;
    [SerializeField]
    private Sprite up;
    [SerializeField]
    private Sprite down;
    [SerializeField]
    private Sprite left;


    [SerializeField]
    private float mspeed = 0;
    [SerializeField]
    private float walkspeed = 3;
    [SerializeField]
    private float runspeed = 8;

    

    [SerializeField]
    private float staminaRecRate = 5;


    [SerializeField]
    private float dashCost = DASH_COST;
    [SerializeField]
    private float atkCost = ATTACK_COST;


    [SerializeField]
    private BasicSword weapon;
    private Vector2 atkdirection = Vector2.down;

    private Vector2 inputdirection;

    private bool isdead;

    private bool canmove = true;

    private bool isstunned = false;
    private float stuncount;
    private Vector2 stundirection = Vector2.down;
    [SerializeField]
    private float stuntime = .1f;

    private bool isdashing = false;
    private float dashcount;
    
    private Vector2 dashdirection = Vector2.down;
    [SerializeField]
    private float dashtime = .25f;


    private float health = 100;
    private float maxhealth = 100;
    private float stamina = 30;
    private float maxstamina = 30;
    
    private const float REG_HEAL = 75;
    private const float MAX_HEAL = 100;
    private const float DASH_COST = 8;
    private const float ATTACK_COST = 2;
    private bool MaxStamina;
    #endregion
    
    public float getHealth()
    {
        return health;
    }

    public float getMaxHealth()
    {
        return maxhealth;
    }

    public float getStamina()
    {
        return stamina;
    }

    public float getMaxStamina()
    {
        return maxstamina;
    }

    public bool getDead()
    {
        return isdead;
    }

    public void damage(float amt, Vector2 pushdirect)
    {
        if (!isstunned)
        {
            stuncount = stuntime;
            isdashing = false;
            dashcount = 0;
            isstunned = true;
            canmove = false;
            stundirection = pushdirect;
        }
        
        

        health -= amt;
        if (health <= 0)
        {
            isdead = true;
            canmove = false;
            isstunned = false;
        }
    }

    public void Debuff(string field, float modifier, float duration)
    {
        if (field.Equals("Slow"))
        {
            StartCoroutine(Restore(field, modifier, duration));
            mspeed *= modifier;
            walkspeed *= modifier;
            runspeed *= modifier;
        }
    }

    protected IEnumerator Restore(string field, float modifier, float duration)
    {
        yield return new WaitForSeconds(duration);
        if (field.Equals("Slow"))
        {
            mspeed /= modifier;
            walkspeed /= modifier;
            runspeed /= modifier;
        }
    }

    public void heal(float amt)
    {
        health += amt;
        if (health >= maxhealth)
        {
            health = maxhealth;
        }
    }

    public void disableMaxStamina()
    {
        dashCost = DASH_COST;
        atkCost = ATTACK_COST;
    }
    
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        MaxStamina = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Health"))
        {
            heal(REG_HEAL);
        } else if (other.gameObject.name.Contains("Stamina"))
        {
            enableMaxStamina();
        } else if (other.gameObject.name.Contains("Mana_Lock"))
        {
            GameObject.Find("DmController").GetComponent<DmController>().activateManaLock();
        }
    }

    private void enableMaxStamina()
    {
        stamina = 30;
        dashCost = 0;
        atkCost = 0;
        Invoke("disableMaxStamina", 5);
    }

    void Update () {
        //make sure we have the input manager
        if (inputmanager == null)
        {
            inputmanager = GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputManager>();
        }
        
        //update stamina
        if (stamina < maxstamina)
        {
            stamina += staminaRecRate * Time.deltaTime;
        }

        //handle being pushed from an attack
        if (isstunned)
        {
            transform.Translate(stundirection * Time.deltaTime * runspeed);
            stuncount -= Time.deltaTime;
            if (stuncount <= 0)
            {
                isstunned = false;
                canmove = true;

            }
        }

        //handle in-process dashing
        if (isdashing)
        {
            mspeed = runspeed;
            transform.Translate(dashdirection * Time.deltaTime * mspeed);
            dashcount -= Time.deltaTime;
            if (dashcount <= 0)
            {
                isdashing = false;
                canmove = true;
            }
        }

        //handle regular movement  + beginning dashing
        if (canmove)
        {
            //get input direction
            inputdirection = inputmanager.GetHeroMovement();
            if (inputdirection != Vector2.zero)
            {
                //change sprite based on direction
                if (Mathf.Abs(inputdirection.x) >= Mathf.Abs(inputdirection.y))
                {
                    //facing right
                    if (inputdirection.x > 0)
                    {
                        sr.sprite = left;
                        sr.flipX = true;
                        atkdirection = Vector2.right;
                    }
                    else //facing left
                    {
                        sr.sprite = left;
                        sr.flipX = false;
                        atkdirection = -Vector2.right;
                    }
                }
                else
                {
                    //facing up
                    if (inputdirection.y > 0)
                    {
                        sr.sprite = up;
                        sr.flipX = false;
                        atkdirection = Vector2.up;
                    }
                    else //facing down
                    {
                        sr.sprite = down;
                        sr.flipX = false;
                        atkdirection = -Vector2.up;
                    }
                }
            }
            
            //handle dash
            if (inputmanager.GetHeroDash())
            {
             if (stamina > dashCost)
                {
                    if (inputdirection != Vector2.zero)
                    {
                        dashdirection = inputdirection;
                    }
                    else
                    {
                        dashdirection = atkdirection;
                    }
                    dashcount = dashtime;
                    isdashing = true;
                    canmove = false;
                    stamina -= dashCost;
                } 
               
            }
            else
            {
                //regular move
                if (inputdirection != Vector2.zero)
                {

                    //move player
                    mspeed = walkspeed;
                    transform.Translate(inputdirection.normalized * Time.deltaTime * mspeed);

                }
            }


            //handle attacking
            if (inputmanager.GetHeroAttack())
            {
                if (!weapon.checkAttacking() && stamina > atkCost)
                {

                    weapon.doBasicAttack(atkdirection/2);
                    stamina -= atkCost;
                }
            }
            
        }

       



	}
}
