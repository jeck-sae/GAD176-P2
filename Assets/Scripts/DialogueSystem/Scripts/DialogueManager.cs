using UnityEngine;
using System.Collections.Generic;

public class DialogueManager : Singleton<DialogueManager>
{
    public Dialogue dialogue;
    public Dialogue DefaultCompletedDialogue;
    private DialogueNode currentNode;
    [HideInInspector] public DialogueTrigger trigger;
    [Header("Random spawns")]
    [HideInInspector] public bool replacement = false;
    [HideInInspector] public string replacementText;

    public void StartDialogue()
    {
        DialogueUI.Instance.DisplayDialogueMenu();
        if (trigger.Completed)
        {
            currentNode = DefaultCompletedDialogue.dialogueNodes[Random.Range(0, 4)];
        }
        else
        {
            currentNode = dialogue.dialogueNodes[0];
        }
        DisplayCurrentNode();
    }

    public void DisplayCurrentNode()
    {
        if (currentNode != null)
        {
            // Display the current dialogue node
            foreach (var option in currentNode.options)
            {
                Debug.Log(option.optionText);
            }

            // Call the UI to update
            DialogueUI.Instance.DisplayNode(currentNode);

            if(replacement)
            {
                if (currentNode.replace)
                {
                    DialogueUI.Instance.DisplayReplacment(currentNode, replacementText);
                    replacementText = null;
                }
                replacement = false;
            }
        }
    }

    public void ChooseOption(int chosenButton)
    {
        int optionIndex = chosenButton;

        if (optionIndex >= 0 && optionIndex < currentNode.options.Count)
        {
            var chosenOption = currentNode.options[optionIndex];

            if (chosenOption.additionalFunctions.SkillCheck)
            {
                int modifier = Random.Range(0, 3);

                int check = modifier + PlayerStats.Instance.Charisma.GetValue();

                if (check >= chosenOption.additionalFunctions.Charisma)
                {
                    if (chosenOption.additionalFunctions.quest != null)
                    {
                         QuestManager.Instance.InitializeQuest(chosenOption.additionalFunctions.quest);
                    }
                    if (chosenOption.additionalFunctions.finishTask)
                    {
                        dialogue.Dialoguequest.OnTaskCompleted();
                        trigger.Completed = true;
                    }
                    if (chosenOption.additionalFunctions.finishDialogue)
                    {
                        FinishDialogue();
                    }
                    else
                    {
                        currentNode = dialogue.GetDialogueNodeByID(chosenOption.nextDialogueNode);
                        DisplayCurrentNode();
                    }
                }
                else
                {
                    currentNode = dialogue.GetDialogueNodeByID(chosenOption.additionalFunctions.failDialogueNode);
                    DisplayCurrentNode();
                }
            }
            else
            {
                if (chosenOption.additionalFunctions.quest != null)
                {
                     QuestManager.Instance.InitializeQuest(chosenOption.additionalFunctions.quest);
                }
                if (chosenOption.additionalFunctions.finishTask)
                {
                    dialogue.Dialoguequest.OnTaskCompleted();
                    trigger.Completed = true;
                }
                if (chosenOption.additionalFunctions.finishDialogue)
                {
                    FinishDialogue();
                }
                else
                {
                    currentNode = dialogue.GetDialogueNodeByID(chosenOption.nextDialogueNode);
                    DisplayCurrentNode();
                }
            }
        }
        else
        {
            Debug.LogWarning("Invalid option index: " + optionIndex);
        }
    }
    public void FinishDialogue()
    {
        DialogueUI.Instance.CloseDialogueMenu();
        dialogue = null;
    }
}