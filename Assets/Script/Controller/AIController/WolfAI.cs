using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAI : AIController
{
    public override void Start ()
    {
        base.Start ();
	}

    public override void Update ()
    {
        base.Update ();
	}

    public override void OnSeeingTargetAction ()
    {
        base.OnSeeingTargetAction ();
        Debug.Log ("I am a Wolf and I am attacking you");
    }
}
