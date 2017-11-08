using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AIBehaviors
{
    public static void PatrolBehaviorEnter (AIController ai)
    {
        if (ai.wayPoints.Count > 0)
        {
            ai.curWayPoint = ai.wayPoints[ai.wayPointIndex];
            ai.MoveToPosition (ai.curWayPoint.targetDestination.position);
        }
    }

    public static void PatrolBehaviorUpdate (AIController ai)
    {
        if (ai.wayPoints.Count > 0)
        {
            //if (ai.curWayPoint.targetDestination == null)
            //{
            //    Debug.Log ("test");
            //    ai.curWayPoint = ai.wayPoints[ai.wayPointIndex];
            //    ai.MoveToPosition (ai.curWayPoint.targetDestination.position);
            //}

            if (ai.myNavMeshAgent.isStopped == true)
                ai.myNavMeshAgent.isStopped = false;

            ai.curWayPoint = ai.wayPoints[ai.wayPointIndex];
            ai.wayPointDistance = Vector3.Distance (ai.transform.position, ai.curWayPoint.targetDestination.position);

            //Debug.Log (ai.myNavMeshAgent.remainingDistance);

            //if (ai.wayPointDistance < 2)
            if (!ai.myNavMeshAgent.pathPending && ai.myNavMeshAgent.remainingDistance < 0.5f)
            {
                if (ai.wayPointIndex < ai.wayPoints.Count - 1)
                {
                    ai.wayPointIndex++;
                }
                else
                {
                    ai.wayPointIndex = 0;
                    //ai.wayPoints.Reverse ();
                }

                ai.curWayPoint = ai.wayPoints[ai.wayPointIndex];
                ai.MoveToPosition (ai.curWayPoint.targetDestination.position);
            }
        }
    }




}

[System.Serializable]
public class WayPoint
{
    public Transform targetDestination;
}
