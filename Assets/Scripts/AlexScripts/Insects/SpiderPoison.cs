using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderPoison : MonoBehaviour         //Attach to player
{
    [SerializeField]
    public float poisonHealth = 200f;

    public float poisonDamage = 1f;

    private bool poisonContact = false;

    void Start()
    {
        
    }

    
    void Update()
    {

        if (poisonContact)
        {

            poisonHealth -= poisonDamage * Time.deltaTime;

            if ( poisonHealth <=0 )
            {

                Destroy(gameObject);

            }


        }
        
    }


    private void OnTriggerEnter2D(Collider2D other)

    {


        if (other.gameObject.tag == "Spider")
        {

            poisonContact = true;

        }

    }



    private void OnTriggerExit2D(Collider2D other) 

    {


        if (other.gameObject.tag == "Spider")
        {

            poisonContact = false;

        }

    }








}
