using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class HiveEditor : EditorWindow
{

    [SerializeField]

    private Hive _hive;   //reference to an object with data

    [MenuItem("Window/Hive editor")]

    public static void Init()                //Method of displaying the editor window
    {

        HiveEditor hiveEditor = GetWindow<HiveEditor>("Hive editor");

        hiveEditor.Show();

    }

    
    private void OnGUI()
    {

        GUILayout.Space(10);                                  //indent, field for data object

        _hive = (Hive)EditorGUILayout.ObjectField(_hive, typeof(Hive), false);

        GUILayout.Space(5);

        if(GUILayout.Button("Clear"))
        {

            GameObject[] findObjects = GameObject.FindGameObjectsWithTag("HiveObject");  //find and delete all objects with the tag

            foreach (var item in findObjects)
            {

                DestroyImmediate(item);

            }

        }

        GUILayout.Space(5);

        if(GUILayout.Button("Generate"))
        {

            foreach (var item in _hive.HiveObjects)
            {

                GameObject obj = PrefabUtility.InstantiatePrefab(item.Prefab) as GameObject; //assigning a prefab created via PrefabUtility from a saved prefab by casting it to the GameObject type

                obj.transform.position = item.Position; //set a saved position for this object

            }

        }


        GUILayout.Space(5);

        if (GUILayout.Button("Save"))
        {

            var saveObjects = GameObject.FindGameObjectsWithTag("HiveObject");  //find objects

            if (saveObjects.Length > 0)  // if found
            {

                _hive.HiveObjects = new List<HiveObject>();  //create a new list in the data file

                foreach (var item in saveObjects)  //iterate over all found objects
                {

                    HiveObject hiveObject = new HiveObject
                    {

                        Prefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource(item), //assigning a value received from a method that will return the item's origin prefab

                        Position = item.transform.position

                    };

                    _hive.HiveObjects.Add(hiveObject);  //add element   to list

                }

                EditorUtility.SetDirty(_hive);  //mark as modified

                

            }

        }




    }
}
