using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour// a very bad script
{
    public GameObject activateObject;
    public GameObject deactivateObject;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(activateObject != null)
            {
                activateObject.SetActive(true);
            }
            if (deactivateObject != null)
            {
                deactivateObject.SetActive(false);
            }
        }
    }
}
