using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : Singleton<SpawnerManager>
{
    public List<SpawnTarget> SpeakTaskTargets;
    public List<SpawnTarget> KillTaskTargets;
    public List<SpawnTarget> ExplorationTargets;
    public void SpawnSpeakTargets(GameObject npc)
    {
        int random = Random.Range(0, SpeakTaskTargets.Count);
        SpawnTarget target = SpeakTaskTargets[random];
        Instantiate(npc, target.SpawnPos.position, Quaternion.identity);
        DialogueManager.Instance.replacementText = target.replacementText;
        DialogueManager.Instance.replacement = true;
    }

    public void SpawnKillTargets(GameObject npc)
    {
        int random = Random.Range(0, KillTaskTargets.Count);
        SpawnTarget target = KillTaskTargets[random];
        Instantiate(npc, target.SpawnPos.position, Quaternion.identity);
        DialogueManager.Instance.replacementText = target.replacementText;
        DialogueManager.Instance.replacement = true;
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