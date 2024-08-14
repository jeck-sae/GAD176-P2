using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class ProjectileWeapon : Weapon
{

    [Header("Projectiles Info")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectileSpawnpoint;
    public float projectileSpeed = 50;
    public int projectilesPerShot = 1;
    public float projectileLifetime = 3;
    public float projectileSpeedVariation = 1;
    public float projectileDirectionVariation = 5;

    [Header("Ammo Info")]
    public float reloadSpeed = .7f;
    public int maxAmmo = 15;

    [Header("Sound Effects")]
    public bool RifleSound = true;
    public bool PistolSound = false;
    public bool ShotGunSound = false;

    [Header("Visual Effects")]
    public ParticleSystem GunSmoke;
    public ParticleSystem MuzleFlash;
    public Light2D muzzleFlashLight;
    public float flashDuration = 0.05f;

    [Header("Debug")]
    //these are shown in the inspector, but cannot be modified while the game is not running
    [SerializeField, DisableInEditorMode] protected int currentAmmo;
    [SerializeField, DisableInEditorMode] protected float nextShotMinTime = 0; //when can the next attack be fired
    [SerializeField, DisableInEditorMode] protected bool isReloading;
    protected void Awake()
    {
        currentAmmo = maxAmmo;
        muzzleFlashLight.enabled = false;
    }

    public override void Reload()
    {
        if (currentAmmo == maxAmmo)
            return;

        //TODO: add animation/sound
        isReloading = true;
        CustomCoroutine.WaitThenExecute(reloadSpeed, DoReload);
    }

    //end of the reload animation
    protected void DoReload()
    {
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    public override void TryAttacking()
    {
        //attack if possible.
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            Reload();
            return;
        }

        if (nextShotMinTime > Time.time)
            return;

        Attack();
    }
    

    public override void Attack()
    {
        if (RifleSound)
            AudioManager.PlaySoundAtPoint(SoundType.RifleShot, projectileSpawnpoint.position, 0.9f);
        if (ShotGunSound)
            AudioManager.PlaySoundAtPoint(SoundType.ShotGunShot, projectileSpawnpoint.position, 0.8f);
        if (PistolSound)
            AudioManager.PlaySoundAtPoint(SoundType.PistolShot, projectileSpawnpoint.position, 0.8f);
        for (int i = 0; i < projectilesPerShot; i++)
        {
            var go = Instantiate(projectilePrefab, projectileSpawnpoint.position, GetProjectileDirection());
            var proj = go.GetComponent<Projectile>();
            InitializeProjectile(proj);
        }
        if (GunSmoke != null)
            GunSmoke.Play();
        if (MuzleFlash != null)
            MuzleFlash.Play();
        if (owner is Player)
        CameraShake.Instance.CamShake();
        // Trigger the flash
        StartCoroutine(Flash());
        nextShotMinTime = Time.time + attackSpeed;
        currentAmmo--;
        GunShots.GunshotFired(transform.position);
    }

    //inheriting classes can override this to make it easier to have different types of projectiles
    protected virtual void InitializeProjectile(Projectile projectile)
    {
        projectile.Initialize(owner, damage, GetProjectileSpeed(), projectileLifetime);       
    }

    protected float GetProjectileSpeed()
    {
        return projectileSpeed + Random.Range(0, projectileSpeedVariation) - projectileSpeedVariation / 2;
    }

    protected Quaternion GetProjectileDirection()
    {
        float dexterity = 0;
        if (owner is Player)
            dexterity = PlayerStats.Instance.Dexterity.GetValue();

        var variation = Random.Range(0, Mathf.Clamp(projectileDirectionVariation - dexterity, 0, float.MaxValue)) - projectileDirectionVariation / 2;
        return Quaternion.Euler(projectileSpawnpoint.rotation.eulerAngles + Vector3.forward * variation);
    }
    private IEnumerator Flash()
    {
        muzzleFlashLight.enabled = true;

        // Wait for the flash duration
        yield return new WaitForSeconds(flashDuration);

        muzzleFlashLight.enabled = false;
    }
}
