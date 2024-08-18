using System.Collections.Generic;
using UnityEngine;

// Stores all the information about the quest
[CreateAssetMenu(fileName = "New Quest", menuName = "Quest/Quest")]
public class Quest : ScriptableObject
{
    public string questName;
    [TextArea(3, 10)]
    public string questDescription;
    public bool startsWithDialoge;
    [SerializeField] protected int progress;
    public List<string> memory;
    public List<Task> tasks;

    public void TaskInitialize()
    {
        progress = 0;
        if (tasks.Count > 0)
        {
            tasks[progress].Initialize(this);
        }
    }

    public void CheckTaskProgress()
    {
        if (tasks[progress].isCompleted)
            OnTaskCompleted();
    }

    public void OnTaskCompleted()
    {
        progress++;
        Debug.Log("Progress increased " +  progress);
        if (progress < tasks.Count)
        {
            tasks[progress].Initialize(this);
        }
        else
        {
            QuestCompleted();
        }
    }
    public void QuestCompleted()
    {
        Debug.Log("Quest " + questName + " is completed!");
        // Handle quest completion (e.g., reward the player, remove the quest, etc.)
        QuestManager.Instance.RemoveQuest(this);
    }
}