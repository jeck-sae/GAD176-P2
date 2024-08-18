using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMessage : MonoBehaviour
{

    protected void QueenIsHuntingMessage()
    {

        Debug.Log("Queen is hunting");

    }



    void Start()
    {

        QueenIsHuntingMessage();  //Calling a protected function in the same class

    }

}
