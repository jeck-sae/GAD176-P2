using UnityEngine;

public abstract class Weapon : Interactable2D 
{
    public float attackDistance = 3;
    public Unit owner;
    protected SpriteRenderer sr;

    protected override void ManagedInitialize()
    {
        if (!sr)
            sr = GetComponent<SpriteRenderer>();

        if (!owner)
            owner = GetComponentInParent<Unit>();

        if(owner)
            SetEquipped(owner);
    }

    public virtual void Reload() { }
    
    public virtual void TryAttacking() 
        => Attack();

    public virtual void Attack() { }

    protected virtual void Drop()
    {
        owner.WeaponDropped -= Drop;
        if(sr) sr.enabled = true;
        owner = null;
        transform.SetParent(null);
    }

    public virtual void SetEquipped(Unit unit)
    {
        owner = unit;
        owner.WeaponDropped += Drop;

        if(sr) sr.enabled = false;
        transform.SetParent(owner.transform);
        transform.position = owner.transform.position;
        transform.rotation = owner.transform.rotation;
    }

    protected override void OnCursorSelectStart()
    {
        //when clicked, equip to the player unit
        FindObjectOfType<Player>().PickupWeapon(this);
    }

}
