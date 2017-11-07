using System.Collections;
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
    private GameObject gameoverscreen;

    [SerializeField]
    private Text winconditiontext;

    private HeroController hero;
    private DmController dmcontroller;
    
	// Use this for initialization
	void Start () {
		
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

        //check if we need to show the game-over screen
        if (hero.getDead())
        {
            gameoverscreen.SetActive(true);
        }


	}


}
