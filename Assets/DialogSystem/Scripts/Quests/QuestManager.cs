using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    #region Singleton

    public static QuestManager QuestInstance;

    private void Awake()
    {
        if (QuestInstance != null)
        {
            Debug.LogWarning("More than one instance");
            return;
        }
        QuestInstance = this;
    }
    #endregion
    public List<Quest> quests;
    public GameObject JornalUI;
    public GameObject questListContainer; // Reference to the UI Content GameObject
    public GameObject questEntryPrefab; // Reference to the Quest Entry Prefab

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
    private void AddQuestUI(Quest quest)
    {
        GameObject questObj = Instantiate(questEntryPrefab, questListContainer.transform);
        questObj.transform.Find("QuestName").GetComponent<TextMeshProUGUI>().text = quest.questName;
        //questObj.transform.Find("QuestDescription").GetComponent<TextMeshProUGUI>().text = quest.questDescription;
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
