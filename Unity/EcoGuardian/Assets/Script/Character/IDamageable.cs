using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    float maxHealt { get; set; }
    float currentHealth { get; set; }

    void TakeDamage(float damage);
    void Heal(float healAmount);
    void Die();
    float GetCurrentHealtPercentage();
}
