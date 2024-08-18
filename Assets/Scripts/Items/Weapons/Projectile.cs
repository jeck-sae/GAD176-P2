using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public partial class Projectile : MonoBehaviour
{
    //these values are set by the weapon shooting them
    public float damage;
    public float speed;
    public Unit shotBy;
    [SerializeField] GameObject impactEffect;

    public virtual void Initialize(Unit shotBy, float damage, float speed, float duration)
    {
        this.damage = damage;
        this.speed = speed;
        this.shotBy = shotBy;
        Destroy(gameObject, duration);
    }

    public void Update()
    {
        MoveUpdate();
    }

    //Inheriting classes can override this to move in a different way
    protected void MoveUpdate()
    {
        float moveby = speed * Time.deltaTime;
        transform.position += transform.up * moveby;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(GameManager.UnitsLayer) &&
            collision.gameObject.layer.Equals(GameManager.LevelLayer))
            return;

        var targetable = collision.GetComponent<Targetable>();

        if (shotBy != null && targetable == shotBy)
            return;
        
        if (targetable == null)
        {
            Impact(collision, false);
            return;
        }

        targetable.Damage(damage, shotBy);
        Impact(collision, true);
    }

    //Inheriting classes can override this to have different
    //impact behaviors (such as bouncing on walls, or piercing through enemies)
    public void Impact(Collider2D collision, bool hitEnemy)
    {
        if(impactEffect)
            Instantiate(impactEffect, transform.position, transform.rotation);

        enabled = false;
        Destroy(gameObject);
    }
}
