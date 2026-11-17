using System.Collections;
using UnityEngine;

public class Spine : Projectile
{
    private Vector3 targetPosition; // Posición final (superficie de la esfera)
    private float protrudeTime; // Tiempo que tarda en sobresalir
    private float launchSpeed; // Velocidad de lanzamiento
    private bool isProtruding = false; // Si la espina está sobresaliendo

    protected override void Start()
    {
        base.Start();
    }

    public void InitializeWithProtrude(Vector3 center, Vector3 direction, float radius, float protrudeTime, float launchSpeed)
    {
        // Calcular posición final
        targetPosition = center + direction * radius;
        this.protrudeTime = protrudeTime;
        this.launchSpeed = launchSpeed;

        // Iniciar el comportamiento de sobresalir
        StartCoroutine(ProtrudeAndLaunch());
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield"))
        {
            Destroy(gameObject);
            return;
        }

        if ((characterLayer.value & (1 << other.gameObject.layer)) == 0) return;
        if (other.TryGetComponent(out Player player))
        {
            player.TakeDamage(damage);
        }
    }

    protected override void Update()
    {
        transform.Translate(Vector3.forward* speed * Time.deltaTime,Space.Self);
    }

    private IEnumerator ProtrudeAndLaunch()
    {
        isProtruding = true;

        // Guardar la posición inicial (centro de la esfera)
        Vector3 initialPosition = transform.position;

        // Animar la espina para que sobresalga hacia la superficie
        float elapsedTime = 0f;
        while (elapsedTime < protrudeTime)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / protrudeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurar que llega a la posición final
        transform.position = targetPosition;


        isProtruding = false;

        // Destruir después de un tiempo
        Destroy(gameObject, 5f);
    }
}
