using System.Collections.Generic;
using UnityEngine;

// Stores all the information about the quest
[CreateAssetMenu(fileName = "New Quest", menuName = "Quest/Quest")]
public class Quest : ScriptableObject
{
    public string questName;
    [TextArea(3, 10)]
    public string questDescription;
    private int progress;
    public List<string> memory;
    public List<Task> tasks;

    public void TaskInitialize()
    {
        progress = 0;
        if (tasks.Count > 0)
        {
            tasks[progress].Initialize();
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
        if (progress < tasks.Count)
        {
            tasks[progress].Initialize();
        }
        else
        {
            Debug.Log("Quest" + questName + " is completed!");
            // Handle quest completion (e.g., reward the player, remove the quest, etc.)
            QuestManager.QuestInstance.RemoveQuest(this);
        }
    }
}