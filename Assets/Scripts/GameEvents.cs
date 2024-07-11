using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static void TargetableKilled(Targetable dead, Unit attacker) 
        => OnTargetableKilled?.Invoke(dead, attacker);
    public static event Action<Targetable, Unit> OnTargetableKilled;


}
