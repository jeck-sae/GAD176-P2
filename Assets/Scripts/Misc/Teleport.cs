using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Singleton<Teleport>
{
    public GameObject TeleportUI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TeleportUI.SetActive(!TeleportUI.activeSelf);
        }
    }
    public void TeleportMe(Transform tr)
    {
        gameObject.transform.position = tr.position;
    }
}
