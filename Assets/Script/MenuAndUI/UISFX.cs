using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISFX : MonoBehaviour {

    public AudioClip onHoverSFX;
    public AudioClip onClickSFX;

    public void OnHoverSFX ()
    {
        if (onHoverSFX)
            SoundManager.PlaySFX (onHoverSFX);
        else
            Debug.Log ("No onHoverSFX assigned for " + gameObject.name);
    }

    public void OnClickSFX ()
    {
        if (onClickSFX)
            SoundManager.PlaySFX (onClickSFX);
        else
            Debug.Log ("No onClickSFX assigned for " + gameObject.name);
    }
}
