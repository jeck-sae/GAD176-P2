using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Item : Interactable
{
    [Header("Item Info")]
    public int maxStack = 64;
    public Sprite icon;

    [SerializeField] protected Unit owner;
    [SerializeField] protected ItemSlot slot;

    //protected bool isSelected => (slot == null) ? true : slot.isSelected;
    protected bool isSelected => debugSelected;
    private bool m_usedThisFrame = false;
    private bool m_usedLastFrame = false;
    private bool m_usingSinceBeforeConsuming = false;


    #region Debug Buttons

    [ShowInInspector, ReadOnly, PropertyOrder(6)] private bool debugUsing;
    [ShowInInspector, ReadOnly, PropertyOrder(5)] private bool debugSelected = true;

    [Button("Start Using"), PropertyOrder(3), ShowIf("@!debugUsing")]
    private void StartUsing() => debugUsing = true;

    [Button("Stop Using"), PropertyOrder(4), ShowIf("debugUsing")]
    private void StopUsing() => debugUsing = false;


    [Button("Select"), PropertyOrder(1), ShowIf("@!isSelected")]
    private void DebugSelect()
    {
        debugSelected = true;
        OnSelect();
    }

    [Button("Deselect"), PropertyOrder(2), ShowIf("isSelected")]
    private void DebugDeselect()
    {
        debugSelected = false;
        OnDeselect();
    }

    #endregion

    private void Update()
    {
        ItemUpdate();

        //DEBUG 
        if(debugUsing) 
            TryUsing();
    }

    private void LateUpdate()
    {
        //logic for invoking OnStopUsing()
        if (m_usedThisFrame)
        {
            m_usedThisFrame = false;
            return;
        }

        if (m_usedLastFrame)
        {
            if (!m_usingSinceBeforeConsuming)
                OnStopUsing();
            else
                m_usingSinceBeforeConsuming = false;

            m_usedLastFrame = false;
        }
    }

    //should be handled by the unit script (will fix later)
    public void MoveTo(Unit newOwner/*ItemSlot*/) 
    {
        owner = newOwner;
        transform.parent = owner.hand;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    //temporary
    public override void Interact()
    {
        GetComponent<Collider2D>().enabled = false;
        var p = FindObjectOfType<Player>();
        p.itemInHand = this;
        MoveTo(p);
    }

    public void Drop()
    {
        owner = null;
        transform.parent = null;
        //TODO: update itemSlot
    }


    public void TryUsing()
    {
        if (m_usedThisFrame)
            return;

        if (!m_usedLastFrame)
            OnStartUsing();
        
        if(!m_usingSinceBeforeConsuming)
            OnUsing();

        m_usedLastFrame = true;
        m_usedThisFrame = true;
    }


    public void Consume()
    {
        if (slot != null)
            slot.ConsumeItem();
        else
            Destroy(gameObject);

        if (m_usedThisFrame)
            m_usingSinceBeforeConsuming = true; ;
    }


    //other scripts can override:

    //remember to call Consume() when the item should be consumed
    //(for example a healing item being used)

    /// <summary>
    /// Equivalent of Awake()
    /// </summary>
    protected override void Initialize() 
    {
        if (owner == null)
            owner = GetComponentInParent<Unit>();
        if (owner)
            owner.itemInHand = this;

        if (debugSelected)
            OnSelect();

        base.Initialize();
    }

    /// <summary>
    /// Equivalent of Update()
    /// </summary>
    protected virtual void ItemUpdate() { }

    /// <summary>
    /// Called when the item is being held 
    /// (for example when the player selects this item's slot)
    /// </summary>
    public virtual void OnSelect() { }

    /// <summary>
    /// Called when the item is no longer being held 
    /// (for example when the player selects a different item)
    /// </summary>
    public virtual void OnDeselect() { }

    /// <summary>
    /// Whether or not this item can be used from the inventory UI
    /// </summary>
    public virtual bool CanUseFromInventory() => false;

    /// <summary>
    /// Called the first frame this item is being used
    /// </summary>
    protected virtual void OnStartUsing() { }

    /// <summary>
    /// Called each frame this item is being used
    /// </summary>
    protected virtual void OnUsing() { }

    /// <summary>
    /// Called the last frame this item is being used (in the LateUpdate())
    /// </summary>
    protected virtual void OnStopUsing() { }
    
}
