using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SporeFogController : MonoBehaviour
{
    public Volume volume; // El volumen de la niebla
    public float maxDistance = 10f; // Distancia máxima a la que el efecto de niebla será nulo
    public float minDistance = 0f; // Distancia mínima a la que el efecto de niebla será máximo

    protected Transform player; // Referencia al transform del jugador

    protected virtual void Start() {
        player=GetComponent<Sporer>().GetPlayer().transform;
    }
    protected virtual void Update()
    {
        if (player != null && volume != null)
        {
            // Calcular la distancia entre el jugador y el centro de la niebla
            float distance = Vector3.Distance(transform.position, player.position);

            // Ajustar el peso del volumen según la distancia
            // Si la distancia es mayor que la distancia máxima, el peso es 0 (sin efecto)
            // Si la distancia es menor que la distancia mínima, el peso es 1 (efecto completo)
            float weight = Mathf.Clamp01(1 - (distance - minDistance) / (maxDistance - minDistance));
            volume.weight = weight;
        }
    }
}
