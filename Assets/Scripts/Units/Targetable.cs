using Sirenix.OdinInspector;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    public string ID;

    [DisableInEditorMode] public float currentHealth;
    public float maxHealth = 100;
    public bool isDead;
    public bool isVulnerable = true;

    [Header("Visual Effects")]
    public GameObject Remains;

    protected void Awake()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        currentHealth = maxHealth;
    }

    //deals damage to this unit and checks if it's dead
    public virtual void Damage(float amount, Unit attacker = null)
    {
        if (isDead || !isVulnerable || amount <= 0)
            return;


        currentHealth -= amount;
        AudioManager.PlaySoundAtPoint(SoundType.Damage, gameObject.transform.position, 0.8f);

        if (currentHealth <= 0)
        {
            if (attacker != null)
            GameEvents.TargetableKilled(this, attacker);

            Die();
        }
    }

    public void Heal(float amount)
    {
        if (isDead || amount <= 0)
            return;

        if (currentHealth + amount > maxHealth)
            currentHealth = maxHealth;
        else
            currentHealth += amount;

        //play SFX
    }

    public virtual void Die()
    {
        isDead = true;
        Instantiate(Remains, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
