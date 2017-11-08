using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuSelector : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string[] options;
    public int defaultIndex;
    

    int selectedIndex = 0;

    void Awake ()
    {
        updateText ();
    }

    public void BtnLeftOnClick ()
    {
        if (selectedIndex > 0)
        {
            selectedIndex--;
            updateText ();

            updateSettings ();
        }
    }

    public void BtnRightOnClick ()
    {
        if (selectedIndex < options.Length - 1)
        {
            selectedIndex++;
            updateText ();

            updateSettings ();
        }
    }

    void updateText ()
    {
        text.text = options[selectedIndex];
    }

    void updateSettings ()
    {
    //    switch (mode)
    //    {
    //        case Settings.SettingMode.Difficulty:
    //            updateDifficulty ();
    //            break;
    //        case Settings.SettingMode.Displaymode:
    //            break;
    //        case Settings.SettingMode.MainVol:
    //            break;
    //        case Settings.SettingMode.MusicVol:
    //            break;
    //        case Settings.SettingMode.SoundEffectsVol:
    //            break;
        //}        
    }    
}
