using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstraCollision : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Level"))
        {
            AudioManager.PlaySoundAtPoint(SoundType.Cracking, gameObject.transform.position, 1f);
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Units"))
        {
            var targetable = collision.GetComponent<Targetable>();

            if (targetable == null)
                return;

            targetable.Damage(damage);
        }
    }
}
