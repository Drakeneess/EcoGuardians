using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossArea : MonoBehaviour
{
    public BossLifebar bossLifebar;
    public Boss boss;
    public float activationRange = 10f; // Rango de activación para iniciar la batalla con el jefe
    private Transform playerTransform;  // Para guardar la referencia al transform del jugador

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;  // Obtener el transform del jugador
    }

    // Update is called once per frame
    void Update()
    {
        // Verificar si el jugador está lo suficientemente cerca
        if (playerTransform != null)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);

            // Si el jefe está activo y no está en batalla, y el jugador está dentro del rango
            if (boss.IsBossActive && !boss.IsBattleActive && distance <= activationRange)
            {
                boss.ActivateBattleMode();  // Activar el modo de batalla
                bossLifebar.boss = boss;    // Asignar el jefe a la barra de vida
            }
        }
    }
}
