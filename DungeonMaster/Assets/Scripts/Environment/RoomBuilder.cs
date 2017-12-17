using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBuilder : MonoBehaviour {

    [SerializeField]
    private bool dobuild = false;

    [SerializeField]
    private bool dofloor = true;

    [SerializeField]
    private GameObject rockbase;

    [SerializeField]
    private GameObject floorbase;


    [SerializeField]
    private int width;
    [SerializeField]
    private int height;

    [SerializeField]
    private float startx;

    [SerializeField]
    private float starty;

    [SerializeField]
    private float floorstartx;

    [SerializeField]
    private float floorstarty;


    // Use this for initialization
    void Start () {

        if (dobuild) makeroom();

        if (dofloor) makefloor();


	}

    void makefloor()
    {
        GameObject temprock;
        for (int yn = 0; yn <=8; yn++)
        {

            for (int xn = 0; xn <=8; xn++)
            {
              
                    temprock = Instantiate(floorbase);
                    temprock.transform.position = new Vector2(floorstartx + xn*10, floorstarty + yn*10);
                
            }

        }
    }

    void makeroom()
    {
        GameObject temprock;
        for (int yn = 0; yn <= height; yn++)
        {

            for (int xn = 0; xn <= width; xn++)
            {
                if (xn == 0 || xn == width || yn == 0 || yn == height)
                {
                    temprock = Instantiate(rockbase);
                    temprock.transform.position = new Vector2(startx + xn, starty + yn);
                }
            }

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
