using System.Collections.Generic;
using UnityEngine;

public class Task : ScriptableObject
{
    public bool isCompleted;
    public virtual void Initialize()
    {
        isCompleted = false;
    }
    public virtual void CheckProgress()
    {
        CompleteTask();
    }
    public virtual void CompleteTask()
    {
        isCompleted = true;
    }
}

[CreateAssetMenu(fileName = "KillEnemyTask", menuName = "Quest/Tasks/KillEnemyTask")]
public class KillEnemyTask : Task
{
    public List<GameObject> NPC;

    public string enemyName;
    public int requiredKillCount;
    private int currentKillCount;

    public override void Initialize()
    {
        /*int r = Random.Range(0, NPC.Count);
        var createdNPC = Instantiate(NPC[r]);
        base.Initialize();
        currentKillCount = 0;*/
        GameEvents.OnTargetableKilled += OnEnemyKilled;
    }
    public void OnEnemyKilled(Targetable killed, Unit attacker)
    {
        if (killed.ID == enemyName && attacker is Player)
        {
            currentKillCount++;
            CheckProgress();
        }

        /*if (killedEnemyName == enemyName)
        {
            currentKillCount++;
            CheckProgress();
        }*/
    }
    public override void CheckProgress()
    {
        base.CheckProgress();
        // Check if the enemy has been killed enough times
        if (currentKillCount >= requiredKillCount)
        {
            CompleteTask();
        }
    }
    public override void CompleteTask()
    {
        base.CompleteTask();
        GameEvents.OnTargetableKilled -= OnEnemyKilled;
    }
}