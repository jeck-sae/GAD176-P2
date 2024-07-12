using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : Singleton<SpawnerManager>
{
    public List<Transform> SpeakTaskTargetsSpawnPos;
    public List<Transform> KillTaskTargetsSpawnPos;
    public List<Transform> ExplorationTargetsSpawnPos;

    public void SpawnSpeakTargets(GameObject npc)
    {
        int random = Random.Range(0, SpeakTaskTargetsSpawnPos.Count);
        Instantiate(npc, SpeakTaskTargetsSpawnPos[random]);
    }

    public void SpawnKillTargets(GameObject npc)
    {
        int random = Random.Range(0, KillTaskTargetsSpawnPos.Count);
        Instantiate(npc, KillTaskTargetsSpawnPos[random]);
    }
    public void SpawnExplorationTargets(GameObject obj)
    {
        int random = Random.Range(0, ExplorationTargetsSpawnPos.Count);
        Instantiate(obj, ExplorationTargetsSpawnPos[random]);
    }
}
