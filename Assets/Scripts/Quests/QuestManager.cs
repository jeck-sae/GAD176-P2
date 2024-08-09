using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    public List<Quest> quests;
    public GameObject JornalUI;
    public TextMeshProUGUI Description;
    public GameObject questListContainer; // Reference to the UI Content GameObject
    public GameObject questEntryPrefab; // Reference to the Quest Prefab

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            JornalUI.SetActive(!JornalUI.activeSelf);
        }
    }
    public void InitializeQuest(Quest quest)
    {
        quests.Add(quest);
        quest.TaskInitialize();
        AddQuestUI(quest);
    }
    public void CheckProgress(Quest quest)
    {
        quest.CheckTaskProgress();
    }
    public void RemoveQuest(Quest quest)
    {
        quests.Remove(quest);
        RemoveQuestUI(quest);
    }
    public void ShowDescription(string descript)
    {
        Description.text = descript;
    }
    private void AddQuestUI(Quest quest)
    {
        GameObject questObj = Instantiate(questEntryPrefab, questListContainer.transform);
        questObj.transform.Find("QuestName").GetComponent<TextMeshProUGUI>().text = quest.questName;
        questObj.GetComponent<QuestDescription>().StoredDescription = quest.questDescription;
        questObj.name = quest.questName; // Name the GameObject for easy reference
    }
    private void RemoveQuestUI(Quest quest)
    {
        foreach (Transform child in questListContainer.transform)
        {
            if (child.name == quest.questName)
            {
                Destroy(child.gameObject);
                break;
            }
        }
    }
}
