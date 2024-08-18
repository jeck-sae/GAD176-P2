using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MeleeWeapon : Weapon
{

    [Header("Weapon Info")]
    public float projectileSpeedVariation = 1;
    public float projectileDirectionVariation = 5;

    [Header("Sound Effects")]
    public bool sharp = false;
    public bool blunt = false;

    [Header("Visual Effects")]
    public ParticleSystem swing;

    protected float nextAttackMinTime = 0; //when can the next attack be
    protected override void Initialize()
    {
        base.Initialize();
    }

    public override void TryAttacking()
    {
        if (nextAttackMinTime > Time.time)
            return;

        Attack();
    }


    public override void Attack()
    {
        if (sharp)
            AudioManager.PlaySoundAtPoint(SoundType.RifleShot, gameObject.transform.position, 0.9f);
        if (blunt)
            AudioManager.PlaySound(SoundType.ShotGunShot, 1f);


        nextAttackMinTime = Time.time + attackSpeed;

    }
}