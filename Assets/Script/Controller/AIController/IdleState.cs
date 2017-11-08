using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State<AIController>
{
    #region Singleton
    private static IdleState _instance;

    private IdleState ()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static IdleState Instance
    {
        get
        {
            if (_instance == null)
            {
                new IdleState ();
            }

            return _instance;
        }
    }
    #endregion

    public override void EnterState (AIController owner)
    {
        Debug.Log ("Idle");
    }

    public override void ExecuteState (AIController owner)
    {
        if (owner.PlayerTarget == null)
            return;

        owner.CheckPlayerDistance (owner.PlayerTarget);
        AIBehaviors.PatrolBehaviorUpdate (owner);

        if (owner.distance < owner.viewRadius)
            owner.stateMachine.ChangeState (TargetInRangeState.Instance);
    }

    public override void ExitState (AIController owner)
    {

    }
}
