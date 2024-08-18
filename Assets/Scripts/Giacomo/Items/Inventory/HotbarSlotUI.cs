using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarSlotUI : ItemSlotUI
{
    [SerializeField] GameObject selectionObject;

    protected HotbarSlot hotbarSlot => slot as HotbarSlot;

    public override void Initialize(ItemSlot slot)
    {
        if (!(slot is HotbarSlot))
        {
            Debug.LogWarning($"[{name}] Non hotbar slot provided to UI hotbar slot", gameObject);
            return;
        }

        HotbarSlot s = slot as HotbarSlot;
        s.OnSelectionChanged += UpdateUI;

        base.Initialize(slot);
    }


    protected override void UpdateUI()
    {
        selectionObject.SetActive(hotbarSlot.IsSelected);
        base.UpdateUI();
    }
}