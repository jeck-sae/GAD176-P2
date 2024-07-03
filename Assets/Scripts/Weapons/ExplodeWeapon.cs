using UnityEngine;

public class ExplodeWeapon : Weapon
{
    [SerializeField] GameObject explosionEffectPrefab;

    public float explosionRange;
    public float damage;

    public override void Attack()
    {
        //get all the targetable scripts nearby and damage them
        var hit = Physics2D.CircleCastAll(transform.position, explosionRange, Vector2.zero);
        foreach (var item in hit) 
        { 
            var targetable = item.transform.GetComponent<Targetable>();
            
            if(targetable != null && targetable != owner)
            {
                targetable.Damage(damage);
                Debug.Log(targetable.name + " " + damage);
            }
        }

        //TODO: if close to player, screenshake

        //spawn the explosion effect
        var explosionEffect = Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        
        //kill the owner unit
        owner.Die();
    }
}
