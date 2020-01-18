using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Ability/Fireball")]
public class Fireball_Ability : Ability
{
    public override void CastAbility(AttackEventArgs attackEventArgs)
    {
        attackEventArgs.Damage = power;
        attackEventArgs.Target.RaiseHitEvent(attackEventArgs);
    }
}
