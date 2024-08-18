using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportButton : MonoBehaviour
{
    public Transform TeleportTo;

    public void Teleporter()
    {
        Teleport.Instance.TeleportMe(TeleportTo);
    }
}
