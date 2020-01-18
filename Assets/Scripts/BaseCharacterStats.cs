using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(menuName ="Base Stats")]
public class BaseCharacterStats : ScriptableObject
{
    // Info 
    public string CharacterName;
    // Temnp
    public Sprite sprite;
    public RuntimeAnimatorController animator;

    // Ability
    public Ability Ability;
    
    
    // Stats
    public BaseStat AD;

    public float AttackWindup;
    public float AttackWinddown;
    public float AbilityWindup;
    public float AbilityWinddown;

    public BaseStat AS;

    public BaseStat MaxHealth;

    public BaseStat StartingMana;
    public BaseStat MaxMana;
    public BaseStat ManaGain;
}
