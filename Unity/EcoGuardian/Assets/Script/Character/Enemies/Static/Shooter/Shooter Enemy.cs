using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : Enemy
{
    [Header("Shooting Settings")]
    public GameObject projectilePrefab; // Prefab del proyectil a disparar
    public Transform shootingPoint; // Punto desde el que se disparan los proyectiles
    public float shootingRange = 10f; // Rango de detección para disparar
    public float shootingCooldown = 3f; // Tiempo de espera entre disparos

    private float lastShootTime = 0f;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();

        if (player != null)
        {
            // Calcular la distancia al jugador
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            // Rotar el enemigo o el punto de disparo hacia el jugador
            Vector3 directionToPlayer = player.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(directionToPlayer);

            // Comprobar si el jugador está en rango y si el cooldown ha terminado
            if (distanceToPlayer <= shootingRange && Time.time >= lastShootTime + shootingCooldown)
            {
                Shoot();
                lastShootTime = Time.time; // Actualizar el tiempo del último disparo
            }
        }
    }

    private void Shoot()
    {
        if (projectilePrefab != null && shootingPoint != null)
        {
            // Crear el proyectil en el punto de disparo con la rotación actual
            Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);

            // Aquí podrías activar una animación de disparo si tienes un Animator
            if (animator != null)
            {
                animator.SetTrigger("Shoot");
            }
        }
    }
}
