using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTrigger : MonoBehaviour// a very bad script version2
{
    public GameObject activateObject;
    public GameObject deactivateObject;
    public GameObject deactivateObject2;
    public float InteractionDist = 7f;
    public GameObject icon;
    protected bool canInteract;

    Transform player;
    void Awake()
    {
        player = PlayerManager.Instance.player;
        if (icon != null)
        icon.SetActive(false);
        canInteract = false;
    }
    public void Interact()
    {
        if (activateObject != null)
        {
            activateObject.SetActive(true);
        }
        if (deactivateObject != null)
        {
            deactivateObject.SetActive(false);
        }
        if (deactivateObject2 != null)
        {
            deactivateObject2.SetActive(false);
        }
    }
    private void Update()
    {
        float distance = Vector2.Distance(player.position, transform.position);
        if (distance <= InteractionDist)
        {
            if (icon != null)
                icon.SetActive(true);
            canInteract = true;
        }
        else
        {
            if (icon != null)
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
