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
        if (currentCharacter.UniqueID == character1_Side1.UniqueID || currentCharacter.UniqueID == character2_Side1.UniqueID)
        {
            if(Random.value == 0)
            {
                return character1_Side1;
            }
            else
            {
                return character2_Side1;
            }
        }
        else if (currentCharacter.UniqueID == character1_Side2.UniqueID || currentCharacter.UniqueID == character2_Side2.UniqueID)
        {
            if (Random.value == 0)
            {
                return character1_Side2;
            }
            else
            {
                return character2_Side2;
            }
        }

        return null;
    }
}
