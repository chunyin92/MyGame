using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetInRangeState : State<AIController>
{
    #region Singleton
    private static TargetInRangeState _instance;

    private TargetInRangeState ()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static TargetInRangeState Instance
    {
        get
        {
            if (_instance == null)
            {
                new TargetInRangeState ();
            }

            return _instance;
        }
    }
    #endregion

    public override void EnterState (AIController owner)
    {
        AIBehaviors.PatrolBehaviorEnter (owner);
        Debug.Log ("Target In Range");
    }

    public override void ExecuteState (AIController owner)
    {
        if (owner.PlayerTarget == null)
            return;

        owner.CheckPlayerDistance (owner.PlayerTarget);
        owner.GetDirection (owner.PlayerTarget);
        owner.CheckAngle ();
        owner.IsClearView (owner.PlayerTarget);
        AIBehaviors.PatrolBehaviorUpdate (owner);

        if (owner.distance > owner.viewRadius)
            owner.stateMachine.ChangeState (IdleState.Instance);

        if (owner.isClear && owner.isInAngle)
            owner.stateMachine.ChangeState (TargetInViewState.Instance);
    }

    public override void ExitState (AIController owner)
    {
        owner.StopMoving ();
    }
}
