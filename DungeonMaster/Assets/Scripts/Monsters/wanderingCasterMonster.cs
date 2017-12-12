using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wanderingCasterMonster : wanderingMonster {
	private Animator animator;
    [SerializeField]
    private GameObject mydeathefx;
    private float castingCD = 4;
    private bool canCast = false;
    private float castingTime = 2;

    [SerializeField]
    private GameObject spell1;

    [SerializeField]
    private GameObject spell2;

    [SerializeField]
    private Sprite normalSprite;

    [SerializeField]
    private Sprite castingSprite;


    private SpriteRenderer sr;
    
	// Use this for initialization
	void Start () {
        movementSpeed = 1.25f;
        sr = GetComponentInChildren<SpriteRenderer>();
		animator = GetComponent<Animator> ();
        hero = GameObject.FindWithTag("Hero");
        direction = hero.transform.position - transform.position;
        Invoke("ChangeDirection", Random.Range(2, 5));
    }

    // Update is called once per frame

    protected override void Move()
    {
        transform.Translate(direction.normalized * Time.deltaTime * movementSpeed);
		if (direction.x >= 0) {
			sr.flipX = false;
		} else {
			sr.flipX = true;
		}
    }

    protected void CastSpell()
    {
        if (Random.Range(0.0f, 1.0f) <= 0.5)
        {
            Instantiate(spell1, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            //sr.sprite = normalSprite;
        }
        else
        {
            Instantiate(spell2, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            //sr.sprite = normalSprite;
        }
    }

    protected override void Update()
    {
        if (health <= 0)
        {
            Instantiate(mydeathefx, transform.position, Quaternion.identity);
            DropItem();
            GetComponent<AudioSource>().PlayOneShot(deathSound);
            StartCoroutine(DestroyThis());
            health = 100;
        }
        if (isStunned)// stunned knockback
        {
            transform.Translate(stundirection * Time.deltaTime * (movementSpeed + 3));
            stunDuration -= Time.deltaTime;
            sr.color = Color.red;
            if (stunDuration <= 0)
            {
                sr.color = Color.white;
                stunDuration = .25f;
                isStunned = false;
            }
            
        }
        else // regular movement
        {

            if (castingCD <= 0) // Can cast a spell
            {
                //sr.sprite = castingSprite;
				animator.SetBool("IsCasting", true);
                if (castingTime <= 0) // CAST THE SPELL
                {
                    CastSpell();
                    castingTime = 2;
                    castingCD = 4;
                }
                else
                {
                    castingTime -= Time.deltaTime; // casting the spell
                }
            }
            else
            {
				animator.SetBool ("IsCasting", false);
                castingCD -= Time.deltaTime;
                Move();
            }
            
        }
    }

    protected override void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            Vector2 pushdirection = other.gameObject.transform.position - transform.position;
            pushdirection.Normalize();
            other.gameObject.GetComponent<HeroController>().damage(attackPower, pushdirection * (5/4));
        }
    }
}
