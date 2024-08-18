using UnityEngine;

public abstract class Weapon : Item 
{
    [Header("Weapon Info")]
    public float attackDistance = 3;
    public float attackSpeed = .5f;
    public float damage = 5;

    
    public virtual void TryAttacking()
    {
        if(isSelected)
            Attack();
    }

    public virtual void Attack() { }

    public virtual void Reload() { }

}
