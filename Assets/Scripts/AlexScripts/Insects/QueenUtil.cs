using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenUtill : MonoBehaviour    ////allows only one object of this class to exist on the scene (removes extra objects when the scene starts)
{


    public static QueenUtill instance;






    void Awake()                   //checks for the existence of a Util instance
    {

        if (instance == null)
        {

            instance = this;

            DontDestroyOnLoad(gameObject);



        }

        else
        {

            Destroy(gameObject);

        }



    }





}
