using System;
using UnityEngine;

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
    public void SetOwner(Unit owner)
    {
        this.owner = owner;
    }

    public ItemSlot() { }
    public ItemSlot(Unit owner)
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

    public int ContainsItem(string id)
    {
        if (item == null || amount == 0 || item.ID != id)
            return 0;

        return amount;
    }

    /// <summary>
    /// RETURNS REMOVED ITEMS
    /// </summary>
    public int RemoveItem(string id, int amount)
    {
        if (ContainsItem(id) == 0)
            return amount;

        int remove = Mathf.Min(amount, ContainsItem(id));
        return RemoveItems(remove);
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
        if (amount + addAmount <= item.maxStack)
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

    /// <summary>
    /// returns how many items were NOT successfully removed
    /// </summary>
    public int RemoveItems(int removeAmount)
    {
        if (removeAmount < 0)
        {
            Debug.LogError($"Negative remove amount ({removeAmount} {item.name})");
            return 0;
        }

        //if it would remove all items or more
        if (amount <= removeAmount)
        {
            int leftover = removeAmount - amount;

            amount = 0;
            if (item) GameObject.Destroy(item.gameObject);
            item = null;
            OnItemChanged?.Invoke();
            return leftover;
        }

        amount -= removeAmount;
        OnItemChanged?.Invoke();
        return 0;
    }


}

[Serializable]
public class HotbarSlot : ItemSlot
{
    public bool IsSelected { get; protected set; }

    public event Action OnSelectionChanged;

    public HotbarSlot() : base() { }
    public HotbarSlot(Unit owner) : base(owner) { }

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