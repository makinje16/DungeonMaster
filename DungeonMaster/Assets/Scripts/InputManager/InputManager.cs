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

        if (Input.GetJoystickNames().Length == 0 || Input.GetJoystickNames()[0] == "")
        {
            useDebugHeroControls = true;
        }
        else
        {
            useDebugHeroControls = false;
        }


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

    // return True if dash button was pressed Down this frame
    public bool GetHeroDash()
    {
        if (useDebugHeroControls)
        {
            return Input.GetKeyDown(KeyCode.RightShift);
        }
        else
        {
            return Input.GetButtonDown("HeroX");
        }
    }

    //returns True if strong attack button was pressed down this frame
    public bool GetHeroStrAttack()
    {
        if (useDebugHeroControls)
        {
            return Input.GetKeyDown(KeyCode.Return);
        }
        else
        {
            return Input.GetButtonDown("HeroB");
        }
    }

    //return True if attack button was pressed down this frame
    public bool GetHeroAttack()
    {
        if (useDebugHeroControls)
        {
            return Input.GetKeyDown(KeyCode.RightControl);
        }
        else
        {
            return Input.GetButtonDown("HeroA");
        }
    }

	public Vector3? GetDmMouseClick()
	{
		if (Input.GetMouseButtonDown (0)) {
			Vector3 pos = Input.mousePosition;
			pos.z = 10;
			return Camera.main.ScreenToWorldPoint (pos);
		}
		else
			return null;
	}

	public string GetDmKey() {
		if (Input.GetKeyDown ("q")) {
			return "q";
		} else if (Input.GetKeyDown ("w")) {
			return "w";
		} else if (Input.GetKeyDown ("e")) {
			return "e";
		} else if (Input.GetKeyDown ("a")) {
			return "a";
		} else if (Input.GetKeyDown ("s")) {
			return "s";
		} else if (Input.GetKeyDown ("d")) {
			return "d";
		} else if (Input.GetKeyDown ("t")) {
			return "t";
		} else
			return "";
	}

	public int GetDmNum() {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			return 1;
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			return 2;
		} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			return 3;
		} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
			return 4;
		} else
			return -1;
	}
}
