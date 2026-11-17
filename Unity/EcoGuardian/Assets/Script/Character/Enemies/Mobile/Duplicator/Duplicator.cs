using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duplicator : PatrollerEnemy
{
    [Header("Duplication Settings")]
    public GameObject duplicatePrefab; // Prefab del clon
    public int maxClones = 6; // Número máximo de clones que puede generar
    public float cloneSpawnRadius = 2f; // Radio alrededor del enemigo para generar clones
    public float detectionRange = 10f; // Rango en el que detecta al jugador
    public float followRange = 15f; // Rango en el que seguirá al jugador
    public float duplicationDelay = 40f; // Tiempo en segundos entre tandas de duplicación
    public float followSpeed = 3f; // Velocidad al seguir al jugador
    public Transform cloneSpawner;

    private List<GameObject> clones = new List<GameObject>(); // Lista para rastrear los clones creados
    private bool isDuplicating = false; // Controla si el duplicador está en proceso de generar clones
    private bool isFollowingPlayer = false; // Controla si el duplicador está siguiendo al jugador

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

            if (distanceToPlayer <= detectionRange)
            {
                // Cambiar al modo de seguimiento y duplicación
                isFollowingPlayer = true;

                if (clones.Count < maxClones && !isDuplicating)
                {
                    StartCoroutine(DuplicateWithDelay());
                }
                FollowPlayer();
            }
            else if (distanceToPlayer > followRange)
            {
                // Si el jugador está demasiado lejos, volver a patrullar
                isFollowingPlayer = false;
            }
        }

        if (!isFollowingPlayer)
        {
            patrolBehavior.Patrol(); // Volver a patrullar si no sigue al jugador
        }
    }

    private IEnumerator DuplicateWithDelay()
    {
        isDuplicating = true;

        // Generar hasta dos clones en esta tanda
        int clonesToSpawn = Mathf.Min(2, maxClones - clones.Count);

        for (int i = 0; i < clonesToSpawn; i++)
        {
            if (clones.Count >= maxClones) break;

            // Crear un nuevo clon en una posición aleatoria dentro del radio
            Vector3 spawnPosition = cloneSpawner.position + (Vector3)(Random.insideUnitCircle * cloneSpawnRadius);
            GameObject clone = Instantiate(duplicatePrefab, spawnPosition, Quaternion.identity);
            clone.transform.SetParent(transform.parent);

            clones.Add(clone); // Agregar el clon a la lista
        }

        // Esperar antes de permitir más duplicaciones
        yield return new WaitForSeconds(duplicationDelay);
        isDuplicating = false;
    }

    private void FollowPlayer()
    {
        // Moverse hacia el jugador manteniendo la posición Y
        Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        Vector3 direction = (targetPosition - transform.position).normalized;

        enemyContainer.transform.position += direction * followSpeed * Time.deltaTime;
    }

    public override void Die()
    {
        base.Die();

        // Destruir todos los clones cuando el enemigo original muere
        foreach (GameObject clone in clones)
        {
            if (clone != null)
            {
                Destroy(clone);
            }
        }
        clones.Clear();
    }
    public void RemoveClone(DuplicatorClone clone)
    {
        clones.Remove(clone.gameObject);
    }
}
