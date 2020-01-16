using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int ID;

    public BaseCharacterStats baseStats;
    public RuntimeCharacterStats runtimeStats;

    public SpriteAttributeChanger spriteAttributeChanger;
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

        spriteAttributeChanger = gameObject.GetComponent<SpriteAttributeChanger>();

        runtimeStats = new RuntimeCharacterStats(baseStats);
        runtimeStats.SetupValues();

        SetupEvents();
    }

    // Subscribe the needed methods to the right events
    public void SetupEvents()
    {

        //characterEvents.AttackEvent += StartAttackCoroutine;
        HitEvent += TakeDamage;
        HitEvent += HitAnimation;

        //RaiseAttackEvent(new AttackEventArgs(null, null));
        //StartAttackWindupEvent += AttackEvent;// RaiseAttackEvent;
        StartAttackWindupEvent += StartAttackWindupCoroutine;
        StartAttackWindupEvent += PrintStartAttackWindupEvent;
        StartAttackWindupEvent += DisableCharge;

        EndAttackWindupEvent += PrintEndAttackWindupEvent;

        AttackEvent += DoAttack;
        AttackEvent += StartAttackWinddownEvent;
        AttackEvent += PrintAttackEvent;
        AttackEvent += StartAttackWinddownCoroutine;

        StartAttackWinddownEvent += PrintStartAttackWinddownEvent;
        EndAttackWinddownEvent += PrintEndAttackWinddownEvent;
        EndAttackWinddownEvent += EnableCharge;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAttackLogic();

        // Testing
        runtimeStats.CalcAS();
        runtimeStats.CalcAD();
    }


    // Handles charging attack and firing it
    public void UpdateAttackLogic()
    {
        if (canCharge)
        {
            currentAttackCharge += Time.deltaTime;
        }

        if(currentAttackCharge >= runtimeStats.timeBetweenAttacks)
        {
            currentAttackCharge -= runtimeStats.timeBetweenAttacks;
            AttackEventArgs newAttack = new AttackEventArgs(this, stadium.GetCharacterOtherSide(this));
            StartAttackWindupEvent(newAttack);


        }
    }

    public void DoAttack(AttackEventArgs attackEventArgs)
    {
        attackEventArgs.Damage = attackEventArgs.User.baseStats.AD;
        attackEventArgs.Target.HitEvent(attackEventArgs);

    }

    // Lowers the Character's health bu the amount in attackEventArgs
    public void TakeDamage(AttackEventArgs attackEventArgs)
    {
        runtimeStats.CurrentHealth -= attackEventArgs.Damage;
        Debug.Log("Damage done to \"" + ID + "\": attackEventArgs.Damage");
    }

    public void HitAnimation(AttackEventArgs attackEventArgs)
    {
        spriteAttributeChanger.SquashSprite(.5f);
    }
    // Starts up the attack wind up
    public void StartAttackWindupCoroutine(AttackEventArgs attackEventArgs)
    {
        Debug.Log("StartAttackCoroutine");
        spriteAttributeChanger.FlashColor(Color.black, baseStats.AttackWindup);
        StartCoroutine(DelayEvent(EndAttackWindupEvent, attackEventArgs, baseStats.AttackWindup));
        StartCoroutine(DelayEvent(AttackEvent, attackEventArgs, baseStats.AttackWindup));
    }

    // Starts up the attack wind down
    public void StartAttackWinddownCoroutine(AttackEventArgs attackEventArgs)
    {
        StartAttackWinddownEvent?.Invoke(attackEventArgs);
        Debug.Log("StartAttackCoroutine");
        spriteAttributeChanger.FlashColor(Color.green, baseStats.AttackWinddown);
        StartCoroutine(DelayEvent(EndAttackWinddownEvent, attackEventArgs, baseStats.AttackWinddown));
    }

    IEnumerator DelayEvent(AttackDelegate deli, AttackEventArgs attackEventArgs, float delay)
    {

        Debug.Log("Delay Event " + deli.ToString()) ;
        yield return new WaitForSeconds(baseStats.AttackWindup);
        deli(attackEventArgs);
    }



    // EVENTS
    public delegate void AttackDelegate(AttackEventArgs attackEventArgs);
    // Events
    public event AttackDelegate StartAttackWindupEvent;
    public event AttackDelegate EndAttackWindupEvent;
    public event AttackDelegate AttackEvent;
    public event AttackDelegate StartAttackWinddownEvent;
    public event AttackDelegate EndAttackWinddownEvent;

    public event AttackDelegate HitEvent;



    public void PeePeePooPoo(AttackEventArgs eh)
    {
        Debug.Log("HEEHE");
    }
    public void PrintStartAttackWindupEvent(AttackEventArgs attackEventArgs)
    {
        Debug.Log("Start Attack Wind Up Event");
    }
    public void PrintEndAttackWindupEvent(AttackEventArgs attackEventArgs)
    {
        Debug.Log("End Attack Wind Up Event");
    }

    public void PrintAttackEvent(AttackEventArgs attackEventArgs)
    {
        Debug.Log("Attack Event");
    }

    public void PrintStartAttackWinddownEvent(AttackEventArgs attackEventArgs)
    {
        Debug.Log("Start Attack Wind Down Event");
    }
    public void PrintEndAttackWinddownEvent(AttackEventArgs attackEventArgs)
    {
        Debug.Log("End Attack Wind Down Event");
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
