using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class PlayerInventory : Singleton<PlayerInventory>, IGameInterface, IItemContainer
{
    public int inventorySize;
    public int hotbarSize;

    [SerializeField] GameObject inventoryObject;

    public GameObject inventorySlotPrefab;
    public GameObject hotbarSlotPrefab;

    public event Action<Item> SelectedItemChanged;

    [SerializeField] protected GameObject inventorySlotsParent;
    [SerializeField] protected GameObject hotbarSlotsParent;

    protected ItemSlot[] slots;
    protected HotbarSlot[] hotbar;

    protected List<ItemSlotUI> inventorySlots = new List<ItemSlotUI>();
    protected List<HotbarSlotUI> hotbarSlots = new List<HotbarSlotUI>();

    protected int selectedSlotIndex;
    protected Unit player;

    protected HotbarSlot selectedSlot => hotbar[selectedSlotIndex];
    public Unit GetOwner() => player;


    private void Awake()
    {
        player = FindObjectOfType<Player>();
        slots = new ItemSlot[inventorySize];
        hotbar = new HotbarSlot[hotbarSize];

        for (int i = 0; i < inventorySize; i++)
        {
            var newSlot = new ItemSlot(player);
            slots[i] = newSlot;
              
            var newSlotUI = Instantiate(inventorySlotPrefab, inventorySlotsParent.transform).GetComponent<ItemSlotUI>();
            inventorySlots.Add(newSlotUI);

            newSlotUI.Initialize(newSlot);
        }
        for (int i = 0; i < hotbarSize; i++)
        {
            var newSlot = new HotbarSlot(player);
            hotbar[i] = newSlot;

            var newSlotUI = Instantiate(hotbarSlotPrefab, hotbarSlotsParent.transform).GetComponent<HotbarSlotUI>();
            hotbarSlots.Add(newSlotUI);

            newSlotUI.Initialize(newSlot);
        }


        selectedSlotIndex = 0;
        selectedSlot.Select();
        SelectedItemChanged?.Invoke(selectedSlot.item);
        selectedSlot.OnItemChanged += OnSelectedSlotItemChanged;
    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y == 1)
            SelectHotbarSlot((selectedSlotIndex + hotbar.Length + 1) % hotbar.Length);
        else if (Input.mouseScrollDelta.y == -1)
            SelectHotbarSlot((selectedSlotIndex + hotbar.Length - 1) % hotbar.Length);
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectHotbarSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectHotbarSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SelectHotbarSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SelectHotbarSlot(3);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            SelectHotbarSlot(4);

    }


    public void Show()
    {
        inventoryObject.SetActive(true);
    }

    public void Hide()
    {
        inventoryObject.SetActive(false);
    }

    public int PickUpItem(Item item, int amount, bool destroy = true)
    {
        var result = LoadItems(item, amount);
        if (destroy)
            Destroy(item.gameObject); // TEMPORARY
        return result;
    }


    public void SelectHotbarSlot(HotbarSlot slot)
        => SelectHotbarSlot(Array.IndexOf(hotbar, slot));
    public void SelectHotbarSlot(int slotIndex)
    {
        if (slotIndex != selectedSlotIndex && slotIndex >= 0 && slotIndex < hotbar.Length)
        {
            selectedSlot?.Deselect();
            selectedSlot.OnItemChanged -= OnSelectedSlotItemChanged;
            
            selectedSlotIndex = slotIndex;
            selectedSlot.Select();
            SelectedItemChanged?.Invoke(selectedSlot.item);
            selectedSlot.OnItemChanged += OnSelectedSlotItemChanged;
        }
    }
    protected void OnSelectedSlotItemChanged()
    {
        SelectedItemChanged?.Invoke(selectedSlot.item);
    }


    public int LoadItems(Item item, int amount)
    {
        amount = InventoryUtils.TryLoadItems(hotbar, item, amount);
        amount = InventoryUtils.TryLoadItems(slots, item, amount);

        return amount;
    }


}
