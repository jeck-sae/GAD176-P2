using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.Events;

public class ItemSlotCollection<T> : IItemContainer where T : ItemSlot, new()
{
    protected T[] slots;
    public Unit owner;


    public ItemSlotCollection(int size, Unit owner = null)
    {
        slots = new T[size];
        for (int i = 0; i < size; i++)
        {
            var newSlot = new T();
            slots[i] = newSlot;
        }
        SetOwner(owner);
    }

    public int ContainsItem(string id)
    {
        return slots.Sum(x => x.ContainsItem(id));
    }

    public T[] GetSlots()
    {
        return slots;
    }

    public Unit GetOwner()
    {
        return owner;
    }
    public void SetOwner(Unit owner)
    {
        this.owner = owner;
        foreach (var slot in slots)
            slot.SetOwner(owner);
    }

    public int LoadItems(Item item, int amount)
    {
        foreach (var slot in slots)
        {
            if (amount <= 0)
                break;

            amount = slot.LoadItems(item, amount);
        }
        return amount;
    }

    public int RemoveItem(string id, int amount)
    {
        for (int i = slots.Length - 1; i >= 0; i--) 
        {
            if (amount <= 0)
                break;
            
            amount = slots[i].RemoveItem(id, amount);
        }
        return amount;
    }
}
