using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamecontroller : MonoBehaviour {
    [SerializeField]
    private int level_num = 2;


    private int monstersKilled;
    [SerializeField]
    private int monsterKillsNeeded;

    private float time;
    private UIManager _uiManager;
    [SerializeField]
    private float timeNeeded = 60;

    private int monstersSummoned;
    [SerializeField]
    private int monsterSummonsNeeded;


    // DM spells enabled?
    [System.Flags]
    public enum DMAbilities : byte
    {
        nothing = 0,
        meleemonsters = 1, specialmonsters = 2, spells = 4, infintemana = 8, regen = 16
    };

    private DMAbilities current_abilities;

    public static bool HasFlag(DMAbilities a, DMAbilities b)
    {
        return (a & b) == b;
    }


    public static DMAbilities SetFlag(DMAbilities a, DMAbilities b)
    {
        return a | b;
    }

    public static DMAbilities UnsetFlag(DMAbilities a, DMAbilities b)
    {
        return a & (~b);
    }


    public DMAbilities getAbilities()
    {
        updateabilities();
        Debug.Log(current_abilities);
        return current_abilities;
    }


    private void updateabilities()
    {
        current_abilities = 0;
        switch (level_num)
        {
            case 0:
                current_abilities = SetFlag(current_abilities, DMAbilities.meleemonsters);
                current_abilities = UnsetFlag(current_abilities, DMAbilities.specialmonsters);
                current_abilities = UnsetFlag(current_abilities, DMAbilities.spells);
                current_abilities = UnsetFlag(current_abilities, DMAbilities.infintemana);
                break;
            case 1:
                
                current_abilities = UnsetFlag(current_abilities, DMAbilities.meleemonsters);
                current_abilities = SetFlag(current_abilities, DMAbilities.specialmonsters);
                current_abilities = SetFlag(current_abilities, DMAbilities.spells);
                current_abilities = SetFlag(current_abilities, DMAbilities.infintemana);
                break;
            case 2:
                current_abilities = SetFlag(current_abilities, DMAbilities.meleemonsters);
                current_abilities = SetFlag(current_abilities, DMAbilities.specialmonsters);
                current_abilities = SetFlag(current_abilities, DMAbilities.spells);
                current_abilities = SetFlag(current_abilities, DMAbilities.infintemana);
                break;
            default:
                break;
        }
    }

    private void updatewincondition()
    {
        switch (level_num)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                time -= Time.deltaTime;
                if (_uiManager == null)
                {
                    _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
                }
                _uiManager.updateWinConditionText("Time Left: " + Mathf.FloorToInt(time));
                if (time <= 0)
                {
                    herowin();
                }
                break;
        }
        
    }


    public void herowin()
    {
        _uiManager.herowin();
       // Debug.Log("Hero won.");

    }

	// Use this for initialization
	void Start () {
        updateabilities();
        time = timeNeeded;
	}
	
	// Update is called once per frame
	void Update () {
        updatewincondition();
	}
}
