using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICharacterBattleStatus : MonoBehaviour
{
    // Stats
    public Character character;
    public RuntimeCharacterStats runtimeCharacterStats;


    // UI Elements
    public TextMeshProUGUI text;
    public Image imagePortrait;
    public Slider sliderHealth;
    public Slider sliderMana;
    public Slider sliderAS;

    // Start is called before the first frame update
    void Start()
    {
        runtimeCharacterStats = character.runtimeStats;
        //Setup();
    }

    // Update is called once per frame
    void Update()
    {
        if(runtimeCharacterStats != null && runtimeCharacterStats.characterName != "")
        {
            // test
            /*
            runtimeCharacterStats.UpdateStats();
            Setup();
            */
            UpdateUI();
        }
        else
        {
            runtimeCharacterStats = character.runtimeStats;
            Setup();
        }
    }

    // Sets the parts of the UI that are static
    public void Setup()
    {
        text.text = runtimeCharacterStats.characterName;
        imagePortrait.sprite = runtimeCharacterStats.baseStats.sprite;

        // Sliders
        sliderHealth.maxValue = runtimeCharacterStats.Health.Actual;
        sliderMana.maxValue = runtimeCharacterStats.Mana.Actual;
        sliderAS.maxValue = runtimeCharacterStats.AS.Interval;


    }
    public void UpdateUI()
    {
        Setup();
        sliderHealth.value = runtimeCharacterStats.Health.Current;
        sliderMana.value = runtimeCharacterStats.Mana.Current;
        sliderAS.value = character.currentAttackCharge;
    }
}
