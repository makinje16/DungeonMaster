using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seekingRangedMonster : seekingMonster {

    private SpriteRenderer spr;
	private Animator animator;

    [SerializeField]
    protected bool canSee = false;

    [SerializeField]
    protected float minDist = 9;

    [SerializeField]
    float distance;

    [SerializeField]
    float initialCD = 2.5f;
    float cd;
    bool canAttack = true;
    bool isAttacking = false;

    [SerializeField]
    float animationDelay = 1f;
    float animationTime = 0;

    [SerializeField]
    GameObject missile;
    // Use this for initialization
    void Start () {
        hero = GameObject.FindWithTag("Hero");
        direction = hero.transform.position - transform.position;
        spr = GetComponentInChildren<SpriteRenderer>();
        isSeeking = true;
        keepDistance = true;
        cd = initialCD;
        animationTime = animationDelay;
        distance = Vector2.Distance(hero.transform.position, transform.position);
		animator = GetComponent<Animator>();
    }
	
    void fireProjectile(Vector2 dir)
    {
        GameObject projectile = Instantiate(missile, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
        projectile.GetComponent<spelldmgMissile>().setDirection(dir);
        
    }

	// Update is called once per frame
	protected override void Move () {
        direction = hero.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.CircleCast(new Vector2(transform.position.x, transform.position.y), .3f, direction);
        if (hit.collider.gameObject.tag == "Hero") // sees if the monster has line of sight of hero
            canSee = true;
        else if (hit.collider.gameObject.tag == "Rock") // any obstacle
            canSee = false;

        distance = Vector2.Distance(hero.transform.position, transform.position);
        if (!canAttack)
        {
            cd -= Time.deltaTime; // recharging attack
            if (cd <= 0) // off cd
            {
                canAttack = true;
                cd = initialCD;
            }
        }
        if (isAttacking)
        {
            animationTime -= Time.deltaTime;
            if (animationTime <= 0) // ready to fire
            {
                fireProjectile(direction);
                isAttacking = false;
                canAttack = false;
                animationTime = animationDelay;
				//animator.SetBool ("IsAttacking", false);
            }
        }

        if (canSee)
        {
            if(distance <= minDist) // within distance, stop pursuing and attack
            {
                gameObject.GetComponent<AIPath>().canMove = false;
                if (canAttack)
                {
                    isAttacking = true;         
					animator.SetBool ("IsAttacking", true);
                }
            }
            else if(!isAttacking)// keep chasing
            {
                gameObject.GetComponent<AIPath>().canMove = true;
            }
        }
        else if(!isAttacking)// find path to hero
        {
            gameObject.GetComponent<AIPath>().canMove = true;
        }
        
        if (direction.x >= 0)
        {
			spr.flipX = true;
        }
        else
        {
			spr.flipX = false;
        }
    }
}
