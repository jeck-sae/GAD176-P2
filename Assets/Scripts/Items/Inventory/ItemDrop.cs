using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemDrop : Interactable
{
    [SerializeField] ItemSlot slot;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] int startAmount;

    private void Start()
    {
        if (slot.item == null)  
            Destroy(gameObject);
        
    }

    public void Initialize(ItemSlot from, int amount)
    {
        if (slot.item == null)
            Destroy(gameObject);

        InventoryUtils.MoveToSlot(from, slot, amount);
        slot.item.transform.parent = transform;
        sr.sprite = slot.item.icon;
        name = "drop: " + slot.item.ID;
        //slot.item.ShowGFX(true);
        //slot.item.gameObject.SetActive(true);
    }

    public void Initialize(Item item, int amount)
    {
        slot.LoadItems(item, amount);
        Destroy(item.gameObject);
        slot.item.transform.parent = transform;
        sr.sprite = slot.item.icon;
        name = "drop: " + slot.item.ID;
        //item.ShowGFX(true);
        //slot.item.gameObject.SetActive(true);
    }



    public override void Interact()
    {
        PickUp(PlayerInventory.Instance);
    }

    public void PickUp(IItemContainer moveTo)
    {
        InventoryUtils.MoveToContainer(slot, moveTo, slot.GetAmount());
        if (slot.GetAmount() <= 0)
            Destroy(gameObject);
    }
}
