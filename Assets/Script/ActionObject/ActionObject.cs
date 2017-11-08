using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A Class that handle an action after interacted. e.g. door
public class ActionObject : Interactable
{
    public override void Interact ()
    {
        Debug.Log ("Interacting with action object. DOOR?");
    }
}
