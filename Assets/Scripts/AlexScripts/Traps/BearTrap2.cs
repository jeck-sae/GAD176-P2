using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap2 : MonoBehaviour
{
    public string tagTarget = "Player";

    public List<Collider2D> detectedObjs = new List<Collider2D>();




    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.tag == tagTarget)
        {

            Destroy(gameObject, 4f);


        }

    }


    void OnTriggerExit2D(Collider2D collider)
    {

    }
}
