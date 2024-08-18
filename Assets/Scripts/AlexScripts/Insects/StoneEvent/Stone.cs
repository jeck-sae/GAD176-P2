using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{

    // [SerializeField] private int _reward;

    public static event System.Action TakeStone; //Stone collection event


    void Start()
    {
        

    }

    
    private void OnTriggerEnter2D(Collider2D other) 
    {


         if (other.CompareTag("Player"))
         {
             
            TakeStone?.Invoke();     //event call


            Destroy(gameObject);


         }


    }



}
