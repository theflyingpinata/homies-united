using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Health : Stat, IStat
{
    [SerializeField]
    private float current;
    public Health(BaseStat Base) : base(Base)
    {
        current = Actual;
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

}
