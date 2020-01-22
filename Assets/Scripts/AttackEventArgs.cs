using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEventArgs : System.EventArgs
{
    public Character User { get; set; }
    public Character Target { get; set; }
    public float Damage { get; set; }
    public DamageType Type { get; set; }

    public AttackEventArgs(Character user, Character target, DamageType type = DamageType.Physical)
    {
        this.User = user;
        this.Target = target;
        this.Type = type;
    }
}

public enum DamageType{
    Physical,
    Magical
}
