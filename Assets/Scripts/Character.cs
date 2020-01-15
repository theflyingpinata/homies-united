using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int UniqueID;

    public CharacterStats stats;

    public ColorChanger colorChanger;
    // Character Events - called during attacks and when hit
    //public CharacterEvents characterEvents;

    // Stadium - general battle info and weather shit
    public Stadium stadium;

    // Attacking info
    public float currentAttackCharge = 0;

    // Start is called before the first frame update
    void Start()
    {
        UniqueID = UnityEngine.Random.Range(0, 10000);

        stadium = GameObject.Find("Stadium").GetComponent<Stadium>();

        colorChanger = gameObject.GetComponent<ColorChanger>();

        stats.CleanUpValues();



        StartAttackWindupEvent += StartAttackCoroutine;
        AttackEvent += DoAttack;
        //characterEvents.AttackEvent += StartAttackCoroutine;
        HitEvent += TakeDamage;
        HitEvent += HitAnimation;

        //RaiseAttackEvent(new AttackEventArgs(null, null));
        //StartAttackWindupEvent += AttackEvent;// RaiseAttackEvent;
        StartAttackWindupEvent += PrintAttackWindupEvent;

        AttackEvent += EndAttackWinddownEvent;
        AttackEvent += PrintAttackEvent;

        EndAttackWinddownEvent += PrintAttackWinddownEvent;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAttackLogic();
    }


    // Handles charging attack and firing it
    public void UpdateAttackLogic()
    {
        currentAttackCharge += Time.deltaTime;

        if(currentAttackCharge >= stats.timeBetweenAttacks)
        {
            currentAttackCharge -= stats.timeBetweenAttacks;
            AttackEventArgs newAttack = new AttackEventArgs(this, stadium.GetCharacterOtherSide(this));
            StartAttackWindupEvent(newAttack);


        }
    }

    public void DoAttack(AttackEventArgs attackEventArgs)
    {
        attackEventArgs.Damage = attackEventArgs.User.stats.attackDamage;
        attackEventArgs.Target.HitEvent(attackEventArgs);

    }

    public void TakeDamage(AttackEventArgs attackEventArgs)
    {
        stats.CurrentHealth -= attackEventArgs.Damage;
        Debug.Log(attackEventArgs.Damage);
    }

    public void HitAnimation(AttackEventArgs attackEventArgs)
    {
        colorChanger.FlashColor(Color.red, .5f);
    }
    public void StartAttackCoroutine(AttackEventArgs attackEventArgs)
    {
        Debug.Log("StartAttackCoroutine");
        StartCoroutine(DelayEvent(AttackEvent, attackEventArgs, stats.attackWindup));
    }

    IEnumerator DelayEvent(AttackDelegate deli, AttackEventArgs attackEventArgs, float delay)
    {

        Debug.Log("AttackSpacer");
        colorChanger.FlashColor(Color.blue, stats.attackWindup);
        yield return new WaitForSeconds(stats.attackWindup);
        deli(attackEventArgs);
    }
    /*
    IEnumerator AttackSpacerCoroutine(AttackEventArgs attackEventArgs)
    {
        Debug.Log("AttackSpacer");
        colorChanger.FlashColor(Color.blue, stats.attackWindup);
        yield return new WaitForSeconds(stats.attackWindup);
        AttackEvent(attackEventArgs);
    }
    */

    // EVENTS
    public delegate void AttackDelegate(AttackEventArgs attackEventArgs);
    // Events
    public event AttackDelegate StartAttackWindupEvent;
    public event AttackDelegate EndAttackWindupEvent;
    public event AttackDelegate AttackEvent;
    public event AttackDelegate StartAttackWinddownEvent;
    public event AttackDelegate EndAttackWinddownEvent;

    public event AttackDelegate HitEvent;


    /*
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
    */
    public void PeePeePooPoo(AttackEventArgs eh)
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

        /*
    public void RaiseAttackEvent(AttackEventArgs attackEventArgs)
    {
        if (AttackEvent != null)
            AttackEvent(attackEventArgs);
    }
    */
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
    public void PrintAttackWindupEvent(AttackEventArgs attackEventArgs)
    {
        Debug.Log("Attack Wind Up Event");
    }

    public void PrintAttackEvent(AttackEventArgs attackEventArgs)
    {
        Debug.Log("Attack Event");
    }

    public void PrintAttackWinddownEvent(AttackEventArgs attackEventArgs)
    {
        Debug.Log("Attack Wind Down Event");
    }


}
