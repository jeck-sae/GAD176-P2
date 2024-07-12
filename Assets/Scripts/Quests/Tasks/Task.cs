using System.Collections.Generic;
using UnityEngine;

public class Task : ScriptableObject
{
    public bool isCompleted;
    public virtual void Initialize()
    {
        isCompleted = false;
    }
    public virtual void CheckProgress()
    {
        CompleteTask();
    }
    public virtual void CompleteTask()
    {
        isCompleted = true;
    }
}