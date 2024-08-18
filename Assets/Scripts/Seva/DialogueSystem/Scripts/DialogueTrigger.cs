using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue tdialogue;
    public Quest dialogueQuest;
    public bool Completed = false;
    public float InteractionDist = 7f;
    public GameObject icon;
    protected bool canInteract;

    Transform player;
    void Awake()
    {
        player = PlayerManager.Instance.player;
        icon.SetActive(false);
        canInteract = false;
    }
    public void Interact()
    {
        DialogueManager.Instance.trigger = this;
        DialogueManager.Instance.dialogue = tdialogue;
        DialogueManager.Instance.StartDialogue();
        Debug.Log("Interacting with " + transform.name);
    }
    private void Update()
    {
        float distance = Vector2.Distance(player.position, transform.position);
        if (distance <= InteractionDist)
        {
            icon.SetActive(true);
            canInteract = true;
        }
        else
        {
            icon.SetActive(false);
            canInteract = false;
        }
    }

    //Check for interaction
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1))
        {
            if (canInteract)
            {
                Interact();
            }
        }
    }
}


