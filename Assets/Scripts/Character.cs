using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Character : MonoBehaviour
{
    public int ID;

    public BaseCharacterStats baseStats;
    public RuntimeCharacterStats runtimeStats;

    // Character Events - called during attacks and when hit
    //public CharacterEvents characterEvents;

    // Stadium - general battle info and weather shit
    public Stadium stadium;

    // Attacking info
    public float currentAttackCharge = 0;
    public bool canCharge = true;

    // Start is called before the first frame update
    void Start()
    {
        ID = UnityEngine.Random.Range(0, 10000);

        baseStats = Instantiate(baseStats);

        stadium = GameObject.Find("Stadium").GetComponent<Stadium>();

        runtimeStats = new RuntimeCharacterStats(baseStats);
        runtimeStats.SetupValues();

        SetupEvents();
    }

    // Subscribe the needed methods to the right events
    public void SetupEvents()
    {
        //StopExceptionsWithEvents();

        //characterEvents.AttackEvent += StartAttackCoroutine;
        HitEvent += TakeDamage;

        StartMoveEvent += DetermineMove;

        //RaiseAttackEvent(new AttackEventArgs(null, null));
        //StartAttackWindupEvent += AttackEvent;// RaiseAttackEvent;
        //AttackStartWindupEvent += StartAttackWindupCoroutine;
        //AttackStartWindupEvent += PrintStartAttackWindupEvent;
        AttackStartWindupEvent += StartAttack;
        AttackStartWindupEvent += DisableCharge;

        //AttackEndWindupEvent += PrintEndAttackWindupEvent;

        AttackEvent += DoAttack;
        AttackEvent += GainMana;
        //AttackEvent += AttackStartWinddownEvent;
        //AttackEvent += PrintAttackEvent;
        //AttackEvent += StartAttackWinddownCoroutine;

        //AttackStartWinddownEvent += PrintStartAttackWinddownEvent;
        //AttackEndWinddownEvent += PrintEndAttackWinddownEvent;
        AttackEndWinddownEvent += EnableCharge;


        //AbilityStartWindupEvent += StartAbilityWindupCoroutine;
        AbilityStartWindupEvent += StartAbility;
        AbilityStartWindupEvent += DisableCharge;

        AbilityEvent += DoAbility;
        AbilityEvent += UseMana;
        //AbilityEvent += AbilityStartWinddownEvent;
       // AbilityEvent += StartAbilityWinddownCoroutine;

        AbilityEndWinddownEvent += EnableCharge;
    }

    // Subscribes StopException to all events
    public void StopExceptionsWithEvents()
    {
        StartMoveEvent += StopException;


        AttackStartWindupEvent += StopException;
        //AttackEndWindupEvent += StopException;
        AttackEvent += StopException;
        //AttackStartWinddownEvent += StopException;
        AttackEndWinddownEvent += StopException;


        AbilityStartWindupEvent += StopException;
        //AbilityEndWindupEvent += StopException;
        AbilityEvent += StopException;
        //AbilityStartWinddownEvent += StopException;
        AbilityEndWinddownEvent += StopException;

        HitEvent += StopException;
    }
    // Update is called once per frame
    void Update()
    {
        UpdateAttackLogic();

        //Testing
        runtimeStats.UpdateStats();
    }


    // Handles charging attack and firing it
    public void UpdateAttackLogic()
    {
        if (canCharge)
        {
            currentAttackCharge += Time.deltaTime;
        }

        if (currentAttackCharge >= runtimeStats.AS.Interval)
        {
            currentAttackCharge -= runtimeStats.AS.Interval;
            AttackEventArgs newAttack = new AttackEventArgs(this, stadium.GetCharacterOtherSide(this));
            StartMoveEvent(newAttack);


        }
    }

    // Determines to start an attack or an ability
    public void DetermineMove(AttackEventArgs attackEventArgs)
    {
        // If the chartacer has enough mana to cast their abilty, cast it
        if (runtimeStats.Mana.Actual == runtimeStats.Mana.Current)
        {
            AbilityStartWindupEvent(attackEventArgs);
        }
        // IF they dont have enough mana, do a normal attack
        else
        {
            AttackStartWindupEvent(attackEventArgs);
        }
    }

    // Does a normal attack
    public void DoAttack(AttackEventArgs attackEventArgs)
    {
        attackEventArgs.Damage = attackEventArgs.User.baseStats.AD.Value;
        attackEventArgs.Target.RaiseHitEvent(attackEventArgs);
       
    }

    // Will call the ability to fire
    public void DoAbility(AttackEventArgs attackEventArgs)
    {
        //TODO
        if(baseStats.Ability.active)
        {
            baseStats.Ability.CastAbility(attackEventArgs);
        }
        // Call  the ability that is held in another class
    }

    // Increases Mana.Current by ManaGain.Actual
    public void GainMana(AttackEventArgs attackEventArgs)
    {
        runtimeStats.Mana.Current += runtimeStats.ManaGain.Actual;
    }

    public void UseMana(AttackEventArgs attackEventArgs)
    {
        runtimeStats.Mana.Current = 0;
    }

    // Lowers the Character's health bu the amount in attackEventArgs
    public void TakeDamage(AttackEventArgs attackEventArgs)
    {
        runtimeStats.Health.Current -= attackEventArgs.Damage;
        //Debug.Log("Damage done to \"" + ID + "\": attackEventArgs.Damage");
    }

    
    // Kicks off coroutines for times off when attacks should hit and attack should end
    public void StartAttack(AttackEventArgs attackEventArgs)
    {
        StartCoroutine(DelayAttack(attackEventArgs));
        StartCoroutine(EndAttack(attackEventArgs));
    }

    IEnumerator DelayAttack(AttackEventArgs attackEventArgs)
    {
        yield return new WaitForSeconds(baseStats.AttackWindup);
        AttackOrder(attackEventArgs);
    }

    IEnumerator EndAttack(AttackEventArgs attackEventArgs)
    {
        yield return new WaitForSeconds(baseStats.AttackWindup + baseStats.AttackWinddown);
        AttackEndWinddownEvent?.Invoke(attackEventArgs);
    }

    // Calls PreAttack, Attack, and PostAttack events so they go in order
    public void AttackOrder(AttackEventArgs attackEventArgs)
    {
        PreAttackEvent?.Invoke(attackEventArgs);
        AttackEvent?.Invoke(attackEventArgs);
        PostAttackEvent?.Invoke(attackEventArgs);
    }

    // Ability
    // Kicks off coroutines for times off when Ability should hit and attack should end
    public void StartAbility(AttackEventArgs attackEventArgs)
    {
        StartCoroutine(DelayAbility(attackEventArgs));
        StartCoroutine(EndAbility(attackEventArgs));
    }

    IEnumerator DelayAbility(AttackEventArgs attackEventArgs)
    {
        yield return new WaitForSeconds(baseStats.AbilityWindup);
        AbilityOrder(attackEventArgs);
    }

    IEnumerator EndAbility(AttackEventArgs attackEventArgs)
    {
        yield return new WaitForSeconds(baseStats.AbilityWindup + baseStats.AbilityWinddown);
        AbilityEndWinddownEvent?.Invoke(attackEventArgs);
    }

    // Calls PreAbility, Ability, and PostAbility events so they go in order
    public void AbilityOrder(AttackEventArgs attackEventArgs)
    {
        PreAbilityEvent?.Invoke(attackEventArgs);
        AbilityEvent?.Invoke(attackEventArgs);
        PostAbilityEvent?.Invoke(attackEventArgs);
    }


    // EVENTS
    public delegate void AttackDelegate(AttackEventArgs attackEventArgs);
    // Events
    public event AttackDelegate StartMoveEvent;

    // Attack
    public event AttackDelegate AttackStartWindupEvent;
    //public event AttackDelegate AttackEndWindupEvent;
    public event AttackDelegate PreAttackEvent;
    public event AttackDelegate AttackEvent;
    public event AttackDelegate PostAttackEvent;
    //public event AttackDelegate AttackStartWinddownEvent;
    public event AttackDelegate AttackEndWinddownEvent;

    // Ability
    public event AttackDelegate AbilityStartWindupEvent;
    //public event AttackDelegate AbilityEndWindupEvent;
    public event AttackDelegate PreAbilityEvent;
    public event AttackDelegate AbilityEvent;
    public event AttackDelegate PostAbilityEvent;
    //public event AttackDelegate AbilityStartWinddownEvent;
    public event AttackDelegate AbilityEndWinddownEvent;

    public event AttackDelegate HitEvent;



    // This is bandaid work around for Events that will through an exception when nothing is subscribed to them
    public void StopException(AttackEventArgs attackEventArgs) { }

    public void RaiseHitEvent(AttackEventArgs attackEventArgs)
    {
        HitEvent(attackEventArgs);
    }

    public void PeePeePooPoo(AttackEventArgs eh)
    {
        //Debug.Log("HEEHE");
    }
    public void PrintStartAttackWindupEvent(AttackEventArgs attackEventArgs)
    {
        //Debug.Log("Start Attack Wind Up Event");
    }
    public void PrintEndAttackWindupEvent(AttackEventArgs attackEventArgs)
    {
        //Debug.Log("End Attack Wind Up Event");
    }

    public void PrintAttackEvent(AttackEventArgs attackEventArgs)
    {
        //Debug.Log("Attack Event");
    }

    public void PrintStartAttackWinddownEvent(AttackEventArgs attackEventArgs)
    {
        //Debug.Log("Start Attack Wind Down Event");
    }
    public void PrintEndAttackWinddownEvent(AttackEventArgs attackEventArgs)
    {
        //Debug.Log("End Attack Wind Down Event");
    }

    // Enables charging of attack
    public void EnableCharge(AttackEventArgs attackEventArgs)
    {
        canCharge = true;
    }

    // Disables charging of attack
    public void DisableCharge(AttackEventArgs attackEventArgs)
    {
        canCharge = false;
    }
}
