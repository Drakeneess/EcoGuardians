using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    public static RespawnController Instance { get; private set; }
    public Player player;
    private RespawnPoint[] respawnPoints;

    private void Awake()
    {
        // Implementar Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        respawnPoints = FindObjectsOfType<RespawnPoint>();
    }

    public static void RespawnPlayerInNearestPoint()
    {
        if (Instance == null || Instance.player == null)
        {
            Debug.LogWarning("RespawnController o Player no están configurados.");
            return;
        }

        // Buscar el punto de respawn más cercano
        RespawnPoint nearestPoint = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 playerPosition = Instance.player.transform.position;

        foreach (RespawnPoint point in Instance.respawnPoints)
        {
            float distance = Vector3.Distance(playerPosition, point.transform.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestPoint = point;
            }
        }

        if (nearestPoint != null)
        {
            // Reposicionar al jugador en el punto de respawn más cercano
            Instance.player.transform.position = nearestPoint.transform.position;
        }
        else
        {
            Debug.LogWarning("No se encontraron puntos de respawn.");
        }
    }
}
