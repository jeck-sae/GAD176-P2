using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    //Storing objects on the stage

    [CreateAssetMenu(fileName = "Hive", menuName = "Create hive")] //menu

    public class Hive : ScriptableObject 
    {

        public List<HiveObject> HiveObjects = new List<HiveObject>(); //list of objects

    }


    [System.Serializable]

    public class HiveObject //Storing an object and its position on the scene
{

        public GameObject Prefab;

        public Vector3 Position;

    }
   
     




