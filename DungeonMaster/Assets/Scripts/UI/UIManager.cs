using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    [SerializeField]
    private Slider heroHealthSlider;
    [SerializeField]
    private Slider heroStaminaSlider;
    [SerializeField]
    private Slider dmManaSlider;
    [SerializeField]
    private Image heroInvImg;


    private gamecontroller gc;
    private gamecontroller.DMAbilities currentabilities;

    [SerializeField]
    private GameObject infinitemana;

    [SerializeField]
    private GameObject[] monstericons;

    [SerializeField]
    private GameObject[] trapicons;

    [SerializeField]
    private GameObject manaspellicon;


    [SerializeField]
    private GameObject gameoverscreen;

    [SerializeField]
    private GameObject herowinscreen;

    [SerializeField]
    private Text winconditiontext;
    [SerializeField]
    private Text livestext;

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

    public void herowin()
    {
        herowinscreen.SetActive(true);
        Invoke("nextlevel", 3f);
    }

    private void nextlevel()
    {
        int ns = SceneManager.GetActiveScene().buildIndex + 1;
        if (ns == 7) ns = 0;
        SceneManager.LoadScene(ns);
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

        //get a reference to gamecontroller
        if (gc == null)
        {
            gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<gamecontroller>();
            currentabilities = gc.getAbilities();
            if (!gamecontroller.HasFlag(currentabilities, gamecontroller.DMAbilities.meleemonsters))
            {
               
                monstericons[0].SetActive(false);
                monstericons[1].SetActive(false);
            }
            if (!gamecontroller.HasFlag(currentabilities, gamecontroller.DMAbilities.specialmonsters))
            {
                
                monstericons[2].SetActive(false);
                monstericons[3].SetActive(false);
            }
            if (!gamecontroller.HasFlag(currentabilities, gamecontroller.DMAbilities.spells))
            {
                trapicons[0].SetActive(false);
                trapicons[1].SetActive(false);
            }
            if (!gamecontroller.HasFlag(currentabilities, gamecontroller.DMAbilities.infintemana))
            {
                manaspellicon.SetActive(false);
               
            }
        }

        //manage hero health slider
        if (heroHealthSlider == null)
        {
            heroHealthSlider = hero.getHeroHealthSlider();
        }
        heroHealthSlider.maxValue = hero.getMaxHealth();
        heroHealthSlider.value = hero.getHealth();

        livestext.text = "Lives: " + hero.getLives();

        //manage hero stamina slider
        if (heroStaminaSlider == null)
        {
            heroStaminaSlider = hero.getHeroStaminaSlider();
        }
        heroStaminaSlider.maxValue = hero.getMaxStamina();
        heroStaminaSlider.value = hero.getStamina();

        //manage DM mana slider
        dmManaSlider.maxValue = dmcontroller.GetMaxMana();
        dmManaSlider.value = dmcontroller.GetManaCount();

        //manage DM infinite mana code;
        if (dmcontroller.getisinfinitemana())
        {
            infinitemana.SetActive(true);
            infinitemana.GetComponentInChildren<Slider>().maxValue = 5f;
            infinitemana.GetComponentInChildren<Slider>().value = dmcontroller.getmanapercentage();
        }
        else
        {
            infinitemana.SetActive(false);
        }


        //manage DM monster icons
        for (int i = 0; i < monstericons.Length; i++)
        {
            if (dmcontroller.GetManaCount() > ((i + 1) * 10) || dmcontroller.getisinfinitemana())
            {
                monstericons[i].GetComponentInChildren<Text>().color = Color.cyan;
            }
            else
            {
                monstericons[i].GetComponentInChildren<Text>().color = Color.black;
            }
        }

        //manage DM trap icons

        if (dmcontroller.GetManaCount() >= 65)
        {
            trapicons[0].GetComponentInChildren<Text>().color = Color.cyan;
        }else{
            trapicons[0].GetComponentInChildren<Text>().color = Color.black;
        }

        if (dmcontroller.GetManaCount() >= 65)
        {
            trapicons[1].GetComponentInChildren<Text>().color = Color.cyan;
        }
        else
        {
            trapicons[1].GetComponentInChildren<Text>().color = Color.black;
        }


        //handle infinite mana spell
        if (dmcontroller.checkInfinteMana())
        {
            manaspellicon.GetComponentInChildren<Text>().color = Color.cyan;
        }
        else
        {
            manaspellicon.GetComponentInChildren<Text>().color = Color.black;
        }

        //check if we need to show the game-over screen
        if (hero.getDead())
        {
            gameoverscreen.SetActive(true);
            Invoke("nextlevel", 3f);
        }


	}


}
