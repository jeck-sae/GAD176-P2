using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyRayCast : MonoBehaviour
{

    //public Transform Pointer;
    

    void Update()
    {

        // Ray2D ray = new Ray2D(transform.position, transform.right); //beginning and direction of ray

        Debug.DrawRay(transform.position, transform.right * 20f, Color.red); //visualise ray

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);



        if (hit.collider != null) 
        {

            //if (hit.collider.CompareTag("Player"))
            //{

                //    Debug.Log("Player on the way");

                //}

            //else
            //{
                Debug.Log("Obstacle in the path of anomaly");
            //}

        }



    }

}





//https://docs.unity3d.com/ScriptReference/RaycastHit.html

//RaycastHit hit;

//if  (Physics.Raycast(ray, out hit))
// {

//     Pointer.position = hit.point; // place of hit

//     Debug.Log("Obstacle in the path of anomaly");

// }