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

    protected ItemSlotCollection<ItemSlot> slots;
    protected Hotbar hotbar;

    protected List<ItemSlotUI> inventorySlots = new List<ItemSlotUI>();
    protected List<HotbarSlotUI> hotbarSlots = new List<HotbarSlotUI>();

    protected Unit owner;

    protected HotbarSlot selectedSlot => hotbar.selectedSlot;
    public Unit GetOwner() => owner;

    public void SetOwner(Unit owner)
        => this.owner = owner;
    
    private void Awake()
    {
        owner = FindObjectOfType<Player>();
        slots = new ItemSlotCollection<ItemSlot>(inventorySize, owner);
        hotbar = new Hotbar(hotbarSize, owner);
        hotbar.SelectedItemChanged += (Item item) => { SelectedItemChanged?.Invoke(item); };

        for (int i = 0; i < inventorySize; i++)
        {
            var newSlotUI = Instantiate(inventorySlotPrefab, inventorySlotsParent.transform).GetComponent<ItemSlotUI>();
            inventorySlots.Add(newSlotUI);

            newSlotUI.Initialize(slots.GetSlots()[i]);
        }
        for (int i = 0; i < hotbarSize; i++)
        {
            var newSlotUI = Instantiate(hotbarSlotPrefab, hotbarSlotsParent.transform).GetComponent<HotbarSlotUI>();
            hotbarSlots.Add(newSlotUI);

            newSlotUI.Initialize(hotbar.GetSlots()[i]);
        }

    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y == 1)
            hotbar.MoveSelection(1);
        else if (Input.mouseScrollDelta.y == -1)
            hotbar.MoveSelection(-1);
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
            hotbar.SelectHotbarSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            hotbar.SelectHotbarSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            hotbar.SelectHotbarSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            hotbar.SelectHotbarSlot(3);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            hotbar.SelectHotbarSlot(4);

    }

    public void SelectHotbarSlot(HotbarSlot slot)
    {
        hotbar.SelectHotbarSlot(slot);
    }
    public void SelectHotbarSlot(int slotIndex)
    {
        hotbar.SelectHotbarSlot(slotIndex);
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


    public int LoadItems(Item item, int amount)
    {
        amount = hotbar.LoadItems(item, amount);
        amount = slots.LoadItems(item, amount);

        return amount;
    }

    public int ContainsItem(string id)
    {
        return hotbar.ContainsItem(id) + slots.ContainsItem(id);
    }

    public int RemoveItem(string id, int amount)
    {
        amount = slots.RemoveItem(id, amount);
        amount = hotbar.RemoveItem(id, amount);
        return amount;
    }

}


public class Hotbar : ItemSlotCollection<HotbarSlot>
{

    protected int selectedSlotIndex;
    public event Action<Item> SelectedItemChanged;

    public Hotbar(int size, Unit owner = null, int defaultSlot = 0) : base(size, owner)
    {
        if(owner == null)
            SetOwner(GameObject.FindObjectOfType<Player>());
        
        selectedSlotIndex = defaultSlot;

        selectedSlot.Select();
        SelectedItemChanged?.Invoke(selectedSlot.item);
        selectedSlot.OnItemChanged += OnSelectedSlotItemChanged;
    }

    public void SelectHotbarSlot(HotbarSlot slot)
        => SelectHotbarSlot(Array.IndexOf(slots, slot));
    public void MoveSelection(int moveBy)
    {
        SelectHotbarSlot((selectedSlotIndex + slots.Length + moveBy) % slots.Length);
    }
    public void SelectHotbarSlot(int slotIndex)
    {
        if (slotIndex != selectedSlotIndex && slotIndex >= 0 && slotIndex < slots.Length)
        {
            selectedSlot?.Deselect();
            selectedSlot.OnItemChanged -= OnSelectedSlotItemChanged;

            selectedSlotIndex = slotIndex;
            selectedSlot.Select();
            SelectedItemChanged?.Invoke(selectedSlot.item);
            selectedSlot.OnItemChanged += OnSelectedSlotItemChanged;
        }
    }
    public HotbarSlot selectedSlot => slots[selectedSlotIndex];


    protected void OnSelectedSlotItemChanged()
    {
        SelectedItemChanged?.Invoke(selectedSlot.item);
    }


}