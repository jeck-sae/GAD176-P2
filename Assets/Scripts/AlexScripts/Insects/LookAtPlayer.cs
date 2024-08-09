using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{

    [SerializeField] private Transform target;

   // public float speed = 5f;



    void Start()
    {
        
    }

    

    void Update()
    {

        //Vector3 direction = (target.position - transform.position).normalized;

        //Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        //transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * speed);

        Vector3 direction = target.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }
}