using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    public int size;
    public List<PresetChestSlot> startingSlots = new List<PresetChestSlot>();

    protected ItemSlotCollection<ItemSlot> slots;

    protected override void Initialize()
    {
        slots = new ItemSlotCollection<ItemSlot>(size);
        foreach(var v in startingSlots)
        {
            Item item = v.item.GetComponent<Item>();
            slots.LoadItems(item, v.amount);
        }
    }

    public override void Interact()
    {
        GameUIManager.Instance.OpenItemContainer(slots, true);
    }

    [Serializable]
    public class PresetChestSlot
    {
        public GameObject item;
        public int amount;
    }
}
