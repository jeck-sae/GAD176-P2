using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectScr : MonoBehaviour
{


    public float moweSpeed = 10f;

    public float timeToLive = 2f;

    private float timeSinceSpawned = 0f;
    



    void Start()
    {
        
    }

    
    void Update()
    {

        transform.position += moweSpeed * transform.right * Time.deltaTime;

        timeSinceSpawned += Time.deltaTime;

        if(timeSinceSpawned > timeToLive)
        {

            Destroy(gameObject);

        }

    }
}
