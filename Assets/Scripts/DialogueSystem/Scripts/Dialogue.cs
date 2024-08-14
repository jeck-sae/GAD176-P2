using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Quest/Dialogue")]
public class Dialogue : ScriptableObject
{
    [Header("Dialogue")]
    public string DialogueName;
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
    [TextArea(3, 10)]
    public string dialogueText;
    public ReplaceText replaceText;
    public List<DialogueOption> options;
}

[System.Serializable]
public class DialogueOption
{
    [TextArea(2, 10)]
    public string optionText;
    public int nextDialogueNode; // Reference to the next DialogueNode by ID
    public AdditionalFunctions additionalFunctions;
}
[System.Serializable]
public class AdditionalFunctions
{
    public Quest quest;
    public bool finishTask; // Needs a way to notify the task
    public bool finishDialogue;
    [Header("SkillCheck")]
    public bool SkillCheck;
    public int Charisma;
    public int failDialogueNode; // Reference to the DialogueNode by ID, for failed skill checks
}
[System.Serializable]
public class ReplaceText
{
    public bool replace = false;
    [TextArea(2, 8)]
    public string dialogueText2;
}