using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.CharacterSystem;

//[ExecuteInEditMode]
public class Testing : MonoBehaviour {

    CharacterDatabase _db;
    public Character character;
    //public BaseCharacter baseCharacter;
    

    void Awake ()
    {
        


        //baseCharacter = _db.Get (0);
    }


	// Use this for initialization
	void Start () {
        _db = Resources.Load ("Database/CharacterDatabase") as CharacterDatabase;
        character = new Character (_db.Get (0), 5);
        


        Debug.Log ("" + character.Status[0].EffectType);
        Debug.Log ("" + character.Status[0].Percentage);
        Debug.Log ("" + character.Status[0].Duration);
        //character.CharacterData.Name = "AAA";
        //Debug.Log ("" + character.CharacterData.Speed.BaseValue);
        //Debug.Log ("" + character.CharacterData.Speed.CurrentValue);
        //Debug.Log ("" + character.Skill.Count);
        //Debug.Log ("" + character.Skill[0].SkillData.Name);
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.T))
        {
            for (int i = 0; i < character.Status.Count; i++)
            {
                character.Status[i].Duration--;
            }
        }

        for (int i = 0; i < character.Status.Count; i++)
        {
            if (character.Status[i].Duration == 0)
                character.Status.RemoveAt (i);
        }
    }
}
