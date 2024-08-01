using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class DialogueUI : Singleton<DialogueUI>
{
    public GameObject DialogueMenu;
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI dialogueText;
    public List<Button> optionButtons;// List of buttons for options

    public void DisplayDialogueMenu()
    {
        DialogueMenu.SetActive(true);
    }

    public void DisplayNode(DialogueNode node)
    {
        characterNameText.text = node.characterName;
        dialogueText.text = node.dialogueText;

        for (int i = 0; i < optionButtons.Count; i++)
        {
            if (i < node.options.Count)
            {
                optionButtons[i].gameObject.SetActive(true);
                TextMeshProUGUI buttonText = optionButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = node.options[i].optionText;
                }
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }
    }
    public void CloseDialogueMenu()
    {
        DialogueMenu.SetActive(false);
    }
}