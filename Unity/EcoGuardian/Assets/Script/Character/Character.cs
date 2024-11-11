using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    public float health = 100f;

    public float maxHealt { get; set; }
    public float currentHealth { get; set; }

    protected virtual void Start()
    {
        // Initialize health values
        maxHealt = health;
        currentHealth = maxHealt;
    }

    protected virtual void Update()
    {
    }

    public virtual void TakeDamage(float damage)
    {
        if (damage < 0) return; // Prevent healing through negative damage
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public virtual void Heal(float healAmount)
    {
        if (healAmount < 0) return; // Prevent damage through negative healing
        currentHealth += healAmount;
        if (currentHealth > maxHealt)
        {
            currentHealth = maxHealt;
        }
    }

    public virtual void Die(){

    }

    public float GetCurrentHealtPercentage()
    {
        return currentHealth / maxHealt;
    }
}
