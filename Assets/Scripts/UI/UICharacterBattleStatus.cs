using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICharacterBattleStatus : MonoBehaviour
{
    [Header("Character Stuff")]
    // Stats
    public string characterToDisplay;
    public Character character;
    public RuntimeCharacterStats runtimeCharacterStats;

    [Header("UI Elements")]
    // UI Elements
    public TextMeshProUGUI text;
    public Image imagePortrait;
    public Slider sliderHealth;
    public Slider sliderMana;
    public Slider sliderAS;

    [Header("More Info")]
    // More info
    public GameObject moreInfoPrefab;
    public bool moreInfoDisplaying = false;
    public GameObject moreInfoGO;

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find(characterToDisplay).GetComponentInChildren<Character>();
        //runtimeCharacterStats = character.runtimeStats;
        //Setup();
    }

    // Update is called once per frame
    void Update()
    {
        if(character != null && runtimeCharacterStats != null && runtimeCharacterStats.characterName != "")
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
            character = GameObject.Find(characterToDisplay).GetComponentInChildren<Character>();
            runtimeCharacterStats = character.runtimeStats;
            //Debug.Log(runtimeCharacterStats.characterName);
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
        //Setup();
        sliderHealth.value = runtimeCharacterStats.Health.Current;
        sliderMana.value = runtimeCharacterStats.Mana.Current;
        sliderAS.value = character.currentAttackCharge;
    }

    public void DisplayMoreInfo()
    {
        Debug.Log("Display More Info has been called");
        if(!moreInfoDisplaying)
        {
            moreInfoGO = Instantiate(moreInfoPrefab, transform.position + new Vector3(imagePortrait.rectTransform.rect.width / 2, imagePortrait.rectTransform.rect.height, 0), Quaternion.identity, transform);
            Debug.Log("Flexible Width/2: " + imagePortrait.rectTransform.rect.width / 2 + "  Height/2: " + imagePortrait.rectTransform.rect.height / 2);
        }
        else
        {
            GameObject.Destroy(moreInfoGO);
        }
        moreInfoDisplaying = !moreInfoDisplaying;
    }
}
