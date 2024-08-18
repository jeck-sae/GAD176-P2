using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public ItemSlot slot;

    [SerializeField] TMP_Text amountText;
    [SerializeField] Image iconImage;

    public virtual void Initialize(ItemSlot slot)
    {
        if (slot != null)
            slot.OnItemChanged -= UpdateUI;
        
        this.slot = slot;
        slot.OnItemChanged += UpdateUI;
        UpdateUI();
    }

    private void OnDestroy()
    {
        if (slot == null) 
            return;
        slot.OnItemChanged -= UpdateUI;
    }

    protected virtual void UpdateUI()
    {
        if(slot.item == null)
        {
            amountText.enabled = false;
            iconImage.enabled = false;
            return;
        }

        amountText.enabled = true;
        amountText.text = slot.GetAmount().ToString();
        if (slot.GetAmount() <= 1)
            amountText.enabled = false;

        iconImage.enabled = true;
        iconImage.sprite = slot.item.icon;
    }

}