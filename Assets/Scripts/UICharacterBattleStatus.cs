using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICharacterBattleStatus : MonoBehaviour
{
    // Stats
    public RuntimeCharacterStats runtimeCharacterStats;


    // UI Elements
    public TextMeshProUGUI text;
    public Image imagePortrait;
    public Slider sliderHealth;
    public Slider sliderAS;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        if(runtimeCharacterStats != null)
        {
            UpdateUI();
        }
    }

    // Sets the parts of the UI that are static
    public void Setup()
    {
        text.text = runtimeCharacterStats.characterName;

        // Sliders
        sliderHealth.maxValue = runtimeCharacterStats.maxHealth;

    }
    public void UpdateUI()
    {
        sliderHealth.value = runtimeCharacterStats.CurrentHealth;
    }
}
