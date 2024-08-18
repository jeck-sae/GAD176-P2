using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyRotation : MonoBehaviour
{



    public float speed = 5f;  // 

    public float distanse = 1f;

    private float firstPosition;

    private int vector = 1;


    void Start()
    {

        firstPosition = transform.position.x;

    }


    void Update()
    {

        float actualPosition = transform.position.x;

        if (Mathf.Abs(actualPosition - firstPosition) >= distanse)
        {

            vector *= -1;

        }


        transform.Translate(vector * speed * Time.deltaTime, 0, 0); 



    }




} 

