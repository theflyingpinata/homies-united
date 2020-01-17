using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackSpeed : Stat, IStat
{
    public float Interval;

    public AttackSpeed(BaseStat Base) : base(Base) { }

    // Calculates the attack speed and time between attacks
    public override void CalcActual()
    {
        base.CalcActual();
        Interval = 1 / Actual;
    }
}
