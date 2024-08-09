using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMowe : MonoBehaviour
{

     [SerializeField] private Transform target;
  
    public float speed = 3f;


    void Start()
    {
        
    }

    
    void Update()
    {

        Vector3 direction = (target.position - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;

    }
}
