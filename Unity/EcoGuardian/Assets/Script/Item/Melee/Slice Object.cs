using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SliceObject : MonoBehaviour
{
    public LayerMask sliceableLayer; // Define las capas de los objetos que pueden ser afectados
    public float damage = 10f; // Daño que se aplicará
    public ParticleSystem sparks;

    private AudioSource audioSource;
    private AudioClip hitSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        hitSound = SoundController.GetSound(1); // Asegúrate de que SoundController funcione correctamente
    }

    private void PlayHitSound()
    {
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto está en el Layer deseado usando el LayerMask
        if (((1 << other.gameObject.layer) & sliceableLayer) != 0)
        {
            // Intenta obtener el componente Enemy del objeto
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && enemy.currentHealth > 0)
            {
                // Aplica daño al enemigo
                enemy.TakeDamage(damage);
                // Reproduce el sonido del golpe
                PlayHitSound();
                sparks.Play();
            }
        }
    }
}
