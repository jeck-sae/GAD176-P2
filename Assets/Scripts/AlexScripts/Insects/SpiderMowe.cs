using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpiderMowe : MonoBehaviour
{

    //PlayerManager.Instance.Player

    [SerializeField] private Transform target;
  
    public float speed = 3f;
    private Transform player;

    void Start()
    {

        
        //target = target.transform;
        player = PlayerManager.Instance.player;

    }

    
    void Update()
    {

        //Vector3 direction = (target.position - transform.position).normalized;
        Vector3 direction = (player.position - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;

    }

    public void RewriteTarget(Transform rewritedTarget) 
    {

        //target = rewritedTarget;
        player = rewritedTarget;
    }

}
