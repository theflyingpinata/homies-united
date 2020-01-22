using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stadium : MonoBehaviour
{
    public GameObject[] alliesGO;
    public Character[] alliesChar;

    public GameObject[] enemiesGO;
    public Character[] enemiesChar;

    public void Start()
    {
        alliesGO = new GameObject[3];
        FindCharacterGO(alliesGO, "Ally");

        alliesChar = new Character[3];
        FillCharList(alliesGO, alliesChar);

        enemiesGO = new GameObject[3];
        FindCharacterGO(enemiesGO, "Enemy");

        enemiesChar = new Character[3];
        FillCharList(enemiesGO, enemiesChar);
    }

    // Will find gameobjects to fill the array of gameobjects using the string given as basically a key
    public void FindCharacterGO(GameObject[] gameObjects, string side)
    {
        for(int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i] = GameObject.Find(side + " " + (i + 1));

        }
    }

    // Will fill the given array of characters with the component from the array of gameobjects given
    public void FillCharList(GameObject[] gameObjects, Character[] characters)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if(gameObjects[i].activeSelf)
            {
                characters[i] = gameObjects[i].GetComponentInChildren<Character>();
            }

        }
    }


    // Returns a character on the other side of the character given. Returns null if character given isn't on either side
    public Character GetCharacterOtherSide(Character currentCharacter)
    {
        foreach(Character character in alliesChar)
        {
            if(character.ID == currentCharacter.ID)
            {
                return GetRandomCharacter(enemiesChar);
                
            }
        }

        foreach (Character character in enemiesChar)
        {
            if (character.ID == currentCharacter.ID)
            {
                return GetRandomCharacter(alliesChar);

            }
        }

        return null;
    }

    // Returns a valid target from the given character array
    public Character GetRandomCharacter(Character[] characters)
    {
        // If characters is null or empty
        if(characters == null || characters.Length <= 1 )
        {
            return null;
        }

        // Get a random member of characters
        int randNum = Random.Range(0, characters.Length);
        while(characters[randNum] == null)
        {
            randNum = Random.Range(0, characters.Length);
        }
        return characters[randNum];
    }
}
