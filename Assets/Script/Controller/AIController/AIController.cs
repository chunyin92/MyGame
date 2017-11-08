using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class AIController : MonoBehaviour
{
    public StateMachine<AIController> stateMachine { get; set; }

    public float viewRadius = 5f;
    [Range(0,360)]
    public float viewAngle = 45;
    public float roamRadius = 5f;    
    public Transform PlayerTarget;

    // TODO make public field private and create a public method that return the private value??
    public float distance;
    Vector3 direction;
    Vector3 rotDirection;
    public Vector3 lastKnownPosition;

    bool isInView;
    public bool isInAngle;
    public bool isClear;

    [HideInInspector]
    public NavMeshAgent myNavMeshAgent;
    //[HideInInspector]
    //public BehaviorsInfo myBehaviorsInfo;
    Vector3 startPos;

    public List<WayPoint> wayPoints = new List<WayPoint> ();
    public WayPoint curWayPoint;
    public int wayPointIndex;
    public float wayPointDistance;
    
    public Action OnSeeCharacter;
    public Action OnMissCharacter;

    public virtual void Start ()
    {
        stateMachine = new StateMachine<AIController> (this);

        myNavMeshAgent = GetComponent<NavMeshAgent> ();
        //myBehaviorsInfo = GetComponent<BehaviorsInfo> ();
        startPos = transform.position;

        stateMachine.ChangeState (IdleState.Instance);
    }
	
	public virtual void Update ()
    {
        stateMachine.Update ();
    }

    public virtual void SpecificAction ()
    {

    }

    public void CheckPlayerDistance (Transform target)
    {
        distance = Vector3.Distance (target.position, transform.position);

        //if (distance > viewRadius && stateMachine.currentState != IdleState.Instance)
        //{
        //    stateMachine.ChangeState (IdleState.Instance);
        //}else if (distance < viewRadius && stateMachine.currentState != TargetInRangeState.Instance)
        //{
        //    stateMachine.ChangeState (TargetInRangeState.Instance);
        //}
    }

    public void IsClearView (Transform target)
    {
        isClear = false;

        RaycastHit hit;
        Vector3 origin = transform.position;
        Debug.DrawRay (origin, direction * viewRadius);

        if (Physics.Raycast(origin,direction,out hit, viewRadius))
        {
            //Debug.Log (hit.transform.name);
            if (hit.transform.CompareTag("Player"))
            {
                isClear = true;
            }
        }
    }

    public void CheckAngle ()
    {
        if (rotDirection == Vector3.zero)
            rotDirection = transform.forward;

        float angle = Vector3.Angle (transform.forward, rotDirection);

        isInAngle = (angle < viewAngle);
    }

    public void RotateTowardsTarget ()
    {
        if (rotDirection == Vector3.zero)
            rotDirection = transform.forward;

        Quaternion lookRotation = Quaternion.LookRotation (rotDirection);
        //Quaternion lookRotation = Quaternion.LookRotation (new Vector3 (direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void GetDirection (Transform target)
    {
        direction = (target.position - transform.position).normalized;
        rotDirection = direction;
        rotDirection.y = 0;
    }

    public void MonitorTargetPosition ()
    {
        float delta_distance = Vector3.Distance (PlayerTarget.position, lastKnownPosition);
        if (delta_distance>2)
        {
            lastKnownPosition = PlayerTarget.position;
            MoveToPosition (lastKnownPosition);
        }
    }

    public void MoveToPosition (Vector3 targetPos)
    {
        myNavMeshAgent.isStopped = false;
        //myNavMeshAgent.SetDestination (targetPos);
        myNavMeshAgent.destination = targetPos;
    }

    public void StopMoving ()
    {
        myNavMeshAgent.isStopped = true;
    }

    void Roam ()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitCircle * roamRadius;
        randomDirection += startPos;
        NavMeshHit hit;
        NavMesh.SamplePosition (randomDirection, out hit, roamRadius, 1);
        Vector3 finalPosition = hit.position;

        //if (!myNavMeshAgent.pathPending)
        myNavMeshAgent.destination = finalPosition;
    }

    void OnDrawGizmos ()
    {
        //Gizmos.DrawWireSphere (startPos, roamRadius);
        Gizmos.DrawWireSphere (transform.position, viewRadius);

        //Gizmos.color = Color.red;
        //Vector3 a = new Vector3 (Mathf.Sin (viewAngle * Mathf.Deg2Rad), 0, Mathf.Cos (viewAngle * Mathf.Deg2Rad));
        //Gizmos.DrawRay (transform.position, transform.position + a / 2 * viewRadius);
        //Gizmos.DrawRay (transform.position, transform.position - a / 2 * viewRadius);
    }
}
