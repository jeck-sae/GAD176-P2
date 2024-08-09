using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class ItemContainerInterface : MonoBehaviour, IGameInterface, IItemContainer
{
    [SerializeField] GameObject interfaceObject;

    [SerializeField] GameObject slotPrefab;
    [SerializeField] Transform slotUIParent;

    List<GameObject> slotsUI = new List<GameObject>();

    ItemSlot[] currentSlots;

    public void DisplaySlots(ItemSlot[] slots)
    {
        foreach(var slotUI in slotsUI)
            Destroy(slotUI.gameObject);
        slotsUI.Clear();

        currentSlots = slots;
        foreach (var slot in slots)
        {
            var newSlotUI = Instantiate(slotPrefab, slotUIParent);
            newSlotUI.GetComponent<ItemSlotUI>().Initialize(slot);
            slotsUI.Add(newSlotUI);
        }
    }

    public void Show()
    {
        interfaceObject.SetActive(true);
    }

    public void Hide()
    {
        interfaceObject.SetActive(false);
    }

    public Unit GetOwner()
    {
        return null;
    }

    public int LoadItems(Item item, int amount)
    {
        return InventoryUtils.TryLoadItems(currentSlots, item, amount);
    }
}
