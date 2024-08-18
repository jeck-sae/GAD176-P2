using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGenerationOnTheMap : MonoBehaviour
{



    public int numberStones = 3; // number of stones on map

    public GameObject[] stonesForGeneration;

  //  public Transform[] stonesLocations; 


    public Vector2 mapZone =  new Vector2(4, 4); //borders of zone

    


    void Start()
    {


        StoneGeneration();

    }

    
    void StoneGeneration()
    {

        for (int i = 0; i < numberStones;  i++)       //     3 times
        {

            Vector2 spawnPosition = RandomPosition();     //Generate random position

            int randomItemIndex = Random.Range(0, stonesForGeneration.Length);    // You can choose any stone from  the array.

            Instantiate (stonesForGeneration[randomItemIndex],  spawnPosition, Quaternion.identity); //   Create a stone in a random position

            //Instantiate (stonesForGeneration[randomItemIndex], stonesLocations[i].position, Quaternion.identity); //   Create a stone in a random position


        }


    }


    Vector2 RandomPosition()
    { 

        float x = Random.Range(0, mapZone.x);  //  Generate a ranndom coordinate

        float y = Random.Range(0, mapZone.y);

        return new Vector2(x, y);


    }




}
