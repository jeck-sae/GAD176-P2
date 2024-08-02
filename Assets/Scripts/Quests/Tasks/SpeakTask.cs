using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SpeakTask", menuName = "Quest/Tasks/SpeakTask")]
public class SpeakToNPCTask : Task
{
    public Dialogue dialogue;
    public List<GameObject> NPC;
    public bool randomSpawn = true;
    public Vector2 spawnPosition;
    public Quest Taskquest;

    public override void Initialize()
    {
        if (NPC == null || NPC.Count == 0 || dialogue == null)
        {
            Debug.Log("NPC list or dialogue is not set up");
            return;
        }

        int r = Random.Range(0, NPC.Count);
        GameObject selectedNPC;

        if (randomSpawn)
        {
            selectedNPC = NPC[r];
            SpawnerManager.Instance.SpawnSpeakTargets(NPC[r]);

        }
        else
        {
            selectedNPC = Instantiate(NPC[r], spawnPosition, Quaternion.identity);
        }

        DialogueTrigger dialogueTrigger = selectedNPC.GetComponent<DialogueTrigger>();
        if (dialogueTrigger != null)
        {
            dialogue.Dialoguequest = Taskquest;
            dialogueTrigger.tdialogue = dialogue;
        }
        else
        {
            Debug.Log("NPC does not have a DialogueTrigger");
        }

        base.Initialize();
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
