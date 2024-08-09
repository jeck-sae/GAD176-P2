using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveSpawnPoint : MonoBehaviour
{
    public GameObject projectileTrap;

    public Transform spawnLocation;

    public Quaternion spawnRotation;

    public DetectionZone detectionZone;

    public float spawnTime = 0.5f;

    private float timeSinceSpawned = 0.5f;


    void Start()
    {

    }


    void Update()
    {

        if (detectionZone.detectedObjs.Count > 0)
        {


            timeSinceSpawned += Time.deltaTime;

            if (timeSinceSpawned >= spawnTime)
            {

                Instantiate(projectileTrap, spawnLocation.position, spawnRotation);

                timeSinceSpawned = 0f;
            }


        }

        else
        {
            timeSinceSpawned = 0.5f;
        }

    }
}
