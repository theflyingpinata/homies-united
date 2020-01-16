using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stadium : MonoBehaviour
{
    public Character character1_Side1;
    public Character character2_Side1;

    public Character character1_Side2;
    public Character character2_Side2;

    // Returns a character on the other side of the character given. Returns null if character given isn't on either side
    public Character GetCharacterOtherSide(Character currentCharacter)
    {
        if (currentCharacter.ID == character1_Side1.ID || currentCharacter.ID == character2_Side1.ID)
        {
            if(Random.Range(0,2) == 0)
            {
                return character1_Side2;
            }
            else
            {
                return character2_Side2;
            }
        }
        else if (currentCharacter.ID == character1_Side2.ID || currentCharacter.ID == character2_Side2.ID)
        {
            if (Random.Range(0, 2) == 0)
            {
                return character1_Side1;
            }
            else
            {
                return character2_Side1;
            }
        }

        return null;
    }
}
