using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameUIManager : Singleton<GameUIManager>
{
    [SerializeField] GameObject interfaceBackground;

    [SerializeField] ItemSlot dragItemSlot;
    [SerializeField] ItemSlotUI dragItemSlotUI;

    [SerializeField] ItemContainerInterface itemContainerInterface;


    public bool interfaceOpen { get; protected set; }

    protected IGameInterface currentlyOpenInterface;

    private void Update()
    {
        CheckInput();
        dragItemSlotUI.transform.position = Input.mousePosition;
    }

    private void Awake()
    {
        dragItemSlot = new ItemSlot();
        dragItemSlotUI.Initialize(dragItemSlot);
    }





    void CheckInput()
    {
        if (interfaceOpen)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
                CloseInterface();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
                OpenInventory();
        }



        bool q = Input.GetKeyDown(KeyCode.Q);
        bool rightClick = Input.GetMouseButton(1);
        bool leftClickDown = Input.GetMouseButtonDown(0);

        if (rightClick || leftClickDown || q)
        {
            //Raycast UI
            PointerEventData _eventDataCurrentPosition;
            List<RaycastResult> _raycastResults;
            _eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
            _raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(_eventDataCurrentPosition, _raycastResults);

            foreach (var hit in _raycastResults)
            {
                //check layer
                if ((GameManager.InventoryLayer & (1 << hit.gameObject.layer)) == 0)
                    continue;

                var slot = hit.gameObject.GetComponentInParent<ItemSlotUI>();
                if (!slot) continue;

                if (q)
                    DropItem(slot.slot);
                if (leftClickDown)
                    LeftClickedSlot(slot.slot);
                if (rightClick)
                    RightClickedSlot(slot.slot);
                break;
            }
        }
    }


    void DropItem(ItemSlot slot)
    {
        bool ctrl = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

        if (ctrl)
            InventoryUtils.DropItem(PlayerManager.Instance.player.position, slot, slot.GetAmount());
        else    
            InventoryUtils.DropItem(PlayerManager.Instance.player.position, slot, 1);
    }

    void RightClickedSlot(ItemSlot slot)
    {
        slot.item?.TryUsing();
    }


    void LeftClickedSlot(ItemSlot slot)
    {
        if (!interfaceOpen)
        {
            if(slot is HotbarSlot)
                PlayerInventory.Instance.SelectHotbarSlot(slot as HotbarSlot);
            
            return;
        }


        bool shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool ctrl = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

        int amount = 0;

        if (shift)
        {
            //TODO: move directly to/from inventory and open container
        }
        else
        {
            if (dragItemSlot.item == null)
            {
                if(ctrl) 
                    amount = (slot.GetAmount() % 2 == 1)
                            ? slot.GetAmount() / 2 + 1
                            : slot.GetAmount() / 2;
                else 
                    amount = slot.GetAmount();

                InventoryUtils.MoveToSlot(slot, dragItemSlot, amount, true);
            }
            else
            {
                if (ctrl) amount = 1;
                else amount = dragItemSlot.GetAmount();

                InventoryUtils.MoveToSlot(dragItemSlot, slot, amount, true);
            }
        }
        
    }





    public void OpenInventory()
    {
        OpenInterface(PlayerInventory.Instance, true);
    }
    public void OpenItemContainer(ItemSlotCollection<ItemSlot> slots, bool showInventory = true)
    {
        itemContainerInterface.DisplaySlots(slots);
        OpenInterface(itemContainerInterface, showInventory);
    }


    public void OpenInterface(IGameInterface gameInterface, bool showInventory) 
    {
        if (currentlyOpenInterface != null)
            currentlyOpenInterface.Hide();
        
        currentlyOpenInterface = gameInterface;
        currentlyOpenInterface.Show();

        if(showInventory)
            PlayerInventory.Instance?.Show();
        
        interfaceBackground?.SetActive(true);
        interfaceOpen = true;
    }

    public void CloseInterface()
    {
        if (dragItemSlot.item != null)
        {
            int leftoverDragItem = PlayerInventory.Instance.LoadItems(dragItemSlot.item, dragItemSlot.GetAmount());
            dragItemSlot.RemoveItems(dragItemSlot.GetAmount() - leftoverDragItem); 
            //TODO: Drop leftover instead of keeping in drag slot
        }        


        currentlyOpenInterface?.Hide();
        PlayerInventory.Instance?.Hide();
        interfaceBackground?.SetActive(false);
        currentlyOpenInterface = null;
        interfaceOpen = false;
    }

}
