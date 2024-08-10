using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue tdialogue;
    public bool Completed = false;
    public float InteractionDist = 7f;

    Transform player;
    void Awake()
    {
        player = PlayerManager.Instance.player;
    }
    public void Interact()
    {
        DialogueManager.Instance.trigger = this;
        DialogueManager.Instance.dialogue = tdialogue;
        DialogueManager.Instance.StartDialogue();
        Debug.Log("Interacting with " + transform.name);
    }

    //Check for interaction
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1))
        {
            float distance = Vector2.Distance(player.position, transform.position);
            if (distance <= InteractionDist)
            {
                Interact();
            }
        }
    }
}


