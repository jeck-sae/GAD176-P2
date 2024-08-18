using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShots : MonoBehaviour
{
    public static Action<Vector2> OnGunshotFired;

    public static void GunshotFired(Vector2 position)
    {
        if (OnGunshotFired != null)
        {
            OnGunshotFired(position);
        }
    }
}
