using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorationTrigger : MonoBehaviour
{
    [HideInInspector]public ExplorationTask explor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            explor.CompleteTask();
        }
    }
}
