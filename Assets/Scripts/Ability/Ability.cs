using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Ability : ScriptableObject
{
    public float power;
    public bool active = true;
    public virtual void CastAbility(AttackEventArgs attackEventArgs) { }
}
