using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InventoryUtils
{

    public static int TryLoadItems(ItemSlot[] slots, Item item, int amount) 
        => TryLoadItems<ItemSlot>(slots, item, amount);
    public static int TryLoadItems<T>(T[] slots, Item item, int amount) where T : ItemSlot
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (amount <= 0)
                break;

            amount = slots[i].LoadItems(item, amount);
        }
        return amount;
    }

    public static ItemSlot FindItem(ItemSlot[] slots, string itemID)
    {
        foreach (ItemSlot slot in slots)
            if (slot.GetItem() && slot.GetItem().ID.Equals(itemID))
                return slot;
        
        return null;
    }


    public static void MoveToSlot(ItemSlot from, ItemSlot to, int amount, bool swapIfIncompatible = false)
    {
        int clampedAmount = Mathf.Clamp(amount, 0, from.GetAmount());

        if (from.item == null || to.item == null || from.item.CanStackWith(to.item))
        {
            int transferLeftover = to.LoadItems(from.GetItem(), clampedAmount);
            from.RemoveItems(clampedAmount - transferLeftover);
        }
        else if (swapIfIncompatible)
        {
            SwapSlots(from, to);
        }
    }

    public static void SwapSlots(ItemSlot slot1, ItemSlot slot2)
    {
        ItemSlot tempSlot = new ItemSlot();
        tempSlot.LoadItems(slot1.item, slot1.GetAmount());

        slot1.RemoveItems(slot1.GetAmount());
        slot1.LoadItems(slot2.item, slot2.GetAmount());
        
        slot2.RemoveItems(slot2.GetAmount());
        slot2.LoadItems(tempSlot.item, tempSlot.GetAmount());
    }
}
