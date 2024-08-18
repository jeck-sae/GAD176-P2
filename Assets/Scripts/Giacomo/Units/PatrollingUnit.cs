using NavMeshPlus.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingUnit : UnitAI
{
    public List<PatrolWaypoint> waypoints;
    public int firstWaypoint = 0;

    protected int currentWaypointIndex;
    protected float patrolWaitTimer;

    protected override void Initialize()
    {
        currentWaypointIndex = firstWaypoint;
        base.Initialize();
    }

    public override void Idle()
    {
        if(patrolWaitTimer > 0)
        {
            patrolWaitTimer -= Time.deltaTime;
            return;
        }

        var currentWaypoint = waypoints[currentWaypointIndex];

        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].destination.position) < 0.2f)
        {
            patrolWaitTimer = currentWaypoint.waitBeforeContinuing;
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
            
            if (patrolWaitTimer > 0)
                return;

            currentWaypoint = waypoints[currentWaypointIndex];
        }

        LookAt(currentWaypoint.destination.position);
        agent.speed = wanderSpeed;
        agent.SetDestination(currentWaypoint.destination.position);
    }

    [Serializable]
    public class PatrolWaypoint
    {
        public Transform destination;
        public float waitBeforeContinuing;
    }
}
