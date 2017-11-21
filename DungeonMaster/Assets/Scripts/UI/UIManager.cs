﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField]
    private Slider heroHealthSlider;
    [SerializeField]
    private Slider heroStaminaSlider;
    [SerializeField]
    private Slider dmManaSlider;
    [SerializeField]
    private Image heroInvImg;

    [SerializeField]
    private GameObject[] monstericons;

    [SerializeField]
    private GameObject[] trapicons;


    [SerializeField]
    private GameObject gameoverscreen;

    [SerializeField]
    private Text winconditiontext;

    private HeroController hero;
    private DmController dmcontroller;




    
	// Use this for initialization
	void Start () {
        heroInvImg.enabled = false;
	}
    
    //update the win condition text
    public void updateWinConditionText(string newtext)
    {
        winconditiontext.text = newtext;
    }

    // Update is called once per frame
    void Update() {
        //get a reference to the hero
        if (hero == null)
        {
            hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<HeroController>();
        }

        //get a reference to the DM controller?
        if (dmcontroller == null)
        {
          dmcontroller = GameObject.FindGameObjectWithTag("DMController").GetComponent<DmController>();
        }

        //manage hero health slider
        heroHealthSlider.maxValue = hero.getMaxHealth();
        heroHealthSlider.value = hero.getHealth();

        //manage hero stamina slider
        heroStaminaSlider.maxValue = hero.getMaxStamina();
        heroStaminaSlider.value = hero.getStamina();

        //manage DM mana slider
        dmManaSlider.maxValue = dmcontroller.GetMaxMana();
        dmManaSlider.value = dmcontroller.GetManaCount();


        //manage DM monster icons
        for (int i = 0; i < monstericons.Length; i++)
        {
            if (dmcontroller.GetManaCount() > ((i + 1) * 10))
            {
                monstericons[i].GetComponentInChildren<Text>().color = Color.cyan;
            }
            else
            {
                monstericons[i].GetComponentInChildren<Text>().color = Color.black;
            }
        }

        //manage DM trap icons
        for (int i = 0; i < trapicons.Length; i++)
        {
            if (dmcontroller.GetManaCount() > ((i *10) + 20))
            {
                trapicons[i].GetComponentInChildren<Text>().color = Color.cyan;
            }
            else
            {
                trapicons[i].GetComponentInChildren<Text>().color = Color.black;
            }
        }

        //check if we need to show the game-over screen
        if (hero.getDead())
        {
            gameoverscreen.SetActive(true);
        }


	}


}
