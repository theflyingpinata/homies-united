using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEventArgs : System.EventArgs
{
    public Character User { get; set; }
    public Character Target { get; set; }
    public float Damage { get; set; }

    public AttackEventArgs(Character user, Character target)
    {
        this.User = user;
        this.Target = target;
    }
}
