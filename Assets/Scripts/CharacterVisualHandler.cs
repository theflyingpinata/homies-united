using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterVisualHandler : MonoBehaviour
{
    public Character character;
    public SpriteAttributeChanger spriteAttributeChanger;
    //public Animator animator;

    public GameObject floatingTextPrefab;


    // Start is called before the first frame update
    void Start()
    {
        spriteAttributeChanger = gameObject.GetComponent<SpriteAttributeChanger>();
        //animator = gameObject.GetComponent<Animator>();

        if (character)
        {
            if (spriteAttributeChanger)
            {
                spriteAttributeChanger.ChangeSprites(character.baseStats.sprite);
            }

            //animator.runtimeAnimatorController = character.baseStats.animator;

            character.AttackStartWindupEvent += AttackAnimation;
            character.PostAttackEvent += EndAttackAnimation;
            character.AbilityStartWindupEvent += AbilityAnimation;
            character.PostAbilityEvent += EndAbilityAnimation;
            character.HitEvent += HitAnimation;
        }
    }

    // Update is called once per frame
    void Update()
    {

        //spriteAttributeChanger.ChangeSprites(character.baseStats.sprite);
    }

    public void HitAnimation(AttackEventArgs attackEventArgs)
    {
        //spriteAttributeChanger.SquashSprite(.5f);
        ShowFloatingText(attackEventArgs);
    }

    public void ShowFloatingText(AttackEventArgs attackEventArgs)
    {
        if (floatingTextPrefab)
        {
            //Debug.Log("Make text");
            GameObject go = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
            TextMeshPro tmpro = go.GetComponent<TextMeshPro>();
            tmpro.text = Mathf.RoundToInt(attackEventArgs.Damage).ToString();
            switch (attackEventArgs.Type)
            {
                case DamageType.Physical:
                    tmpro.color = new Color(256, 0, 0);
                    break;
                case DamageType.Magical:
                    tmpro.color = new Color(0, 0, 256);
                    break;
            }
        }
    }

    // Does the stuff for an animation of the attack
    public void AttackAnimation(AttackEventArgs attackEventArgs)
    {
        //animator.SetTrigger("Attack");
        spriteAttributeChanger.FlashColor(Color.black, character.baseStats.AttackWindup);
    }

    // For Testing
    // Changes color of character for end of attack
    public void EndAttackAnimation(AttackEventArgs attackEventArgs)
    {
        spriteAttributeChanger.FlashColor(Color.green, character.baseStats.AttackWinddown);
    }


    // Does the stuff for an animation of the attack
    public void AbilityAnimation(AttackEventArgs attackEventArgs)
    {
        //animator.SetTrigger("Attack");
        spriteAttributeChanger.FlashColor(Color.yellow, character.baseStats.AbilityWindup);
    }

    // For Testing
    // Changes color of character for end of attack
    public void EndAbilityAnimation(AttackEventArgs attackEventArgs)
    {
        spriteAttributeChanger.FlashColor(Color.cyan, character.baseStats.AbilityWinddown);
    }
}
