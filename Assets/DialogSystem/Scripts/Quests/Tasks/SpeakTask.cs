using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SpeakTask", menuName = "Quest/Tasks/SpeakTask")]
public class SpeakToNPCTask : Task
{
    public Dialogue dialogue;
    public List<GameObject> NPC;
    public Quest Taskquest;

    public override void Initialize()
    {
        if (NPC == null || NPC.Count == 0 || dialogue == null)
        {
            Debug.LogError("NPC list or dialogue is not set up");
            return;
        }

        dialogue.Dialoguequest = Taskquest;
        int r = Random.Range(0, NPC.Count);
        var createdNPC = Instantiate(NPC[r]);
        createdNPC.GetComponent<DialogueTrigger>().tdialogue = dialogue;

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
