using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    //whether to use arrow keys for hero movement instead of gamepad joystick
    public bool useDebugHeroControls = false;

    public static InputManager instance = null;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

       
    }





    // Update is called once per frame
    void Update () {
		



	}

 
    // return X value (between -1 and 1) of hero movement input vector
    public float GetHeroMovementX()
    {
        if (useDebugHeroControls)
        {
            return Input.GetAxisRaw("HeroKeyHorizontal");
        }
        else
        {
            return Input.GetAxisRaw("HeroHorizontal");
        }
    }

    // return Y value (between -1 and 1) of hero movement input vector
    public float GetHeroMovementY()
    {
        if (useDebugHeroControls)
        {
            return Input.GetAxisRaw("HeroKeyVertical");
        }
        else
        {
            return Input.GetAxisRaw("HeroVertical");
        }

    }

    // return Vector2 of hero movement input
    public Vector2 GetHeroMovement()
    {
        return new Vector2(GetHeroMovementX(), GetHeroMovementY());
    }

}
