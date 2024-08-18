using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class HealingItem : Item
{
    public bool instant;
    public float timeToHeal;
    public float healAmount;

    private float healAtTime;


    public override bool CanUseFromInventory()
    {
        return CanUse();
    }
    protected override void OnStartUsing()
    {
        if (!CanUse())
            return;

        healAtTime = Time.time + timeToHeal;

        if (instant)
        {
            Heal();
        }
        else
        {
            //start sfx
        }
    }


    protected override void OnUsing()
    {
        if (!instant && Time.time >= healAtTime)
        {
            Heal();
            healAtTime = Time.time + timeToHeal; 
        }
    }

    protected bool CanUse()
    {
        return owner && owner.currentHealth < owner.maxHealth;
    }

    protected void Heal()
    {
        float modifier = PlayerStats.Instance.Intelligence.GetValue() / 10f;
        float finalhealAmount = healAmount * modifier + healAmount;

        owner.Heal(finalhealAmount);

        Debug.Log("Heals " + finalhealAmount);
        Consume();
    }

}
