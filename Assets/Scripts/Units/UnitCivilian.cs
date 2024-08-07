using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class UnitCivilian : Unit
{
    public enum UnitState { Idling, Wandering, Fleeing, Alert }

    [Tooltip("How far the unit can see threats")]
    public float visionRange = 10;
    [Tooltip("How wide the unit can see threats")]
    public float fieldOfView = 10;
    [Tooltip("Speed when fleeing from a threat")]
    public float fleeSpeed = 6f;

    [Tooltip("How long should it stay alert after losing sight of a threat")]
    public float alertDuration = 5;

    [Tooltip("How far should the unit wander")]
    public float wanderDistance = 4;
    [Tooltip("How fast should it walk when wandering")]
    public float wanderSpeed = 2;
    [Tooltip("Pause duration when wandering")]
    public float wanderPauseDuration = 1;
    [Tooltip("Pause duration variation when wandering")]
    public float wanderPauseVariation = .5f;

    public Animator anim;
    public Targetable threat;
    protected Vector3 threatLastSeenPosition;
    [HideInInspector] public NavMeshAgent agent;

    [ShowInInspector, ReadOnly]
    protected UnitState state;
    protected bool firstFrameInCurrentState;
    private UnitState lastFrameState;

    protected float alertUntil;
    protected float wanderPauseTimer;
    protected Vector2 wanderDestination;
    protected Vector2 startPosition;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    protected override void Initialize()
    {
        if (startPosition == null)
            startPosition = transform.position;

        base.Initialize();
    }

    protected void Update()
    {
        UpdateThreatIfNotValid();

        if (threat)
            threatLastSeenPosition = threat.transform.position;

        UpdateState();

        firstFrameInCurrentState = state != lastFrameState;

        InvokeCurrentBehavior();

        lastFrameState = state;
    }

    protected virtual void InvokeCurrentBehavior()
    {
        switch (state)
        {
            case UnitState.Idling:
                Idle();
                break;
            case UnitState.Wandering:
                Wander(startPosition);
                break;
            case UnitState.Fleeing:
                FleeFromThreat();
                break;
            case UnitState.Alert:
                Alert();
                break;

            default:
                Debug.LogError("Invalid unit state");
                break;
        }
    }

    protected virtual void UpdateState()
    {
        if (threat)
        {
            state = UnitState.Fleeing;
            alertUntil = Time.time + alertDuration;
        }
        else
        {
            if (alertUntil >= Time.time)
            {
                state = UnitState.Alert;
            }
            else
            {
                state = UnitState.Idling;
            }
        }
    }

    public virtual void Alert()
    {

        if (alertUntil < Time.time)
        {
            Wander(startPosition);
        }
    }

    public virtual void Idle()
    {
        Wander(startPosition);
    }

    public virtual void FleeFromThreat()
    {
        Vector2 fleeDirection = (transform.position - threat.transform.position).normalized;
        Vector2 fleeTarget = (Vector2)transform.position + fleeDirection * fleeSpeed;

        agent.speed = fleeSpeed;
        agent.SetDestination(fleeTarget);
    }

    public void Wander(Vector2 wanderAroundPoint)
    {
        float distance = Vector2.Distance(transform.position, wanderDestination);
        if (firstFrameInCurrentState || distance < 0.2f || distance > wanderDistance)
        {
            float x = wanderAroundPoint.x + Random.Range(-wanderDistance, wanderDistance);
            float y = wanderAroundPoint.y + Random.Range(-wanderDistance, wanderDistance);
            wanderDestination = new Vector2(x, y);

            if (!firstFrameInCurrentState)
                wanderPauseTimer = wanderPauseDuration + (Random.Range(0, wanderPauseVariation) - wanderPauseVariation / 2);
        }

        wanderPauseTimer -= Time.deltaTime;
        if (wanderPauseTimer > 0)
            return;

        LookAt(wanderDestination);
        agent.speed = wanderSpeed;
        agent.SetDestination(wanderDestination);
    }

    public void UpdateThreatIfNotValid()
    {
        if (threat && IsValidThreat(threat))
        {
            return;
        }

        threat = null;
        FindNewThreat();
    }

    public void FindNewThreat()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, visionRange, GameManager.UnitsLayer);
        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out Unit unit))
                if (unit != this && IsValidThreat(unit))
                {
                    threat = unit;
                    break;
                }
        }
    }

    protected bool IsValidThreat(Targetable potentialThreat)
    {
        if (!potentialThreat) return false;

        float distance = Vector3.Distance(potentialThreat.transform.position, transform.position);
        if (distance >= visionRange) return false;

        Vector2 direction = (potentialThreat.transform.position - transform.position).normalized;
        float angle = Vector2.Angle(transform.up, direction);
        if (angle > fieldOfView / 2)
            return false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, GameManager.LevelLayer | GameManager.ShootThroughLayer);
        if (hit.collider)
            return false;

        return true;
    }
}