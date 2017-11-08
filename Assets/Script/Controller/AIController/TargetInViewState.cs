using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetInViewState : State<AIController>
{
    #region Singleton
    private static TargetInViewState _instance;

    private TargetInViewState ()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }

    public static TargetInViewState Instance
    {
        get
        {
            if (_instance == null)
            {
                new TargetInViewState ();
            }

            return _instance;
        }
    }
    #endregion

    public override void EnterState (AIController owner)
    {
        owner.OnSeeCharacter.Invoke ();

        owner.lastKnownPosition = owner.PlayerTarget.position;
        owner.MoveToPosition (owner.lastKnownPosition);
        Debug.Log ("Target In View");
    }

    public override void ExecuteState (AIController owner)
    {
        if (owner.PlayerTarget == null)
            return;

        owner.CheckPlayerDistance (owner.PlayerTarget);
        owner.GetDirection (owner.PlayerTarget);
        owner.CheckAngle ();
        owner.IsClearView (owner.PlayerTarget);
        owner.RotateTowardsTarget ();
        owner.MonitorTargetPosition ();

        if (!owner.isClear)
            owner.stateMachine.ChangeState (TargetInRangeState.Instance);
    }

    public override void ExitState (AIController owner)
    {
        owner.OnMissCharacter.Invoke ();
    }
}
