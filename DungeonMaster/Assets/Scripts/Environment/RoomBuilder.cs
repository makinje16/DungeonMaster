using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBuilder : MonoBehaviour {

    [SerializeField]
    private GameObject rockbase;

    [SerializeField]
    private int width;
    [SerializeField]
    private int height;

    [SerializeField]
    private float startx;

    [SerializeField]
    private float starty;


	// Use this for initialization
	void Start () {
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
