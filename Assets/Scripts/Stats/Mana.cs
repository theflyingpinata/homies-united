using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mana : Stat, IStat
{
    [SerializeField]
    private float current;
    public Mana(BaseStat Base, float starting = 0) : base(Base)
    {
        Current = starting;
    }

    public float Current
    {
        get
        {
            return current;
        }
        set
        {
            if (value < 0)
            {
                current = 0;
            }
            else if (value > Actual)
            {
                current = Actual;
            }
            else
            {
                current = value;
            }
        }
    }
    /*
    Starting;
    Max;
    Flat Starting;

    Gain;
    Flat;
    Percent;

    Current;
    */

}
