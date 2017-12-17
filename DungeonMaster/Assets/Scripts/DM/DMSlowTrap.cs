using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMSlowTrap : MonoBehaviour {

    [SerializeField]
    private GameObject slow;

    [SerializeField]
    private GameObject crate;

    [SerializeField]
    private GameObject ranged;

    [SerializeField]
    private GameObject melee;

    private int num_explosions = 20;
    private int cur_explosions = 0;

    private float radius = 4.5f;
    private Vector2 origin;

    private GameObject hero;

    // Use this for initialization
    void Start()
    {
        if (hero == null)
        {
            hero = GameObject.FindGameObjectWithTag("Hero");
            origin = hero.transform.position;
        }
        Instantiate(slow, origin, Quaternion.identity);
        generateRoom();

    }

    // Update is called once per frame
    void Update()
    {





    }

    void generateRoom()
    {
        // 7x7 room
        float botleft_x = origin.x - 4f;
        float botleft_y = origin.y - 4f;
        float offset = 3f;
        for(int i = 0; i < 9; i++)
            for(int j = 0; j < 9; j++)
                if(i == 0 || i == 8 || j == 0 || j == 8)
                    // summon arena of crates
                    Instantiate(crate, new Vector3(botleft_x + i, botleft_y + j), Quaternion.identity);
            
        
        Instantiate(slow, new Vector3(origin.x, origin.y), Quaternion.identity); // slow

        // summon monsters
        Instantiate(ranged, new Vector3(origin.x - offset, origin.y - offset), Quaternion.identity);
        Instantiate(ranged, new Vector3(origin.x - offset, origin.y + offset), Quaternion.identity);
        Instantiate(ranged, new Vector3(origin.x + offset, origin.y - offset), Quaternion.identity);
        Instantiate(ranged, new Vector3(origin.x + offset, origin.y + offset), Quaternion.identity);

        wanderingCasterMonster a = Instantiate(melee, new Vector3(origin.x - offset, origin.y), Quaternion.identity).GetComponent<wanderingCasterMonster>();
        wanderingCasterMonster b = Instantiate(melee, new Vector3(origin.x + offset, origin.y), Quaternion.identity).GetComponent<wanderingCasterMonster>();
        wanderingCasterMonster c = Instantiate(melee, new Vector3(origin.x, origin.y - offset), Quaternion.identity).GetComponent<wanderingCasterMonster>();
        wanderingCasterMonster d = Instantiate(melee, new Vector3(origin.x, origin.y + offset), Quaternion.identity).GetComponent<wanderingCasterMonster>();

        a.CastSpell();
        b.CastSpell();
        c.CastSpell();
        d.CastSpell();


        AstarPath astarscript = GameObject.FindGameObjectWithTag("A*").GetComponent<AstarPath>();
        Bounds bound = new Bounds();
        bound.center = origin;
        bound.size = new Vector3(10f,10f);
        astarscript.UpdateGraphs(bound);

        //GameObject.FindGameObjectWithTag("A*").GetComponentInChildren<AstarPath>().UpdateGraphs(bound);
    }


    /*
    void dooneexplosion()
    {
        Vector2 exploc = origin;
        exploc += new Vector2(Random.Range(-radius, radius), Random.Range(-radius, radius));
        radius -= .25f;
        if (radius <= .5f)
        {
            radius = .5f;
        }
        
        cur_explosions++;
        if (cur_explosions < num_explosions)
        {
            Invoke("dooneexplosion", .15f);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    */
}
