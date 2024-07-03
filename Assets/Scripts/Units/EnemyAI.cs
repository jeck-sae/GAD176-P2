using UnityEngine;

public class EnemyAI : Unit
{
    public float visionRange = 10;
    public float rotationSpeedWhenIdle = 10;
    public float chaseSpeed = 6f;
    public float slowWhenNearTarget = 3.5f;

    public override void ManagedUpdate()
    {
        BehaviorUpdate();
    }

    //Handles the behavior of the unit. By default, it idles until an enemy is in range,
    //at which point it starts chasing it until it's within the weapon range or the target
    //escapes. When it is within weapon range, it attacks.
    public virtual void BehaviorUpdate()
    {
        TargetUpdate();

        if (target)
        {
            LookAt(target.transform.position);

            float distance = Vector3.Distance(target.transform.position, transform.position);
            if(distance < weapon.attackDistance)
            {
                TryAttacking();
            }
            else
            {
                ChaseTarget();
            }
        }
        else
        {
            Idle();
        }
    }


    public void TargetUpdate()
    {
        if (target && IsValidTarget(target))
            return;
        target = null;

        // TEMPORARY ===========================
        var player = FindObjectOfType<Player>();
        if(player && IsValidTarget(player)) 
            target = player;
    }

    //checks if the target is within vision range.
    //TODO: add check for direct line of sight (so that the
    //target is not valid if there is a wall in the way)
    protected bool IsValidTarget(Targetable target)
    {
        if (!target) return false;
        float distance = Vector3.Distance(target.transform.position, transform.position);
        return distance < visionRange;
    }

    //Default idle behavior is to slowly rotate
    public void Idle()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + Vector3.forward * Time.deltaTime * rotationSpeedWhenIdle);
    }

    //moves towards the player and gets slower the closer it gets
    public void ChaseTarget()
    {
        var direction = (target.transform.position - transform.position).normalized;
        var distance = Vector2.Distance(target.transform.position, transform.position);
        var speed = chaseSpeed * Mathf.Clamp01(distance/slowWhenNearTarget);
        var velocity = direction * speed;
        MoveBy(velocity * Time.deltaTime);
    }


}
