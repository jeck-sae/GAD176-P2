using Sirenix.OdinInspector;
using UnityEngine;

public class ProjectileWeapon : Weapon
{

    [Header("Projectiles Info")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform projectileSpawnpoint;
    public float projectileSpeed = 10;
    public int projectilesPerShot = 1;
    public float projectileLifetime = 3;
    public float projectileSpeedVariation = 1;
    public float projectileDirectionVariation = 5;

    [Header("Ammo Info")]
    public float reloadSpeed = .7f;
    public int maxAmmo = 15;

    [Header("Sound Effects")] 
    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip reloadSound;
    [SerializeField] AudioClip noAmmoSound;

    [Header("Debug")]
    //these are shown in the inspector, but cannot be modified while the game is not running
    [SerializeField, DisableInEditorMode] protected int currentAmmo;
    [SerializeField, DisableInEditorMode] protected float nextShotMinTime = 0; //when can the next attack be fired
    [SerializeField, DisableInEditorMode] protected bool isReloading;
    

    protected override void Initialize()
    {
        currentAmmo = maxAmmo;
        base.Initialize();
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
        for(int i = 0; i < projectilesPerShot; i++)
        {
            var go = Instantiate(projectilePrefab, projectileSpawnpoint.position, GetProjectileDirection());
            var proj = go.GetComponent<Projectile>();
            InitializeProjectile(proj);
        }
        nextShotMinTime = Time.time + attackSpeed;
        currentAmmo--; 
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
        var variation = Random.Range(0, projectileDirectionVariation) - projectileDirectionVariation / 2;
        return Quaternion.Euler(projectileSpawnpoint.rotation.eulerAngles + Vector3.forward * variation);
    }

}
