using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public string cityName;
    public Faction faction;
    public float spawnRadius = 50f;
    public int cityPopulation = 15; // thats more like a village, but who cares
    public int solders = 10;
    public GameObject factionSolder;
    public List<Transform> houses; // List of house
    public List<GameObject> NPC; // List of NPC prefabs to spawn
    private List<GameObject> spawnedNPCs = new List<GameObject>();

    public void SpawnNPCs()
    {
        for(int i = 0; i < solders; i++)
        {
            GameObject spawnedNPC = Instantiate(factionSolder, RandomSpawn(), Quaternion.identity);
            spawnedNPCs.Add(spawnedNPC);
        }
        for (int i = 0; i < cityPopulation; i++)
        {
            int R = Random.Range(0, NPC.Count);
            GameObject spawnedNPC = Instantiate(NPC[R], RandomSpawn(), Quaternion.identity);
            spawnedNPCs.Add(spawnedNPC);
            AssignRandomHouse(spawnedNPC);
        }
    }

    void AssignRandomHouse(GameObject npc)
    {
        if (houses.Count == 0)
        {
            Debug.LogWarning("No houses available to assign to NPCs!");
            return;
        }

        int houseIndex = Random.Range(0, houses.Count);
        Transform assignedHouse = houses[houseIndex];

        UnitCivilian npcBehavior = npc.GetComponent<UnitCivilian>();
        if (npcBehavior != null)
        {
            npcBehavior.home = assignedHouse;
            npcBehavior.faction = faction;
        }
        else
        {
            Debug.LogWarning("NPC does not have an NPCBehavior script to assign a house!");
        }
    }


    public void DespawnNPCs()
    {
        foreach (GameObject npc in spawnedNPCs)
        {
            Destroy(npc);
        }
        spawnedNPCs.Clear();
    }

    Vector2 RandomSpawn()
    {
        return new Vector2(transform.position.x + Random.Range(-10, 10), transform.position.y + Random.Range(-10, 10));
    }
}