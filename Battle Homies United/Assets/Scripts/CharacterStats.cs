using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct CharacterStats
{
    public float attackDamage;

    public float attackWindup;
    public float attackWinddown;

    public float attacksPerSecond;
    public float timeBetweenAttacks;

    public float maxHealth;
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
            if(value < 0)
            {
                currentHealth = 0;
            }
            else if(value > maxHealth)
            {
                currentHealth = maxHealth;
            }
            else
            {
                currentHealth = value;
            }
        }
    }

    public void CleanUpValues()
    {
        CurrentHealth = maxHealth;
        timeBetweenAttacks = 1 / attacksPerSecond;
    }
}
