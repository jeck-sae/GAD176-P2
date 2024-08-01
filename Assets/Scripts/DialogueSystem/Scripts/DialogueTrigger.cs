using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue tdialogue;
    public float InteractionDistance = 5f; // does nothing for now
    public virtual void Interact()
    {
        DialogueManager.Instance.dialogue = tdialogue;
        DialogueManager.Instance.StartDialogue();
        Debug.Log("Interacting with " + transform.name);
    }
    //radious for editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, InteractionDistance);
    }
    //Check for interaction
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1))
        {
            Interact();
        }
    }
}
