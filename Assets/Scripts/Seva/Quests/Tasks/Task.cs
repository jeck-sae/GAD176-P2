using System.Collections.Generic;
using UnityEngine;

public class Task : ScriptableObject
{
    public bool isCompleted;
    protected Quest taskQuest;
    public virtual void Initialize(Quest quest)
    {
        isCompleted = false;
        taskQuest = quest;
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