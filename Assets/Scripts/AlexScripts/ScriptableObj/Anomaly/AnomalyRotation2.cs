using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyRotation2 : MonoBehaviour
{

    public float speed = 15f;


    void Start()
    {

       

    }

   
    void Update()
    {

        

        transform.Rotate(0, 0,  speed * Time.deltaTime);

         
    }

}
