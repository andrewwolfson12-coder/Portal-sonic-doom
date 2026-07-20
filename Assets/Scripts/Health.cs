using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health = 100f;

    void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f) Die();
    }

    protected virtual void Die()
    {
        // If enemy
        var enemy = GetComponent<SimpleEnemy>();
        if (enemy != null) enemy.OnDeath();
        else Destroy(gameObject);
    }
}
