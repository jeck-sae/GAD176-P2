using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Quest/Dialogue")]
public class Dialogue : ScriptableObject
{
    [Header("Dialogue")]
    public string DialogueID;
    public string DialogueName;
    public bool isCompleted = false;
    [HideInInspector]public Quest Dialoguequest;
    public List<DialogueNode> dialogueNodes;

    public DialogueNode GetDialogueNodeByID(int id)
    {
        return dialogueNodes.Find(node => node.dialogueNodeID == id);
    }
}

[System.Serializable]
public class DialogueNode
{
    public int dialogueNodeID;
    public string characterName;
    [TextArea(3, 10)]
    public string dialogueText;
    public List<DialogueOption> options;
}

[System.Serializable]
public class DialogueOption
{
    public int optionID; // ID for the option
    public string optionText;
    public int nextDialogueNode; // Reference to the next DialogueNode by ID
    public AdditionalFunctions additionalFunctions;
}
[System.Serializable]
public class AdditionalFunctions
{
    public bool finishTask; // Needs a way to notify the task
    public bool finishDialogue;
    [Header("SkillCheck")]
    public bool SkillCheck;
    public int Charisma;
    public int failDialogueNode; // Reference to the DialogueNode by ID, for failed skill checks
}