using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExplorationTask", menuName = "Quest/Tasks/ExplorationTask")]
public class ExplorationTask : Task
{
    public GameObject ExplorationObj;
    public bool randomSpawn = true;
    public Vector2 spawnPosition;
    public override void Initialize(Quest quest)
    {

        if (randomSpawn)
        {
            SpawnerManager.Instance.SpawnExplorationTargets(ExplorationObj);
        }
        else
        {
            Instantiate(ExplorationObj, spawnPosition, Quaternion.identity);

            ExplorationTrigger explorationTrigger = ExplorationObj.GetComponent<ExplorationTrigger>();
            if (explorationTrigger != null)
            {
                explorationTrigger.explor = this;
            }
            else
            {
                Debug.LogError("no ExplorationTrigger");
            }
        }

        base.Initialize(quest);
    }

    public override void CheckProgress()
    {
        base.CheckProgress();
    }
    public override void CompleteTask()
    {
        base.CompleteTask();
    }
}
