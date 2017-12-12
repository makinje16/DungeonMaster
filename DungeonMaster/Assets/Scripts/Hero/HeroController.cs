using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroController : MonoBehaviour {

    #region member Variables
    private InputManager inputmanager;

    private SpriteRenderer sr;
	private Animator animator;
    /*    [SerializeField]
        private Sprite up;
        [SerializeField]
        private Sprite down;
        [SerializeField]
        private Sprite left;
    */


    [SerializeField]
    private GameObject summonefx;
    [SerializeField]
    private Slider HeroHealth;
    [SerializeField]
    private Slider HeroMana;


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
    private AudioClip healSound;

    [SerializeField]
    private AudioClip staminaSound;

    [SerializeField]
    private AudioClip objectiveSound;

    [SerializeField]
    private AudioClip manaLockSound;

    [SerializeField]
    private BasicSword weapon;
    private Vector2 atkdirection = Vector2.down;

    private Vector2 inputdirection;

    private bool isdead;

    private bool invincible = false;
    private float invincibleCount = 0;


    private bool canmove = true;

    private bool isstunned = false;
    private float stuncount;
    private Vector2 stundirection = Vector2.down;
    [SerializeField]
    private float stuntime = .1f;

    private bool isdashing = false;
    private float dashcount;
    
    private Vector2 dashdirection = Vector2.down;

    private float dashtime = .3f;


    private float health = 50;
    private float maxhealth = 50;
    private float stamina = 30;
    private float maxstamina = 30;

    private int lives = 10;

    private const float INV_FRAMES = .25f; 
    private const float REG_HEAL = 30;
    private const float MAX_HEAL = 100;
    private const float DASH_COST = 8;
    private const float ATTACK_COST = 2;
    private const float ATTACK_BONUS = 15;
    private bool MaxStamina;

    private RaycastHit2D rc;
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
		animator = GetComponent<Animator>();
		animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        MaxStamina = false;
    }

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
            increaseAttack();
        }
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


    private void FixedUpdate()
    {
        
    }

    void Update () {

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
            mspeed = runspeed * 2f;

            if (rc = Physics2D.Raycast(transform.position + new Vector3(dashdirection.x/2f,dashdirection.y/2f), dashdirection,1f, ~LayerMask.GetMask("Obstacle")))
            {

                    if (rc.collider.CompareTag("Rock"))
                    dodash = false;

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
			if (inputdirection != Vector2.zero) {
				//change sprite based on direction
				if (Mathf.Abs (inputdirection.x) >= Mathf.Abs (inputdirection.y)) {
					//facing right
					if (inputdirection.x > 0) {
						//sr.sprite = left;
						sr.flipX = false;
						atkdirection = Vector2.right;
						animator.SetBool ("IsFacingUp", false);
						animator.SetBool ("IsFacingSide", true);
						animator.SetBool ("IsFacingDown", false);
						animator.SetBool ("IsMoving", true);
					} else { //facing left
						//sr.sprite = left;
						sr.flipX = true;
						atkdirection = -Vector2.right;
						animator.SetBool ("IsFacingUp", false);
						animator.SetBool ("IsFacingSide", true);
						animator.SetBool ("IsFacingDown", false);
						animator.SetBool ("IsMoving", true);
					}
				} else {
					//facing up
					if (inputdirection.y > 0) {
						//sr.sprite = up;
						sr.flipX = false;
						atkdirection = Vector2.up;
						animator.SetBool ("IsFacingUp", true);
						animator.SetBool ("IsFacingSide", false);
						animator.SetBool ("IsFacingDown", false);
						animator.SetBool ("IsMoving", true);
					} else { //facing down
						//sr.sprite = down;
						sr.flipX = false;
						atkdirection = -Vector2.up;
						animator.SetBool ("IsFacingUp", false);
						animator.SetBool ("IsFacingSide", false);
						animator.SetBool ("IsFacingDown", true);
						animator.SetBool ("IsMoving", true);
					}
				}
			} else {
				animator.SetBool ("IsMoving", false);
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
                    weapon.doStrongAttack(atkdirection/1.5f);
                    stamina -= (5 * atkCost);
					animator.SetTrigger ("OnSwingAttack");
                }
            }
            if (inputmanager.GetHeroAttack())
            {
                if (!weapon.checkAttacking() && stamina > atkCost)
                {

                    weapon.doBasicAttack(atkdirection/1.5f);
                    stamina -= atkCost;
					animator.SetTrigger ("OnAttack");
                }
            }
            
        }

       



	}
}
