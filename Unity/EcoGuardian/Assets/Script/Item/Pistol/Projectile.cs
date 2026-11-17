using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;             // Velocidad del proyectil
    public LayerMask characterLayer;     // Capa de los personajes
    public float lifeTime = 5f;          // Tiempo de vida del proyectil
    public float damage = 10f;           // Daño del proyectil
    public Vector3 origin;               // Origen del proyectil, utilizado para parry
    private Vector3 currentDirection;    // Dirección actual del proyectil
    private float remainingLifeTime;     // Tiempo de vida restante

    protected virtual void Start()
    {
        // Almacena la posición inicial del proyectil
        origin = transform.position;

        // Configura la dirección inicial (hacia abajo en este caso)
        currentDirection = Vector3.down;

        // Inicializa el temporizador de vida
        remainingLifeTime = lifeTime;
    }

    protected virtual void Update()
    {
        // Mover el proyectil en la dirección actual
        transform.Translate(currentDirection * speed * Time.deltaTime,Space.Self);

        // Reducir el tiempo de vida restante
        remainingLifeTime -= Time.deltaTime;

        // Destruir el proyectil si el tiempo de vida llega a cero
        if (remainingLifeTime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield"))
        {
            Destroy(gameObject);
            return;
        }

        if (other.CompareTag("Parry"))
        {
            ResetLifeTime(); // Reiniciar el tiempo de vida
            return;
        }

        if ((characterLayer.value & (1 << other.gameObject.layer)) == 0) return;

        if (other.TryGetComponent(out Character character))
        {
            character.TakeDamage(damage);
        }

        // Destruir el proyectil al colisionar
        Destroy(gameObject);
    }

    // Método para actualizar la dirección del proyectil
    public void SetDirection(Vector3 newDirection)
    {
        currentDirection = newDirection.normalized;
        transform.LookAt(transform.position + currentDirection); // Ajustar la rotación para que apunte en la nueva dirección
    }

    // Método para reiniciar el tiempo de vida
    public void ResetLifeTime()
    {
        remainingLifeTime = lifeTime;
    }
}
