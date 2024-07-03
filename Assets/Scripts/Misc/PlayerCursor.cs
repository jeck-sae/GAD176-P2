using System;
using System.Collections.Generic;
using UnityEngine;


// From The Hex
public class PlayerCursor : ManagedBehaviour
{
    public static PlayerCursor instance;

    private Interactable currentInteractable;

    private Interactable cursorDownInteractable;

    public Action<Interactable> CurrentInteractableEntered;

    public Action<Interactable> CurrentInteractableExited;


    [SerializeField]
    private Camera rayCamera;

    private List<string> excludedLayers = new List<string> { "NonInteractable" };

    public Interactable CurrentInteractable => currentInteractable;

    protected override void ManagedInitialize()
    {
        rayCamera = Helpers.Camera;
        instance = this;
    }

    public override void ManagedUpdate()
    {
        base.ManagedUpdate();
        UpdatePosition();
        UpdateMainInput();
        UpdateDragInput();
    }

    public void UpdatePosition()
    {
        Vector2 vector = rayCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(vector.x, vector.y, rayCamera.nearClipPlane);
    }

    private void UpdateMainInput()
    {
        currentInteractable = UpdateCurrentInteractable(currentInteractable, excludedLayers.ToArray());

        if (currentInteractable != null)
        {
            currentInteractable.CursorStay();
        }

        if (currentInteractable != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentInteractable.CursorSelectStart();
                cursorDownInteractable = currentInteractable;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                currentInteractable.CursorSelectEnd();
            }
        }
    }

    private void UpdateDragInput()
    {
        if (cursorDownInteractable != null && currentInteractable != cursorDownInteractable)
        {
            cursorDownInteractable.CursorDragOff();
            cursorDownInteractable = null;
        }
        if (Input.GetMouseButtonUp(0))
        {
            cursorDownInteractable = null;
        }
    }

    private Interactable UpdateCurrentInteractable(Interactable current, string[] excludeLayers)
    {
        Interactable interactable = RaycastForInteractable(~LayerMask.GetMask(excludeLayers), base.transform.position);
        if (interactable != current)
        {
            if (current != null && current.CollisionEnabled)
            {
                CurrentInteractableExited?.Invoke(current);
                current.CursorExit();
            }
            if (interactable == null)
            {
                return null;
            }
            CurrentInteractableEntered?.Invoke(interactable);
            interactable.CursorEnter();
        }
        return interactable;
    }

    private Interactable RaycastForInteractable(int layerMask, Vector3 cursorPosition)
    {
        Interactable result = null;
        RaycastHit2D[] array = Physics2D.RaycastAll(cursorPosition, Vector2.zero, 1000f, layerMask);
        if (array.Length != 0)
        {
            List<Interactable2D> interactablesFromRayHits = GetInteractablesFromRayHits(array);
            if (interactablesFromRayHits.Count > 0)
            {
                interactablesFromRayHits.Sort((Interactable2D a, Interactable2D b) => a.CompareInteractionSortOrder(b));
                result = interactablesFromRayHits[0];
            }
        }
        return result;
    }

    private List<Interactable2D> GetInteractablesFromRayHits(RaycastHit2D[] rayHits)
    {
        List<Interactable2D> list = new List<Interactable2D>();
        for (int i = 0; i < rayHits.Length; i++)
        {
            Interactable2D component = rayHits[i].transform.GetComponent<Interactable2D>();
            if (component != null)
            {
                list.Add(component);
            }
        }
        return list;
    }
}
