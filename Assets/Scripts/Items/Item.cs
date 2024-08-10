using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Item : MonoBehaviour, ICloneable
{
    [Header("Item Info")]
    public string ID;
    public int maxStack = 64;
    public Sprite icon;

    [SerializeField] GameObject itemGFX = null;

    public Unit owner;
    public ItemSlot slot;

    protected bool isSelected
         /*=> ((slot as HotbarSlot) == null) ? false
            : (slot as HotbarSlot).IsSelected*/;

    private bool m_usedThisFrame = false;
    private bool m_usedLastFrame = false;
    private bool m_usingSinceBeforeConsuming = false;


    private void Update()
    {
        ItemUpdate();
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

    private void Start()
    {
        if(owner == null && slot.item == null)
        {
            InventoryUtils.CreateDrop(transform.position, this, 1);
        } 
    }

    private void Awake()
    {
        if (itemGFX == null)
        {
            itemGFX = new GameObject();
            itemGFX.transform.parent = transform;
            itemGFX.SetActive(false);
            itemGFX.name = "GFX";
        }
        Initialize();
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

    public void Select(bool setSelected)
    {
        if (setSelected && !isSelected)
        {
            isSelected = true;
            ShowGFX(true);
            OnSelect();
        }
        else if(!setSelected && isSelected)
        {
            isSelected = false;
            ShowGFX(false);
            OnDeselect();
        }
    }

    public void ShowGFX(bool show)
    {
        itemGFX?.SetActive(show);
    }

    public bool CanStackWith(Item item)
    {
        return ID == item.ID;
    }

    public virtual object Clone()
    {
        return Instantiate(gameObject);
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
    /// Equivalent of Awake(). Before any logic, call base.Initialize()
    /// </summary>
    protected virtual void Initialize() 
    {
    }

    /// <summary>
    /// Equivalent of Update()
    /// </summary>
    protected virtual void ItemUpdate() { }

    /// <summary>
    /// Called when the item is being held 
    /// (for example when the player selects this item's slot)
    /// </summary>
    protected virtual void OnSelect() { }

    /// <summary>
    /// Called when the item is no longer being held 
    /// (for example when the player selects a different item)
    /// </summary>
    protected virtual void OnDeselect() { }

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
