using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class ItemContainerInterface : MonoBehaviour, IGameInterface
{
    [SerializeField] GameObject interfaceObject;

    [SerializeField] GameObject slotPrefab;
    [SerializeField] Transform slotUIParent;

    ItemSlotCollection<ItemSlot> currentCollection;
    List<GameObject> slotsUI = new List<GameObject>();

    public void DisplaySlots(ItemSlotCollection<ItemSlot> collection)
    {
        foreach(var slotUI in slotsUI)
            Destroy(slotUI.gameObject);
        slotsUI.Clear();

        currentCollection = collection;
        foreach (var slot in collection.GetSlots())
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

}
