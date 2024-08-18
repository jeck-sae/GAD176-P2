using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Quest/Dialogue")]
public class Dialogue : ScriptableObject
{
    [Header("Dialogue")]
    [HideInInspector]public Quest Dialoguequest;
    public List<DialogueNode> dialogueNodes;
    public DialogueNode GetDialogueNodeByID(int id)
    {return dialogueNodes.Find(node => node.dialogueNodeID == id);}
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
    public Quest quest; // starts an attached quest if there is one
    public bool finishTask; // completes the attached task if there is one
    public bool finishDialogue; // closes dialogue menu
    [Header("SkillCheck")]
    public bool SkillCheck; // makes a skill check
    public int Charisma; // charisma needed to pass the skill check
    public int failDialogueNode; // Reference to the DialogueNode by ID, for failed skill checks
}
[System.Serializable]
public class ReplaceText
{
    public bool replace = false; // it will summ up an existet text with a replacment text from spawner manager
    [TextArea(2, 8)]             // Formula for replacment: dialogueText + replacement text + dialogueText2
    public string dialogueText2;
}