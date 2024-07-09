using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    List<ItemSlot> slots = new List<ItemSlot>();

    
}

public class ItemSlot
{
    public Item item;
    public int amount;
    public bool isSelected;

    public void ConsumeItem(int consumeAmount = 1)
    {
        if (amount < consumeAmount)
            Debug.LogWarning($"Consumed {consumeAmount} of {amount} owned items ({item.name})");
        amount -= consumeAmount;
        
        if(amount <= 0)
            GameObject.Destroy(item.gameObject);
    }

    public void Select()
    {
        isSelected = true;
        item.OnSelect();
    }

    public void Deselect() 
    {
        isSelected = false;
        item.OnDeselect();
    }
}
