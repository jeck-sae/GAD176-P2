using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody2D))]
public class Unit : Targetable
{
    public Faction faction;

    public Item itemInHand;
    public Transform hand; //where to show held items/weapons

    internal Weapon weapon => itemInHand as Weapon;
    protected Rigidbody2D m_rigidbody;

    protected override void Initialize()
    {
        if (!hand)
            hand = transform;

        m_rigidbody = GetComponent<Rigidbody2D>();

        if(!itemInHand)
            itemInHand = GetComponentInChildren<Weapon>();

        base.Initialize();
    }


    public void MoveBy(Vector2 movement)
    {
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
        m_rigidbody.MovePosition(currentPos + movement);
    }

    public virtual void TryAttacking()
    {
        if(itemInHand is Weapon)
            (itemInHand as Weapon).TryAttacking();
        //else basic attack
    }

    //rotate towards a target
    public void LookAt(Vector2 target)
    {
        Vector2 lookDirection = target - (Vector2)transform.position;
        transform.up = lookDirection;
    }
}
