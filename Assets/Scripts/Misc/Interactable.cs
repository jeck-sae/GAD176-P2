using System;
using UnityEngine;

public class Interactable : ManagedBehaviour
{
    private Collider coll;

    private Collider2D coll2D;

    public bool CollisionEnabled
    {
        get
        {
            if (CollisionIs2D)
            {
                return GetComponent<Collider2D>().enabled;
            }
            return GetComponent<Collider>().enabled;
        }
    }

    public Action<Interactable> CursorEntered { get; set; }

    public Action<Interactable> CursorExited { get; set; }

    public Action<Interactable> CursorSelectStarted { get; set; }

    public Action<Interactable> CursorSelectEnded { get; set; }

    protected virtual bool CollisionIs2D => false;

    protected bool CursorHovering { get; private set; }

    protected virtual void Start()
    {
    }

    public void SetCollisionEnabled(bool enabled)
    {
        if (!enabled || CanEnable())
        {
            if (coll2D == null && CollisionIs2D)
            {
                coll2D = GetComponent<Collider2D>();
            }
            if (coll == null && !CollisionIs2D)
            {
                coll = GetComponent<Collider>();
            }
            if (coll != null)
            {
                coll.enabled = enabled;
            }
            if (coll2D != null)
            {
                coll2D.enabled = enabled;
            }
            if (enabled)
            {
                OnInteractionEnabled();
            }
            else
            {
                OnInteractionDisabled();
            }
        }
    }

    public void CursorSelectStart()
    {
        CursorSelectStarted?.Invoke(this);
        OnCursorSelectStart();
    }

    public void CursorSelectEnd()
    {
        CursorSelectEnded?.Invoke(this);
        OnCursorSelectEnd();
    }

    public void CursorEnter()
    {
        CursorEntered?.Invoke(this);
        OnCursorEnter();
    }

    public void CursorStay()
    {
        CursorHovering = true;
        OnCursorStay();
    }

    public void CursorExit()
    {
        CursorExited?.Invoke(this);
        CursorHovering = false;
        OnCursorExit();
    }

    public void CursorDragOff()
    {
    }

    public virtual void ClearDelegates()
    {
    }

    protected virtual bool CanEnable()
    {
        return true;
    }

    protected virtual void OnInteractionEnabled()
    {
    }

    protected virtual void OnInteractionDisabled()
    {
    }

    protected virtual void OnCursorEnter()
    {
    }

    protected virtual void OnCursorStay()
    {
    }

    protected virtual void OnCursorExit()
    {
    }

    protected virtual void OnCursorSelectStart()
    {
    }

    protected virtual void OnCursorSelectEnd()
    {
    }

    protected virtual void OnCursorDrag()
    {
    }
}
