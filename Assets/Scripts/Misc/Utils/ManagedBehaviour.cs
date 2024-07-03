using UnityEngine;


//allows me to decide when to call the update method of any script (for example to pause the game)
public class ManagedBehaviour : ManagedBehaviourBase
{
    private bool initialized;

    public virtual bool UpdateWhenPaused => false;

    public void Initialize()
    {
        if (!initialized)
        {
            initialized = true;
            ManagedInitialize();
        }
    }

    public virtual void ManagedUpdate()
    {
    }

    public virtual void ManagedFixedUpdate()
    {
    }

    public virtual void ManagedLateUpdate()
    {
    }

    protected virtual void ManagedInitialize()
    {
    }

    public sealed override void Update()
    {
        if (CanUpdate())
        {
            ManagedUpdate();
        }
    }

    public sealed override void FixedUpdate()
    {
        if (CanUpdate())
        {
            ManagedFixedUpdate();
        }
    }

    public sealed override void LateUpdate()
    {
        if (CanUpdate())
        {
            ManagedLateUpdate();
        }
    }

    protected sealed override void Awake()
    {
        if (!initialized)
        {
            initialized = true;
            ManagedInitialize();
        }
    }

    private bool CanUpdate()
    {
        if (!UpdateWhenPaused)
        {
            //logic for pausing
        }
        return true;
    }
}


public class ManagedBehaviourBase : MonoBehaviour
{
    public virtual void Update()
    {
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void LateUpdate()
    {
    }

    protected virtual void Awake()
    {
    }
}