using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvent : ScriptableObject
{
    [HideInInspector] public RandomEventTrigger triger;
    public virtual void Initialize()
    {
        
    }
}
