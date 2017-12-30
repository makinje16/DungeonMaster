﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HeroController : NetworkBehaviour {

#region member Variables
    private InputManager inputmanager;

    private SpriteRenderer sr;
	private Animator animator;

    [SerializeField] private Behaviour[] componentsToDisable;

    [SerializeField] private GameObject summonefx;
    [SerializeField] private GameObject[] gameObjectsToDisable;
    
    [SerializeField] private Slider HeroHealth;
    [SerializeField] private Slider HeroMana;

    [SerializeField] private int lives = 10;
    
    [SerializeField] private float mspeed = 0;
    [SerializeField] private float walkspeed = 3;
    [SerializeField] private float runspeed = 8;
    [SerializeField] private float staminaRecRate = 5;
    [SerializeField] private float dashCost = DASH_COST;
    [SerializeField] private float atkCost = ATTACK_COST;
    [SerializeField] private float stuntime = .1f;
    
    [SerializeField] private AudioClip healSound;
    [SerializeField] private AudioClip staminaSound;
    [SerializeField] private AudioClip objectiveSound;
    [SerializeField] private AudioClip manaLockSound;
    [SerializeField] private AudioClip powerupSound;

    [SerializeField] private BasicSword weapon;
    
    private Vector2 atkdirection = Vector2.down;
    private Vector2 inputdirection;
    private Vector2 stundirection = Vector2.down;
    private Vector2 dashdirection = Vector2.down;

    private bool isdead;
    private bool invincible = false;
    private bool canmove = true;
    private bool isstunned = false;
    private bool isdashing = false;
    private bool MaxStamina;

    private float stuncount;
    private float dashcount;
    private float invincibleCount = 0;
    private float dashtime = .3f;

    [SyncVar] private float health = 50;
    private float maxhealth = 50;
    private float stamina = 30;
    [SyncVar] private float maxstamina = 30;
    
    private const float INV_FRAMES = .25f; 
    private const float REG_HEAL = 30;
    private const float MAX_HEAL = 100;
    private const float DASH_COST = 8;
    private const float ATTACK_COST = 2;
    private const float ATTACK_BONUS = 15;

    private RaycastHit2D rc;
#endregion

#region variable accessors

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
    
#endregion

#region member functions
    
    public void damage(float amt, Vector2 pushdirect)
    {
        if (!invincible  && !isdashing)
        {
            invincible = true;
            invincibleCount = INV_FRAMES;

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

                lives--;
                if (lives <= 0)
                {
                    isdead = true;
                }
                resethero();
            }
        }

       
    }

    public int getLives()
    {
        return lives;
    }

    private void resethero()
    {
        GameObject.Instantiate(summonefx, transform.position, Quaternion.identity);
        canmove = true;
        isstunned = false;
        stamina = maxstamina;
        health = maxhealth;
    }

    public void Debuff(string field, float modifier, float duration)
    {
        if (field.Equals("Slow"))
        {
            StartCoroutine(Restore(field, modifier, duration));
            mspeed *= modifier;
            walkspeed *= modifier;
            runspeed *= modifier;
            sr.color = new Color(.5f,.4f,1f);
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
            sr.color = Color.white;
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

    private void enableMaxStamina()
    {
        stamina = 30;
        dashCost = 0;
        atkCost = 0;
        Invoke("disableMaxStamina", 5);
    }

    public Slider getHeroHealthSlider()
    {
        return HeroHealth;
    }

    public Slider getHeroStaminaSlider()
    {
        return HeroMana;
    }

    private void increaseAttack()
    {
        weapon.boostAttack(ATTACK_BONUS);
    }

    [Command]
    public void CmdFlipSprite(bool flip)
    {
        RpcFlipSprite(flip);
    }

    [ClientRpc]
    public void RpcFlipSprite(bool flip)
    {
        sr.flipX = flip;
    }

    [Command]
    public void CmdSetHealth(float value)
    {
        RpcSetHealth(value);
    }

    [ClientRpc]
    public void RpcSetHealth(float value)
    {
        HeroHealth.value = value * 2;
    }

    [Command]
    public void CmdSetStamina(float value)
    {
        RpcSetStamina(value);
    }

    [ClientRpc]
    public void RpcSetStamina(float value)
    {
        HeroMana.value = value;
    }
    
 #endregion

#region Unity functions
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Health"))
        {
            heal(REG_HEAL);
            GetComponent<AudioSource>().PlayOneShot(healSound);
        } else if (other.gameObject.name.Contains("Stamina"))
        {
            GetComponent<AudioSource>().PlayOneShot(staminaSound);
            enableMaxStamina();
        } else if (other.gameObject.name.Contains("Mana_Lock"))
        {
            GetComponent<AudioSource>().PlayOneShot(manaLockSound);
            GameObject.Find("DmController").GetComponent<DmController>().activateManaLock();
        }
        else if (other.gameObject.name.Contains("Objective"))
        {
            GetComponent<AudioSource>().PlayOneShot(objectiveSound);
        }else if (other.gameObject.name.Contains("Attack"))
        {
            GetComponent<AudioSource>().PlayOneShot(powerupSound);
            increaseAttack();
        }
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        MaxStamina = false;

        if (isLocalPlayer){return;}
        foreach (Behaviour component in componentsToDisable)
        {
            component.enabled = false;
        }
        foreach (GameObject gameObj in gameObjectsToDisable)
        {
            gameObj.SetActive(false);
        }
    }
    
    void Update()
    {
        if (isLocalPlayer)
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
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

            if (invincible)
            {
                invincibleCount -= Time.deltaTime;
                if (invincibleCount <= 0)
                {
                    invincible = false;
                    Debug.Log("uninvincible");
                }
            }

            //handle in-process dashing
            if (isdashing)
            {
                bool dodash = true;
                mspeed = runspeed * 1.5f;
                Debug.DrawRay(transform.position + new Vector3(dashdirection.x / 2f, dashdirection.y / 2f),
                    dashdirection);
                if (rc = Physics2D.Raycast(transform.position + new Vector3(dashdirection.x / 2f, dashdirection.y / 2f),
                    dashdirection, mspeed, ~LayerMask.GetMask("Obstacle")))
                {

                    if (rc.collider.CompareTag("Rock"))
                    {
                        dodash = false;
                    }


                    Debug.Log("attempted to dash, collided with " + rc.collider.tag);


                }
                if (dodash)
                {
                    transform.Translate(dashdirection * Time.deltaTime * mspeed);
                }

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
                            CmdFlipSprite(false);
                            atkdirection = Vector2.right;
                            animator.SetBool("IsFacingUp", false);
                            animator.SetBool("IsFacingSide", true);
                            animator.SetBool("IsFacingDown", false);
                            animator.SetBool("IsMoving", true);
                        }
                        else
                        {
                            CmdFlipSprite(true);
                            atkdirection = -Vector2.right;
                            animator.SetBool("IsFacingUp", false);
                            animator.SetBool("IsFacingSide", true);
                            animator.SetBool("IsFacingDown", false);
                            animator.SetBool("IsMoving", true);
                        }
                    }
                    else
                    {
                        //facing up
                        if (inputdirection.y > 0)
                        {
                            atkdirection = Vector2.up;
                            animator.SetBool("IsFacingUp", true);
                            animator.SetBool("IsFacingSide", false);
                            animator.SetBool("IsFacingDown", false);
                            animator.SetBool("IsMoving", true);
                        }
                        else
                        {
                            atkdirection = -Vector2.up;
                            animator.SetBool("IsFacingUp", false);
                            animator.SetBool("IsFacingSide", false);
                            animator.SetBool("IsFacingDown", true);
                            animator.SetBool("IsMoving", true);
                        }
                    }
                }
                else
                {
                    animator.SetBool("IsMoving", false);
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
                if (inputmanager.GetHeroStrAttack())
                {
                    if (!weapon.checkAttacking() && stamina > (5 * atkCost))
                    {
                        weapon.doStrongAttack(atkdirection / 1.5f);
                        stamina -= (5 * atkCost);
                        animator.SetTrigger("OnSwingAttack");
                    }
                }
                if (inputmanager.GetHeroAttack())
                {
                    if (!weapon.checkAttacking() && stamina > atkCost)
                    {

                        weapon.doBasicAttack(atkdirection / 1.5f);
                        stamina -= atkCost;
                        animator.SetTrigger("OnAttack");
                    }
                }
            }
            CmdSetHealth(health * 2);
            CmdSetStamina(stamina);
        }
    }

#endregion
}
