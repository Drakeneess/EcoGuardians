using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;             // Velocidad del proyectil
    public LayerMask characterLayer;       // Capa de los personajes
    public float lifeTime = 5f;            // Tiempo de vida del proyectil

    private void Start()
    {
        // Destruir el proyectil después de su tiempo de vida
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        // Mover el proyectil en su dirección
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que colisiona pertenece a la capa de personajes
        if (((1 << other.gameObject.layer) & characterLayer) != 0)
        {
            // Aquí puedes implementar la lógica de daño o efecto del proyectil
            Debug.Log("Impacto con: " + other.gameObject.name);

            // Destruir el proyectil al colisionar
            Destroy(gameObject);
        }
    }
}
