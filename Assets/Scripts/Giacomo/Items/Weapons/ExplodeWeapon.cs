using UnityEngine;

public class ExplodeWeapon : Weapon
{
    [SerializeField] GameObject explosionEffectPrefab;

    public float explosionRange = 4;

    public override void Attack()
    {
        //get all the targetable scripts nearby and damage them
        var hit = Physics2D.CircleCastAll(transform.position, explosionRange, Vector2.zero);
        foreach (var item in hit) 
        { 
            var targetable = item.transform.GetComponent<Targetable>();
            
            if(targetable != null && targetable != owner)
            {
                targetable.Damage(damage, owner);
            }
        }

        //TODO: screenshake

        //spawn the explosion effect
        if(explosionEffectPrefab)
            Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        
        //kill the owner unit
        owner.Die();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
