using System.Collections;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public LineRenderer line;         // Referencia al LineRenderer que representará el rayo
    public float maxDistance = 50f;   // Distancia máxima del rayo
    private Ray ray;                  // El rayo que lanzaremos
    private RaycastHit hit;           // Almacena la información del objeto golpeado por el rayo

    void Start()
    {
        if (line == null)
        {
            line = gameObject.AddComponent<LineRenderer>();
        }

        // Configuración del LineRenderer
        line.positionCount = 2;  // Solo necesitamos dos puntos: inicio y fin del rayo
        line.startWidth = 0.02f; // Ancho del rayo al inicio
        line.endWidth = 0.02f;   // Ancho del rayo al final

        // Puedes cambiar los colores según tus preferencias
        line.startColor = Color.red;
        line.endColor = Color.red;
    }

    void Update()
    {
        // Configuramos el rayo desde la posición actual del objeto y hacia adelante
        ray = new Ray(transform.position, transform.forward);

        // Determina la distancia del rayo dependiendo de si golpea algo o no
        float distance = maxDistance;

        // Si el rayo golpea un objeto
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            distance = hit.distance; // Ajusta la distancia al punto de impacto
        }

        // Configura la posición inicial y final del LineRenderer
        line.SetPosition(0, transform.position);                 // Posición de inicio del rayo
        line.SetPosition(1, transform.position + transform.forward * distance); // Posición final del rayo
    }
}
