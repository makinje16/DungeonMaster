using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour {

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
    private float runspeed = 5;


    private Vector2 inputdirection;

    private bool canmove = true;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }


 
    void Update () {
        //make sure we have the input manager
        if (inputmanager == null)
        {
            inputmanager = GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputManager>();
        }


        if (canmove)
        {
            //get input direction
            inputdirection = inputmanager.GetHeroMovement();
            if (inputdirection != Vector2.zero)
            {

                //move player
                mspeed = walkspeed;
                transform.Translate(inputdirection.normalized * Time.deltaTime * mspeed);

                //change sprite based on direction
                if (Mathf.Abs(inputdirection.x) >= Mathf.Abs(inputdirection.y))
                {
                    //facing right
                    if (inputdirection.x > 0)
                    {
                        sr.sprite = left;
                        sr.flipX = true; ;
                    }
                    else //facing left
                    {
                        sr.sprite = left;
                        sr.flipX = false;
                    }
                }
                else
                {
                    //facing up
                    if (inputdirection.y > 0)
                    {
                        sr.sprite = up;
                        sr.flipX = false;
                    }
                    else //facing down
                    {
                        sr.sprite = down;
                        sr.flipX = false;
                    }
                }
            }
        }
       





	}
}
