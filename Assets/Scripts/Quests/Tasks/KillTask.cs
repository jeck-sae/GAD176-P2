using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KillTask", menuName = "Quest/Tasks/KillTask")]
public class KillTask : Task
{
    public List<GameObject> NPC;
    public bool randomSpawn = true;
    public Vector2 spawnPosition;

    public string targetID;
    public int requiredKillCount;
    private int currentKillCount;

    public override void Initialize()
    {
        if (NPC == null || NPC.Count == 0 || targetID == null)
        {
            Debug.LogError("NPC list or targetID is not set up");
            return;
        }

        int r = Random.Range(0, NPC.Count);
        GameObject selectedNPC;

        if (randomSpawn)
        {
            selectedNPC = NPC[r];
            SpawnerManager.Instance.SpawnKillTargets(NPC[r]);
        }
        else
        {
            selectedNPC = Instantiate(NPC[r], spawnPosition, Quaternion.identity);
        }

        Targetable targetable = selectedNPC.GetComponent<Targetable>();
        if (targetable != null)
        {
            targetable.ID = targetID;
        }
        else
        {
            Debug.LogError("NPC does not have a Targetable script");
        }

        base.Initialize();
        GameEvents.OnTargetableKilled += OnEnemyKilled;
        currentKillCount = 0;
    }
    public void OnEnemyKilled(Targetable killed, Unit attacker)
    {
        if (killed.ID == targetID && attacker is Player)
        {
            currentKillCount++;
            CheckProgress();
        }
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