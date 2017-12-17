using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSword : MonoBehaviour {

    private SpriteRenderer sr;
    private CircleCollider2D ccollider;

    [SerializeField]
    private GameObject crateexp;

    private float atktime;
    private float atkcount;

    private bool isAttacking;

    private bool isSpin;

    private float spinatktime = .3f;

    private float atkpower;

    private float BASIC_ATTACK = 20f;
    private float STRONG_ATTACK = 30f;
    private float bonusAttack;
    private const float BONUS_ATTACK_TIME = 15f;

    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Monster collidedMonster = collision.gameObject.GetComponent<Monster>();
            Vector2 pushdirection = collidedMonster.transform.position - transform.position;
            pushdirection.Normalize();
            collidedMonster.Damage(atkpower, pushdirection);
           
        }

        if (collision.gameObject.GetComponent<breakableCrate>() != null)
        {
            Destroy(collision.gameObject);
            Instantiate(crateexp, collision.transform.position, Quaternion.identity);

            // recalculate bounds when crates break
            AstarPath astarscript = GameObject.FindGameObjectWithTag("A*").GetComponentInChildren<AstarPath>();
            Bounds b = new Bounds();
            b.center = collision.gameObject.GetComponent<BoxCollider2D>().transform.position;
            b.size = collision.gameObject.GetComponent<BoxCollider2D>().transform.position;
            astarscript.UpdateGraphs(b);
            //GameObject.FindGameObjectWithTag("A*").GetComponent<AstarPath>().UpdateGraphs(b);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<breakableCrate>() != null)
        {
            Destroy(collision.gameObject);
            Instantiate(crateexp, collision.transform.position, Quaternion.identity);
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
        doAttack(BASIC_ATTACK, .2f, direct);
    }

    public void doStrongAttack(Vector2 direct)
    {
        isSpin = true;
        doAttack(STRONG_ATTACK, spinatktime, direct);
    }

    public void boostAttack(float amount)
    {
        BASIC_ATTACK += amount;
        STRONG_ATTACK += amount;
        bonusAttack = amount;
        Invoke("resetAttack", BONUS_ATTACK_TIME);
    }

    private void resetAttack()
    {
        BASIC_ATTACK -= bonusAttack;
        STRONG_ATTACK -= bonusAttack;
        bonusAttack = 0;
    }

    public void doAttack(float power, float time, Vector2 direct)
    {
        isAttacking = true;
        atkpower = power;
        atkcount = time;
        transform.localPosition = direct;
     //   sr.enabled = true;
        ccollider.enabled = true;
        GetComponent<AudioSource>().Play();
    }

    public bool checkAttacking()
    {
        return isAttacking;
    }
}
