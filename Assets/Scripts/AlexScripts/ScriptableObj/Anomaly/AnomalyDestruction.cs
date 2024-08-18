using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyDestruction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)

    {


        if (other.gameObject.tag == "Player")
        {

            Destroy(other.gameObject);

            Debug.Log("Player is annihilated");



        }

    }

}
