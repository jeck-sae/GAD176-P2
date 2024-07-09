using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class UnitAI : Unit
{
    public enum UnitState { Fighting, Chasing, Idling, Alert }

    [Tooltip("How far the unit can see")]
    public float visionRange = 10;
    [Tooltip("How wide the unit can see")]
    public float fieldOfView = 10;
    [Tooltip("Speed when chasing the target")]
    public float chaseSpeed = 6f;
    [Tooltip("How close to the target before the unit starts slowing down")]
    public float slowWhenNearTarget = 3.5f;

    [Tooltip("How long should it stay alert after losing sight of an enemy")]
    public float alertDuration = 5;
    [Tooltip("How long should it chase the target even after it's out of vision")]
    public float assumingTime = 3;

    [Tooltip("How far should the unit wander")]
    public float wanderDistance = 4;
    [Tooltip("How fast should it walk when wandering")]
    public float wanderSpeed = 4;
    [Tooltip("Pause duration when wandering")]
    public float wanderPauseDuration = 1;
    [Tooltip("Pause duration variation when wandering")]
    public float wanderPauseVariation = .5f;

    public Targetable target;

    [ShowInInspector, ReadOnly]
    protected UnitState state;
    protected bool firstFrameInCurrentState;
    private UnitState lastFrameState;

    protected float assumingTimer;
    protected float alertTimer;
    protected float wanderPauseTimer;
    protected Vector2 wanderDestination;
    protected Vector2 startPosition;

    protected override void Initialize()
    {
        if(startPosition == null) 
            startPosition = transform.position;

        base.Initialize();
    }

    protected void Update()
    {
        UpdateTargetIfNotValid();
        UpdateState();
        
        firstFrameInCurrentState = state != lastFrameState;

        InvokeCurrentBehavior();

        lastFrameState = state;
    }

    protected virtual void InvokeCurrentBehavior()
    {
        switch(state)
        {
            case UnitState.Idling:
                Idle();
                break;
            case UnitState.Fighting:
                Fight();
                break;
            case UnitState.Chasing:
                ChaseTarget();
                break;
            case UnitState.Alert:
                Alert();
                break;

            default:
                Debug.LogError("Invalid unit state");
                break;
        }
        
    }

    //By default, it idles until an enemy is in range, at which point it starts chasing it until
    //it's within the weapon range or the target escapes. When exiting a fighting state, it stays
    //alert for a while and walks around the current position and then goes back to idling.
    protected virtual void UpdateState()
    {
        if (target)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if(distance < weapon?.attackDistance)
            {
                state = UnitState.Fighting;
            }
            else
            {
                state = UnitState.Chasing;
            }

            alertTimer = alertDuration;
        }
        else
        {
            if (alertTimer >= 0)
            {
                state = UnitState.Alert;
                alertTimer -= Time.deltaTime;
            }
            else
            {
                state = UnitState.Idling;
            }
        }
    }

    public virtual void Fight()
    {
        LookAt(target.transform.position);
        TryAttacking();
    }

    public virtual void Alert()
    {
        Wander(transform.position);
    }

    public virtual void Idle()
    {
        Wander(startPosition);
    }

    //moves towards the player and gets slower the closer it gets
    public virtual void ChaseTarget()
    {
        LookAt(target.transform.position);
        MoveTowards(target.transform.position, chaseSpeed);
    }

    //Default: closest unit. Can be overridden (e.g. prioritizing enemis with higher armor or less HP) 
    public virtual Unit SelectIdealTarget(List<Unit> possibleTargets)
    {
        float minDistance = float.MaxValue;
        Unit closestUnit = null;
        foreach (Unit unit in possibleTargets)
        {
            float distance = (transform.position - unit.transform.position).sqrMagnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                closestUnit = unit;
            }
        }
        return closestUnit;
    }

    protected virtual bool IsValidTarget(Targetable target)
    {
        if (!target) return false;

        //checks if it's from an enemy faction. For more complex logic,
        //we could make a Faction Manager (e.g.: FactionManager.instance.IsEnemyOf(faction1, faction2) )
        if (target is Unit && (target as Unit).faction == faction)
            return false;

        return CanSee(target);
    }




    public void Wander(Vector2 wanderAroundPoint)
    {
        float distance = Vector2.Distance(transform.position, wanderDestination);
        if(firstFrameInCurrentState || distance < 0.2f || distance > wanderDistance)
        {
            //New destination
            float x = wanderAroundPoint.x + Random.Range(-wanderDistance, wanderDistance);
            float y = wanderAroundPoint.y + Random.Range(-wanderDistance, wanderDistance);
            wanderDestination = new Vector2(x, y);

            if(!firstFrameInCurrentState)
                wanderPauseTimer = wanderPauseDuration + (Random.Range(0, wanderPauseVariation) - wanderPauseVariation / 2);
        }

        wanderPauseTimer -= Time.deltaTime;
        if (wanderPauseTimer > 0)
            return;

        LookAt(wanderDestination);
        MoveTowards(wanderDestination, wanderSpeed);
    }


    public void MoveTowards(Vector3 targetPosition, float moveSpeed)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        float distance = Vector2.Distance(targetPosition, transform.position);
        float speed = moveSpeed * Mathf.Clamp01(distance/slowWhenNearTarget);
        Vector3 velocity = direction * speed;
        MoveBy(velocity * Time.deltaTime);
    }

    public void UpdateTargetIfNotValid()
    {
        if (target && IsValidTarget(target))
        {
            assumingTimer = assumingTime;
            return;
        }

        if(target && !target.isDead && assumingTimer > 0)
        {
            assumingTimer -= Time.deltaTime;
            return;
        }

        target = null;
        FindNewTarget();
    }


    public void FindNewTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, visionRange, GameManager.UnitsLayer);
        List<Unit> validTargets = new List<Unit>();

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out Unit unit))
                if (unit != this && IsValidTarget(unit)) 
                    validTargets.Add(unit);
        }

        target = SelectIdealTarget(validTargets);
    }

    protected bool CanSee(Targetable target)
    {
        //Check range
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if(distance >= visionRange) return false;

        //Check FOV
        Vector2 direction = (target.transform.position - transform.position).normalized;
        float angle = Vector2.Angle(transform.up, direction);
        if (angle > fieldOfView / 2)
            return false;

        //Check line of sight (if there is something in the way)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, GameManager.LevelLayer);
        if (hit.collider)
            return false;

        Debug.DrawRay(transform.position, direction * distance, Color.red);
        return true;
    }

}
