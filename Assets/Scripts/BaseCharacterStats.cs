using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(menuName ="Base Stats")]
public class BaseCharacterStats : ScriptableObject
{
    public string CharacterName;
    public float AD;

    public float AttackWindup;
    public float AttackWinddown;

    public float AS;

    public float MaxHealth;
}
