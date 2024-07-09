using Sirenix.OdinInspector;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    [DisableInEditorMode] public float currentHealth;
    public float maxHealth = 100;
    public bool isDead;
    public bool isVulnerable = true;

    protected void Awake()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        currentHealth = maxHealth;
    }

    //deals damage to this unit and checks if it's dead
    public void Damage(float amount)
    {
        if (isDead || !isVulnerable || amount <= 0)
            return;

        currentHealth -= amount;
        //play SFX

        if (currentHealth <= 0)
            Die();
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
        //play SFX
        Destroy(gameObject);
    }
}
