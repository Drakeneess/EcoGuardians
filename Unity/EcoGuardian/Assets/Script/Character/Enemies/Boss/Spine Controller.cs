using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineController : MonoBehaviour
{
    public GameObject spinePrefab; // Prefab de la espina
    public int minSpines = 5; // Cantidad mínima de espinas a generar
    public int maxSpines = 15; // Cantidad máxima de espinas a generar
    public float spawnRadius = 10f; // Radio del área de generación
    public float minLaunchSpeed = 5f; // Velocidad mínima de lanzamiento
    public float maxLaunchSpeed = 15f; // Velocidad máxima de lanzamiento
    public float protrudeTime = 1f; // Tiempo que la espina tarda en sobresalir

    // Start is called before the first frame update
    void Start()
    {
    }

    // Método para generar espinas aleatorias
    public void SpawnRandomSpines()
    {
        int spineCount = Random.Range(minSpines, maxSpines + 1);

        for (int i = 0; i < spineCount; i++)
        {
            // Generar una dirección aleatoria hacia la superficie de la esfera
            Vector3 randomDirection = Random.onUnitSphere;

            // Instanciar la espina en el centro del Transform
            GameObject newSpine = Instantiate(spinePrefab, transform.position, Quaternion.identity);

            // Orientar la espina hacia la dirección generada
            newSpine.transform.rotation = Quaternion.LookRotation(randomDirection);

            // Iniciar el comportamiento de la espina (mover hacia afuera y luego lanzarse)
            newSpine.GetComponent<Spine>().InitializeWithProtrude(transform.position, randomDirection, spawnRadius, protrudeTime, Random.Range(minLaunchSpeed, maxLaunchSpeed));
        }
    }
}
