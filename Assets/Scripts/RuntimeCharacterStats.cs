using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RuntimeCharacterStats
{
    public BaseCharacterStats baseStats;

    // Current stats or stats that change

    // Attack Speed
    [Header("Attack Speed")]
    public float percentAS;
    public float currentAS;
    public float timeBetweenAttacks;

    // Attack Damage
    [Header("Attack Damage")]
    public float flatAD;
    public float percentAD;
    public float currentAD;

    // Health
    [Header("Health")]
    [SerializeField]
    private float currentHealth;

    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            if (value < 0)
            {
                currentHealth = 0;
            }
            else if (value > baseStats.MaxHealth)
            {
                currentHealth = baseStats.MaxHealth;
            }
            else
            {
                currentHealth = value;
            }
        }
    }

    public RuntimeCharacterStats(BaseCharacterStats baseStats)
    {
        this.baseStats = baseStats;
    }
    public void Start()
    {
        SetupValues();
    }
    public void SetupValues()
    {
        // Attack Speed
        percentAS = 0;
        CalcAS();

        // Attack Damage
        flatAD = 0;
        percentAD = 0;
        CalcAD();

        // Health
        CurrentHealth = baseStats.MaxHealth;
    }

    // Calculates the attack speed and time between attacks
    public void CalcAS()
    {
        currentAS = baseStats.AS  + (baseStats.AS * (percentAS / 100f));
        timeBetweenAttacks = 1 / currentAS ;
    }


    // Calculates the attack damage, factoring in flat and percent bonuss
    public void CalcAD()
    {
        currentAD = baseStats.AD + flatAD + (baseStats.AD * (percentAD / 100f));
    }
}
