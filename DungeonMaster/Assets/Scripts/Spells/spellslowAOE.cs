using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellslowAOE : MonoBehaviour
{
    [SerializeField]
    private float attackPower = 0;
    [SerializeField]
    private float aoeCD = 0;
    [SerializeField]
    private float time = 1;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
            other.gameObject.GetComponent<HeroController>().Debuff("Slow", 0.5f, 2.5f);
            aoeCD = 1;
        }else if(other.CompareTag("Monster")) {
            other.gameObject.GetComponent<HeroController>().Debuff("Slow", 0.5f, 2.5f);
            aoeCD = 1;
        }
    }
}
