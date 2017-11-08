using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class AIController : MonoBehaviour
{
    public StateMachine<AIController> stateMachine { get; set; }

    public float viewRadius = 5f;
    [Range (0, 90)]
    public float viewAngle = 60;
    [Range (0, 90)]
    public float viewVertAngle = 50;

    public float roamRadius = 5f;    
    public Transform PlayerTarget;

    public float distance;
    Vector3 direction;
    Vector3 rotDirection;
    public Vector3 lastKnownPosition;

    bool isInView;
    public bool isInAngle;
    public bool isClear;

    [HideInInspector]
    public NavMeshAgent myNavMeshAgent;
    Vector3 startPos;

    public List<WayPoint> wayPoints = new List<WayPoint> ();
    public WayPoint curWayPoint;
    public int wayPointIndex;
    public float wayPointDistance;
    
    public Action OnSeeCharacter;
    public Action OnMissCharacter;

    public virtual void Start ()
    {
        myNavMeshAgent = GetComponent<NavMeshAgent> ();
        startPos = transform.position;
        stateMachine = new StateMachine<AIController> (this);
        stateMachine.ChangeState (IdleState.Instance);
    }
	
	public virtual void Update ()
    {
        stateMachine.Update ();
    }

    public virtual void OnSeeingTargetAction ()
    {

    }

    public void CheckPlayerDistance (Transform target)
    {
        distance = Vector3.Distance (target.position, transform.position);
    }

    public void IsClearView (Transform target)
    {
        isClear = false;

        RaycastHit hit;
        Vector3 origin = transform.position;
        Debug.DrawRay (origin, direction * viewRadius, Color.red);

        if (Physics.Raycast (origin, direction, out hit, viewRadius))
        {
            //Debug.Log (hit.transform.name);
            if (hit.transform.CompareTag ("Player"))
                isClear = true;
        }
    }

    public void CheckAngle ()
    {
        if (rotDirection == Vector3.zero)
            rotDirection = transform.forward;

        float horiAngle = Vector3.Angle (transform.forward, rotDirection);
        float vertAngle = Vector3.Angle (direction, rotDirection);

        isInAngle = (horiAngle < viewAngle && vertAngle < viewVertAngle);
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
        myNavMeshAgent.SetDestination (targetPos);
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
        myNavMeshAgent.SetDestination (finalPosition);
    }

    void OnDrawGizmos ()
    {
        //Gizmos.DrawWireSphere (startPos, roamRadius);
        Gizmos.DrawWireSphere (transform.position, viewRadius);

        Gizmos.color = Color.yellow;
        Vector3 vector = Quaternion.Euler (0, viewAngle, 0) * transform.forward;
        Vector3 vector2 = Quaternion.Euler (0, -viewAngle, 0) * transform.forward;
        Gizmos.DrawRay (transform.position, vector * viewRadius);
        Gizmos.DrawRay (transform.position, vector2 * viewRadius);
        Gizmos.color = Color.black;
        Vector3 vector3 = Quaternion.AngleAxis (viewVertAngle, Vector3.left) * transform.forward;
        Gizmos.DrawRay (transform.position, vector3 * viewRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawRay (transform.position, rotDirection * viewRadius);
        
    }
}
