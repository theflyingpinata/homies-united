using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterEs;

public class Character : MonoBehaviour
{
    public int UniqueID;

    public CharacterStats stats;

    // Character Events - called during attacks and when hit
    public CharacterEvents characterEvents;

    // Stadium - general battle info and weather shit
    public Stadium stadium;

    // Attacking info
    public float currentAttackCharge = 0;

    // Start is called before the first frame update
    void Start()
    {
        UniqueID = UnityEngine.Random.Range(0, 10000);

        stadium = GameObject.Find("Stadium").GetComponent<Stadium>();

        characterEvents = new CharacterEvents();

        characterEvents.StartAttackWindupEvent += StartAttackCoroutine;
        characterEvents.AttackEvent += DoAttack;
        //characterEvents.AttackEvent += StartAttackCoroutine;
        characterEvents.HitEvent += TakeDamage;


        stats.CleanUpValues();
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
            characterEvents.RaiseEvents("StartAttackWindupEvent", newAttack);


        }
    }

    public void DoAttack(AttackEventArgs attackEventArgs, object additional)
    {
        attackEventArgs.Damage = attackEventArgs.User.stats.attackDamage;
        attackEventArgs.Target.characterEvents.RaiseEvents("HitEvent", attackEventArgs);

    }

    public void TakeDamage(AttackEventArgs attackEventArgs, object additional)
    {
        stats.CurrentHealth -= attackEventArgs.Damage;
        Debug.Log(attackEventArgs.Damage);
    }

    public void StartAttackCoroutine(AttackEventArgs attackEventArgs, object additional)
    {
        Debug.Log("StartAttackCoroutine");
        StartCoroutine(AttackSpacerCoroutine(attackEventArgs));
    }

    IEnumerator AttackSpacerCoroutine(AttackEventArgs attackEventArgs)
    {
        Debug.Log("AttackSpacer");
        yield return new WaitForSeconds(stats.attackWindup);
        characterEvents.RaiseEvents("AttackEvent", attackEventArgs);
    }




}
