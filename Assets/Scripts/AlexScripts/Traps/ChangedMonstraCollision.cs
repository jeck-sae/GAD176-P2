using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangedMonstraCollision : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.gameObject.layer == LayerMask.NameToLayer("Units"))
        {
            var targetable = collision.GetComponent<Targetable>();

            if (targetable == null)
                return;

            targetable.Damage(damage);
        }
    }
}
