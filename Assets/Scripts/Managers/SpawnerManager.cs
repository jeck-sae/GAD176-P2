using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : Singleton<SpawnerManager>
{
    public List<SpawnTarget> SpeakTaskTargets;
    public List<SpawnTarget> KillTaskTargets;
    public List<SpawnTarget> ExplorationTargets;
    public void SpawnSpeakTargets(GameObject npc, Dialogue dialogue, Quest quest)
    {
        int random = Random.Range(0, SpeakTaskTargets.Count);
        SpawnTarget target = SpeakTaskTargets[random];
        GameObject selectedNPC;
        selectedNPC = Instantiate(npc, target.SpawnPos.position, Quaternion.identity);
        DialogueTrigger dialogueTrigger = selectedNPC.GetComponent<DialogueTrigger>();
        if (dialogueTrigger != null)
        {
            dialogueTrigger.dialogueQuest = quest;
            dialogueTrigger.tdialogue = dialogue;
        }
        else
        {
            Debug.Log("NPC does not have a DialogueTrigger");
        }
        DialogueManager.Instance.replacementText = target.replacementText;
        DialogueManager.Instance.replacement = true;
    }

    public void SpawnKillTargets(GameObject npc, string targetID)
    {
        int random = Random.Range(0, KillTaskTargets.Count);
        SpawnTarget target = KillTaskTargets[random];
        GameObject selectedNPC;
        selectedNPC = Instantiate(npc, target.SpawnPos.position, Quaternion.identity);
        DialogueManager.Instance.replacementText = target.replacementText;
        DialogueManager.Instance.replacement = true;

        Targetable targetable = selectedNPC.GetComponent<Targetable>();
        if (targetable != null)
        {
            targetable.ID = targetID;
        }
        else
        {
            Debug.LogError("NPC does not have a Targetable script");
        }
    }
    public void SpawnExplorationTargets(GameObject obj)
    {
        int random = Random.Range(0, ExplorationTargets.Count);
        SpawnTarget target = ExplorationTargets[random];
        Instantiate(obj, target.SpawnPos.position, Quaternion.identity);
        DialogueManager.Instance.replacementText = target.replacementText;
        DialogueManager.Instance.replacement = true;
    }
}

[System.Serializable]
public class SpawnTarget
{
    public Transform SpawnPos;
    public string replacementText; // The string to use for replacement
}