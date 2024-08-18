using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedAnomalyMove : MonoBehaviour
{

     

    public float speed = 1f; 

    public float acceleration = 2f;

    private bool moveToPlace = true;    

     

    public Transform firstPlace;

    public Transform secondPlace;

    private Rigidbody2D x; 



    void Start()
    {
        
        x = GetComponent<Rigidbody2D>(); //looks for the Rigidbody2D component on the game object and stores a reference to it in the x variable

    }

     //      After that I can use it to control the physics of the object


    void Update()
    {
   
      Vector2 destination = moveToPlace ? secondPlace.position : firstPlace.position;  //determine the direction


        Vector2 direction = (destination - (Vector2)transform.position).normalized;



        x.velocity = direction * speed;

        speed += acceleration * Time.deltaTime;

        if (Vector2.Distance(transform.position, destination) < 0.1f)
        {
            moveToPlace = !moveToPlace;  //chaange direction

            speed = 1f;   // reset base speed
        }


    }











}
