using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class Enconter : RandomEvent
{
    public List<GameObject> NPC;
    public float spawnRange = 5f;

    public override void Initialize()
    {
        base.Initialize();
        int r = Random.Range(0, NPC.Count);
        triger.SpawnEncounter(NPC[r], spawnRange);
    }
}
