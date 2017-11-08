using UnityEngine;
using System.Collections.Generic;
using RPG.CharacterSystem;

public class PlayerCharacterList : MonoBehaviour {

    CharacterDatabase characterdb;

    //List<string> characterName = new List<string>{ "Adam", "Jon"};
    //int characterIndex; 
    //List<Character> character = new List<Character>();
    
    void OnEnable()
    {
        LoadCharacterDatabase();

        //for (int i = 0; i < characterName.Count; i++)
        //{
        //    characterIndex = characterdb.GetIndex(characterName[i]);

        //    if (characterIndex == -1)
        //        Debug.LogWarning("Character Name not found: " + characterName[i]);

        //    Character temp = new Character(characterdb.Get(characterIndex), 5);
        //    character.Add(temp);
        //}
    }

    void Start ()
    {
        
    }

    
	

    void LoadCharacterDatabase()
    {
        //characterdb = Resources.Load("Database/CharacterDatabase") as CharacterDatabase;
    }
}
