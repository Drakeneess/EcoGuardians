using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootDamageController : MonoBehaviour
{
    public float damage = 7; // Daño que inflige la raíz

    private void OnTriggerEnter(Collider other) {
        if (other.transform.CompareTag("Player"))
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                player.TakeDamage(damage);
            }
        }
    }
}
