using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat : IStat
{
    public BaseStat Base;
    public float Flat;
    public float Percent;
    public float Actual;

    public Stat(BaseStat Base)
    {
        this.Base = Base;
        this.Flat = 0;
        this.Percent = 0;
        CalcActual();
    }
    /*
    // Sets up the Stat fresh
    public virtual void SetupStat()
    {
        CalcStat();
    }
    */
    // Calculates Current, using Base value, Flat, and Percent
    public virtual void CalcActual()
    {
        Actual = Base.Value + Flat * (1f + (Percent / 100f));
    }
}
