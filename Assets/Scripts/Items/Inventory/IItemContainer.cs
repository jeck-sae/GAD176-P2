using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public interface IItemContainer
{
    public int LoadItems(Item item, int amount);

    public Unit GetOwner();
}

/*public interface IItemContainerInterface : IItemContainer
{
    public void Show();
    public void Hide();
}*/
/*
public class ItemContainer : IItemContainer
{
    ItemSlot 

    public ItemContainer(int maxAmount, Unit owner = null)
    {

    }
}
*/
[Serializable]
public class ItemSlot : IItemContainer
{
    public Item item;
    protected int amount;

    public event Action OnItemChanged;

    protected Unit owner;

    public Unit GetOwner()
    {
        return owner;
    }

    public ItemSlot(Unit owner = null)
    {
        this.owner = owner;
    }

    public void ConsumeItem()
    {
        amount--;
        if (amount <= 0)
        {
            GameObject.Destroy(item.gameObject);
            item = null;
        }
        OnItemChanged?.Invoke();
    }

    public int GetAmount()
    {
        return amount;
    }

    public Item GetItem()
    {
        return item;
    }

    /// <summary>
    /// returns how many items are leftover
    /// </summary>
    public int LoadItems(Item newItem, int addAmount)
    {
        if (addAmount < 0)
        {
            Debug.LogError($"Negative add amount ({addAmount} {newItem.name})");
            return 0;
        }

        //do nothing if addAmount is zero or item is null or not compatible, return
        if (addAmount == 0 
            || newItem == null 
            || (item != null && !item.CanStackWith(newItem)))
            return addAmount;

        //if item was previously null, instantiate a new one
        if (item == null)
            InstantiateItem(newItem);

        int leftover = 0;
        if(amount + addAmount <= item.maxStack)
        {
            amount += addAmount;
        }
        else
        {
            leftover = amount + addAmount - item.maxStack;
            amount = item.maxStack;
        }

        OnItemChanged?.Invoke();
        
        return leftover;
    }

    /// <summary>
    /// returns how many items were NOT successfully removed
    /// </summary>
    public int RemoveItems(int removeAmount)
    {
        if(removeAmount < 0)
        {
            Debug.LogError($"Negative remove amount ({removeAmount} {item.name})");
            return 0;
        }

        //if it would remove all items or more
        if (amount <= removeAmount)
        {
            int leftover = removeAmount - amount;
            
            amount = 0;
            if(item) GameObject.Destroy(item.gameObject);
            item = null;
            OnItemChanged?.Invoke();
            return leftover;
        }

        amount -= removeAmount;
        OnItemChanged?.Invoke();
        return 0;
    }


    protected void InstantiateItem(Item newItem)
    {
        item = (newItem.Clone() as GameObject).GetComponent<Item>();
        item.slot = this;
        item.name = item.ID;
        item.owner = owner;

        if (owner)
        {
            item.transform.parent = owner.hand.transform;
            item.transform.position = owner.hand.transform.position;
            item.transform.rotation = owner.hand.transform.rotation;
        }

        item.gameObject.SetActive(owner != null);
        item.ShowGFX(false);
    }

}


public class HotbarSlot : ItemSlot
{
    public bool IsSelected {  get; protected set; }

    public event Action OnSelectionChanged;

    public HotbarSlot(Unit owner = null) : base(owner) { }

    public void Select()
    {
        IsSelected = true;
        OnSelectionChanged?.Invoke();
        item?.Select(true);
        OnItemChanged += OnItemUpdated;
    }

    protected void OnItemUpdated()
    {
        OnSelectionChanged?.Invoke();
        item?.Select(true);
    }

    public void Deselect()
    {
        IsSelected = false;
        OnSelectionChanged?.Invoke();
        item?.Select(false);
        OnItemChanged -= OnItemUpdated;
    }
}