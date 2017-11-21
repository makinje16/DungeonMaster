using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spelldmgAOE : MonoBehaviour {
    [SerializeField]
    private float attackPower = 12;
    [SerializeField]
    private float aoeCD = 0;
    [SerializeField]
    private float time = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (time < 0)
            Destroy(gameObject);
        if (aoeCD > 0)
            aoeCD -= Time.deltaTime;
        time -= Time.deltaTime;
	}

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            Vector2 pushdirection = other.gameObject.transform.position - transform.position;
            pushdirection.Normalize();
            other.gameObject.GetComponent<HeroController>().damage(attackPower, pushdirection * 1.5f);
            aoeCD = 1;
        }
    }
}
