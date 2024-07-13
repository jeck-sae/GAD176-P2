using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody2D))]
public class Unit : Targetable
{
    public Faction faction;

    public Item itemInHand;
    public Transform hand; //where to show held items/weapons

    public AnimationCurve curveWhenNearTarget = AnimationCurve.Linear(0, 0, 2, 1);

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
