using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class MonstraAI : Unit
{
    public enum State { Scare, Attack, Retreat }
    public State currentState = State.Scare;
    public float speed = 10f;

    [Header("Scare")]
    public float scareDuration = 60f; // Duration of the Scare phase
    public float TryscareInterval = 20f; // Time interval
    public float circleSpeed = 5f;
    public float radius = 10f;
    public float ShowRadius = 5f;
    private float angle;
    private float scareTimer;
    private float TryScareTimer;

    [Header("Hunt")]
    public float attackSpeed = 15f;

    [Header("Retreat")]
    public float retreatDistance = 50f;
    public float timeToRecover = 10f;
    private bool leave = false;

    private NavMeshAgent agent;
    private Transform player;
    [HideInInspector] public Vector3 target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = speed;
        player = PlayerManager.Instance.player;
        scareTimer = scareDuration;
        TryScareTimer = TryscareInterval;
        angle = 0f;
        AudioManager.PlaySound(SoundType.Monstra, 0.2f);
    }

    void Update()
    {
        LookAt(target);

        switch (currentState)
        {
            case State.Scare:
                ScarePlayer();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Retreat:
                Retreat();
                break;
        }
    }

    void ScarePlayer()
    {
        scareTimer -= Time.deltaTime;
        TryScareTimer -= Time.deltaTime;

        if (TryScareTimer <= 0)
        {
            CirclePlayer(ShowRadius);
            if (TryScareTimer <= -20)
                TryScareTimer = TryscareInterval; // Reset the interval timer
        }
        else
        {
            // Circle around the player
            CirclePlayer(radius);
        }


        if (scareTimer <= 0)
        {
            TransitionToAttack();
        }
    }

    void CirclePlayer(float radius)
    {
        // Update the angle based on the circle speed
        angle += circleSpeed * Time.deltaTime;
        if (angle >= 360f) angle -= 360f; // Keep angle within 0-360 degrees

        // Calculate the new position on the circle
        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
        target = player.position + offset;

        // Use NavMeshAgent to move towards the position
        agent.SetDestination(target);

    }
    void TransitionToAttack()
    {
        currentState = State.Attack;
        AudioManager.PlaySound(SoundType.Monstra, 0.1f);
        leave = false;
    }

    void Attack()
    {
        target = player.position;
        // Logic to actively chase and attack the player
        agent.SetDestination(target);
        agent.speed = attackSpeed;
        Debug.Log("Kill");
    }
    void Retreat()
    {
        agent.SetDestination(target);
        if (!leave)
        {
            agent.speed = speed;
            Invoke("TransitionToAttack", timeToRecover);
            leave = true; // To run the method only once
        }
    }
    public void Leave()
    {
        leave = true;
        AudioManager.PlaySound(SoundType.Monstra, 0.1f);
        currentState = State.Retreat;
        retreatDistance = 300f;
        agent.speed = 35f;
        Vector3 leavePosition = GetPosOutsideView();
        target = leavePosition;
    }
    Vector3 GetPosOutsideView()
    {
        Vector3 RetreatPos;
        float angle = Random.Range(0, 360);
        Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0).normalized;

        // Calculate spawn position at a distance from the player
        RetreatPos = player.position + direction * retreatDistance;

        return RetreatPos;
    }


    //Changing unit's stuff
    public override void Damage(float amount, Unit attacker = null)
    {
        if(attacker is Player)
        {
            currentHealth -= amount;
            //play SFX
            AudioManager.PlaySoundAtPoint(SoundType.Damage, gameObject.transform.position, 0.8f);

            if (currentHealth <= 0)
            {
                AudioManager.PlaySound(SoundType.Monstra, 0.1f);
                currentState = State.Retreat;
                Vector3 leavePosition = GetPosOutsideView();
                target = leavePosition;
                currentHealth = maxHealth;
            }
        }
    }
    public override void TryAttacking()
    {
        //Nothing
    }
    public override void Die()
    {
        //Nothing just in case of something going wrong
    }
}
