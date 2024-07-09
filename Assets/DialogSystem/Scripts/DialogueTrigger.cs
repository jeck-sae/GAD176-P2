using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue tdialogue;
    // A way to trigger dialogue
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            DialogueManager.DialogueInstance.dialogue = tdialogue;
            DialogueManager.DialogueInstance.StartDialogue();
        }
    }
}
