using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.CharacterSystem;
using TMPro;

public class CharacterUI : MonoBehaviour {

    public Image Icon;
    public Image HP;
    public Image MP;
    public TMP_Text HPText;
    public TMP_Text MPText;

    Character character;
    bool initialized = false;

    public void UpdateCharacterHPAndMP ()
    {
        if (!initialized)
            Debug.LogWarning ("Trying to update character HP and MP without initialization");
        else
        {
            HP.fillAmount = (float)(character.CurHP) / (float)(character.MaxHP);
            MP.fillAmount = (float)(character.CurMP) / (float)(character.MaxMP);
            HPText.text = (character.CurHP).ToString () + " / " + character.MaxHP.ToString ();
            MPText.text = (character.CurMP).ToString () + " / " + character.MaxMP.ToString ();
        }
    }

    public void Init (Character character)
    {
        this.character = character;
        
        Icon.sprite = character.Icon;
        HP.fillAmount = (float)(character.CurHP) / (float)(character.MaxHP);
        MP.fillAmount = (float)(character.CurMP) / (float)(character.MaxMP);
        HPText.text = (character.CurHP).ToString () + " / " + character.MaxHP.ToString ();
        MPText.text = (character.CurMP).ToString () + " / " + character.MaxMP.ToString ();

        initialized = true;
    }
}
