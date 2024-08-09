using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    public int size;

    protected ItemSlot[] slots;

    protected override void Initialize()
    {
        slots = new ItemSlot[size];
        for (int i = 0; i < slots.Length; i++) 
            slots[i] = new ItemSlot();

        base.Initialize();
    }



    public override void Interact()
    {
        ItemInteractionManager.Instance.OpenItemContainer(slots, true);
    }
}
