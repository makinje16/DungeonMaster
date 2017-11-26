using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    //whether to use arrow keys for hero movement instead of gamepad joystick
    public bool useDebugHeroControls = false;

    public static InputManager instance = null;

	public bool isOsx = false;

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

		if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)
			isOsx = true;
		else
			isOsx = false;
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
		if (useDebugHeroControls) {
			return Input.GetKeyDown (KeyCode.RightShift);
		} 
		else if (isOsx) {
			return Input.GetButtonDown ("HeroX_OSX");
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
		else if (isOsx) {
			return Input.GetButtonDown ("HeroB_OSX");
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
		else if (isOsx) {
			return Input.GetButtonDown ("HeroA_OSX");
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

	public string GetDmMonsterZone() {
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
		} else
			return "";
	}

	public int GetDmSpell() {
		if (Input.GetKeyDown ("z")) {
			return 1;
		} else if (Input.GetKeyDown ("x")) {
			return 2;
		} else if (Input.GetKeyDown ("c")) {
			return 3;
		} else if (Input.GetKeyDown ("v")) {
			return 4;
		} else
			return -1;
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
