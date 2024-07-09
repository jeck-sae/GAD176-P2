using UnityEngine;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    #region Singleton

    public static DialogueManager DialogueInstance;

    private void Awake()
    {
        if (DialogueInstance != null)
        {
            Debug.LogWarning("More than one instance");
            return;
        }
        DialogueInstance = this;
    }
    #endregion
    public Dialogue dialogue;
    private DialogueNode currentNode;

    public void StartDialogue()
    {
        DialogueUI.DialogueUIInstance.DisplayDialogueMenu();
        if (dialogue.isCompleted)
        {
            currentNode = dialogue.dialogueNodes[100];
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
            Debug.Log(currentNode.characterName + ": " + currentNode.dialogueText);
            foreach (var option in currentNode.options)
            {
                Debug.Log(option.optionText);
            }

            // Call the UI to update
            DialogueUI.DialogueUIInstance.DisplayNode(currentNode);
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
                    if (chosenOption.additionalFunctions.finishTask)
                    {
                        dialogue.Dialoguequest.OnTaskCompleted();
                        dialogue.isCompleted = true;
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
                if (chosenOption.additionalFunctions.finishTask)
                {
                    dialogue.Dialoguequest.OnTaskCompleted();
                    dialogue.isCompleted = true;
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
        DialogueUI.DialogueUIInstance.CloseDialogueMenu();
    }
}