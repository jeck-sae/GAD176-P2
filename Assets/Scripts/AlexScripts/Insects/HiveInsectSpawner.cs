using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveInsectSpawner : MonoBehaviour
{


    public Transform[] spavnPlaces; //array

    public GameObject spiderSpavn; // spiders for spavning

    private bool spavnedOrNotSpavned = false;

   // public float time = 240f;


    void OnTriggerEnter2D(Collider2D other)
    {
        
        if ( !spavnedOrNotSpavned && other.CompareTag("Player"))
        {

            foreach (Transform spawnPoint in spavnPlaces) // cycle for all spawnpoints
            {
                 
             
                Instantiate(spiderSpavn, spawnPoint.position, Quaternion.identity);

                 
                  

            }

            spavnedOrNotSpavned = true; //no spavn anymore

        }
                    
          

    }


   
    void Update()
    {
        
    }
}
