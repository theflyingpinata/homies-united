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
    public AttackSpeed AS;

    // Attack Damage
    [Header("Attack Damage")]
    public Stat AD;

    // Health
    [Header("Health")]
    public Health Health;

    // Mana
    [Header("Mana")]
    public Mana Mana;
    public Stat ManaGain;

    // Name info
    public string characterName;

    public RuntimeCharacterStats(BaseCharacterStats baseStats)
    {
        this.baseStats = baseStats;
        SetupValues();
    }

    public void SetupValues()
    {
        // Attack Speed
        AS = new AttackSpeed(baseStats.AS);

        // Attack Damage
        AD = new Stat(baseStats.AD);

        // Health
        Health = new Health(baseStats.MaxHealth);

        // Mana
        Mana = new Mana(baseStats.MaxMana, baseStats.StartingMana.Value);
        ManaGain = new Stat(baseStats.ManaGain);

        // Name
        characterName = baseStats.CharacterName;
    }

    public void UpdateStats()
    {
        AS.CalcActual();
        AD.CalcActual();
        Mana.CalcActual();
        Health.CalcActual();

    }
    /*
    // Calculates the attack speed and time between attacks
    public void CalcAS()
    {
        currentAS = baseStats.AS  * (1f + (percentAS / 100f));
        timeBetweenAttacks = 1 / currentAS ;
    }


    // Calculates the attack damage, factoring in flat and percent bonuss
    public void CalcAD()
    {
        currentAD = baseStats.AD + flatAD * (1f + (percentAD / 100f));
    }

    // Calculates the max health, factoring in flat and percent bonuss
    public void CalcMaxHealth()
    {
        maxHealth = baseStats.MaxHealth + flatHealth * (1f + (percentHealth / 100f));
    }
    */

}
