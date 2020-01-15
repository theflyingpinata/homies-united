using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace CharacterEs
{
    public class CharacterEvents
    {
        public delegate void AttackDelegate(AttackEventArgs attackEventArgs, object additional = null);
        // Events
        public event AttackDelegate StartAttackWindupEvent;
        public event AttackDelegate EndAttackWindupEvent;
        public event AttackDelegate AttackEvent;
        public event AttackDelegate StartAttackWinddownEvent;
        public event AttackDelegate EndAttackWinddownEvent;

        public event AttackDelegate HitEvent;

        public CharacterEvents()
        {

            //RaiseAttackEvent(new AttackEventArgs(null, null));
            StartAttackWindupEvent += RaiseAttackEvent;
            StartAttackWindupEvent += PrintAttackWindupEvent;

            AttackEvent += EndAttackWinddownEvent;
            AttackEvent += PrintAttackEvent;

            EndAttackWinddownEvent += PrintAttackWinddownEvent;


        }

        public void RaiseEvents(string eventName, AttackEventArgs attackEventArgs)
        {
            switch (eventName)
            {
                case "StartAttackWindupEvent":
                    StartAttackWindupEvent?.Invoke(attackEventArgs);
                    break;
                case "EndAttackWindupEvent":
                    EndAttackWindupEvent?.Invoke(attackEventArgs);
                    break;
                case "AttackEvent":
                    AttackEvent?.Invoke(attackEventArgs);
                    break;
                case "StartAttackWinddownEvent":
                    StartAttackWinddownEvent?.Invoke(attackEventArgs);
                    break;
                case "EndAttackWinddownEvent":
                    EndAttackWinddownEvent?.Invoke(attackEventArgs);
                    break;
                case "HitEvent":
                    HitEvent?.Invoke(attackEventArgs);
                    break;

            }
        }
        public void PeePeePooPoo(AttackEventArgs eh, object additional)
        {
            Debug.Log("HEEHE");
        }
        /*
        public void RaiseStartAttackWindupEvent(AttackEventArgs attackEventArgs)
        {
            AttackDelegate handler = StartAttackWindupEvent;
            if (handler != null)
                StartAttackWindupEvent?.Invoke(attackEventArgs);
        }
        */

        public void RaiseAttackEvent(AttackEventArgs attackEventArgs, object additional)
        {
            if (AttackEvent != null)
                AttackEvent(attackEventArgs);
        }
        /*
        public void RaiseAttackWinddownEvent(AttackEventArgs attackEventArgs)
        {
            if (AttackWinddownEvent != null)
                AttackWinddownEvent(attackEventArgs);
        }


        public void RaiseHitEvent(AttackEventArgs attackEventArgs)
        {
            if (HitEvent != null)
                HitEvent(attackEventArgs);
        }
        */
        public void PrintAttackWindupEvent(AttackEventArgs attackEventArgs, object additional)
        {
            Debug.Log("Attack Wind Up Event");
        }

        public void PrintAttackEvent(AttackEventArgs attackEventArgs, object additional)
        {
            Debug.Log("Attack Event");
        }

        public void PrintAttackWinddownEvent(AttackEventArgs attackEventArgs, object additional)
        {
            Debug.Log("Attack Wind Down Event");
        }

    }
}
