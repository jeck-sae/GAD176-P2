using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public partial class Projectile : ManagedBehaviour
{
    [ReadOnly] public float damage;
    [ReadOnly] public float speed;
    [ReadOnly] public Targetable shotBy;
    [SerializeField] GameObject impactEffect;
    public virtual void Initialize(Targetable shotBy, float damage, float speed, float duration)
    {
        this.damage = damage;
        this.speed = speed;
        this.shotBy = shotBy;
        Destroy(gameObject, duration);
    }

    public override void ManagedUpdate()
    {
        MoveUpdate();
    }

    //Inheriting classes can override this to move in a different way
    protected void MoveUpdate()
    {
        float moveby = speed * Time.deltaTime;
        transform.position += transform.right * moveby;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Level"))
            return;

        var targetable = collision.GetComponent<Targetable>();
        
        if (targetable == shotBy)
            return;
        
        if (targetable == null)
        {
            Impact(collision, false);
            return;
        }

        targetable.Damage(damage);
        Impact(collision, true);
    }

    //Inheriting classes can override this to have different
    //impact behaviors (such as bouncing on walls, or piercing enemies)
    public void Impact(Collider2D collision, bool hitEnemy)
    {
        enabled = false;
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
