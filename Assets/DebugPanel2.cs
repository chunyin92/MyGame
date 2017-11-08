using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugPanel2 : MonoBehaviour
{
    public TextMeshProUGUI[] textMeshProGroup;

    void Update ()
    {
        textMeshProGroup[0].text = MenuManager.instance.settings.difficulty.ToString ();
        textMeshProGroup[1].text = MenuManager.instance.settings.isFullScreen.ToString ();
        textMeshProGroup[2].text = MenuManager.instance.settings.resoulutionIndex.ToString ();
        textMeshProGroup[3].text = MenuManager.instance.settings.textureQuality.ToString ();
        textMeshProGroup[4].text = MenuManager.instance.settings.antialiasing.ToString ();
        textMeshProGroup[5].text = MenuManager.instance.settings.vSync.ToString ();
        textMeshProGroup[6].text = MenuManager.instance.settings.mainVol.ToString ();
        textMeshProGroup[7].text = MenuManager.instance.settings.BGMVol.ToString ();
    }	
}
