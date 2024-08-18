using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class GeneralSpiderFeatures : MonoBehaviour
{

    protected int legs = 8;

    [SerializeField]

    private float speed;


    

    public virtual void run()
    {
        Debug.Log("Spider is rinning");
    }

    public virtual void attack()
    {
        Debug.Log("Spider is attacking");
    }

}

