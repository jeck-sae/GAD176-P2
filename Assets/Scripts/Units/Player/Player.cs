using UnityEngine;

public class Player : Unit
{
    protected override void Initialize()
    {
        PlayerInventory.Instance.SelectedItemChanged += ChangeSelectedItem;
        base.Initialize();
    }

    public void Update()
    {
        Vector3 mousePos = Helpers.Camera.ScreenToWorldPoint(Input.mousePosition);
        LookAt(mousePos);
    }


    protected void ChangeSelectedItem(Item item)
    {
        itemInHand = item;
    }

    //I don't destroy the Player script to prevent some bugs from happening
    public override void Die()
    {
        Debug.Log("DEAD");
        gameObject.SetActive(false);
    }
}
